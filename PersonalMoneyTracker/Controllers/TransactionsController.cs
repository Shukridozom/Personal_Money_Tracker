using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalMoneyTracker.Core.Models;
using PersonalMoneyTracker.Core.Repositories;
using PersonalMoneyTracker.Dtos;
using System.Security.Claims;

namespace PersonalMoneyTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransactionsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public TransactionsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult Get()
        {
            var userId = GetLoggedInUserId();
            var transactions = _unitOfWork.Transactions.GetUserTransactions(userId);

            List<TransactionDto> transactionDtos = new List<TransactionDto>();

            foreach (var transaction in transactions)
                transactionDtos.Add(_mapper.Map<Transaction, TransactionDto>(transaction));

            return Ok(transactionDtos);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var userId = GetLoggedInUserId();
            var transaction = _unitOfWork.Transactions.Get(id);

            if (transaction == null || transaction.UserId != userId)
                return NotFound();

            var transactionDto = _mapper.Map<Transaction, TransactionDto>(transaction);

            return Ok(transactionDto);
        }

        [HttpPost]
        public IActionResult Post(TransactionDto transactionDto)
        {
            var userId = GetLoggedInUserId();
            var transaction = _mapper.Map<TransactionDto, Transaction>(transactionDto);
            transaction.UserId = userId;

            var walletKeys = _unitOfWork.Wallets
                .Find(w => w.UserId == userId)
                .Select(w => w.Id)
                .ToList();

            var transactionCategoryKeys = _unitOfWork.TransactionCategories
                .Find(tc => tc.UserId == userId)
                .Select(tc => tc.Id)
                .ToList();

            if (!walletKeys.Contains(transactionDto.WalletId))
                return BadRequest("Unavailable walletId");

            if (!transactionCategoryKeys.Contains(transactionDto.TransactionCategoryId))
                return BadRequest("Unavailable transactionCategoryId");

            _unitOfWork.Transactions.Add(transaction);
            _unitOfWork.Complete();

            _mapper.Map(transaction, transactionDto);

            return CreatedAtAction(nameof(Get), new { id = transaction.Id }, transactionDto);
        }

        [HttpPut]
        public IActionResult Put(TransactionDto transactionDto)
        {
            var userId = GetLoggedInUserId();
            var transactionFromDb = _unitOfWork.Transactions
                .SingleOrDefault(t => t.Id == transactionDto.Id && t.UserId == userId);

            if (transactionFromDb == null)
                return BadRequest();

            var walletKeys = _unitOfWork.Wallets
                .Find(w => w.UserId == userId)
                .Select(w => w.Id)
                .ToList();

            var transactionCategoryKeys = _unitOfWork.TransactionCategories
                .Find(tc => tc.UserId == userId)
                .Select(tc => tc.Id)
                .ToList();

            if (!walletKeys.Contains(transactionDto.WalletId))
                return BadRequest("Unavailable walletId");

            if (!transactionCategoryKeys.Contains(transactionDto.TransactionCategoryId))
                return BadRequest("Unavailable transactionCategoryId");

            _mapper.Map(transactionDto,transactionFromDb);
            transactionFromDb.UserId = userId;

            _unitOfWork.Complete();

            return Ok(transactionDto);

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var userId = GetLoggedInUserId();
            var transaction = _unitOfWork.Transactions
                .SingleOrDefault(t => t.UserId == userId && t.Id == id);

            if (transaction == null)
                return BadRequest();

            _unitOfWork.Transactions.Remove(transaction);
            _unitOfWork.Complete();

            return Ok();

        }


        private int GetLoggedInUserId()
        {
            var claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            return Int32.Parse(claim.Value);
        }
    }


}
