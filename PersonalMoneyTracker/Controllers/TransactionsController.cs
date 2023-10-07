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

            var walletKeys = _unitOfWork.Wallets.GetUserWalletIds(userId);
            var transactionCategoryKeys = _unitOfWork.TransactionCategories
                .GetUserTransactionCategoryIds(userId);

            if (!walletKeys.Contains(transactionDto.WalletId))
                return BadRequest("Unavailable wallet");

            if (!transactionCategoryKeys.Contains(transactionDto.TransactionCategoryId))
                return BadRequest("Unavailable transactionCategory");


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

            var walletKeys = _unitOfWork.Wallets.GetUserWalletIds(userId);
            var transactionCategoryKeys = _unitOfWork.TransactionCategories
                .GetUserTransactionCategoryIds(userId);

            if (!walletKeys.Contains(transactionDto.WalletId))
                return BadRequest("Unavailable wallet");

            if (!transactionCategoryKeys.Contains(transactionDto.TransactionCategoryId))
                return BadRequest("Unavailable transactionCategory");

            _mapper.Map(transactionDto,transactionFromDb);
            transactionFromDb.UserId = userId;

            _unitOfWork.Complete();

            return Ok(transactionDto);

        }

        [HttpPut("/MoveTransactionsToWallet")]
        public IActionResult MoveTransactionsToWallet(int sourceWalletId, int destinationWalletId)
        {
            if (sourceWalletId == destinationWalletId)
                return BadRequest("Wallets must be different");

            var userId = GetLoggedInUserId();
            var walletKeys = _unitOfWork.Wallets.GetUserWalletIds(userId);

            if (!(walletKeys.Contains(sourceWalletId) || walletKeys.Contains(destinationWalletId)))
                return BadRequest("Unavailable wallet");

            var transactions = _unitOfWork.Transactions
                .Find(t => t.UserId == userId && t.WalletId == sourceWalletId);

            foreach (var transaction in transactions)
                transaction.WalletId = destinationWalletId;

            _unitOfWork.Complete();

            return Ok();
        }

        [HttpPut("/MoveTransactionsToCategory")]
        public IActionResult MoveTransactionsToCategory(int sourceCategoryId, int destinationCategoryId)
        {
            if (sourceCategoryId == destinationCategoryId)
                return BadRequest("Categories must be different");

            var userId = GetLoggedInUserId();

            var categoryKeys = _unitOfWork.TransactionCategories.GetUserTransactionCategoryIds(userId);

            if (!(categoryKeys.Contains(sourceCategoryId) || categoryKeys.Contains(destinationCategoryId)))
                return BadRequest("Unavailable Category");

            var sourceCategory = _unitOfWork.TransactionCategories.Get(sourceCategoryId);
            var destinationCategory = _unitOfWork.TransactionCategories.Get(destinationCategoryId);
            if (sourceCategory.TransactionTypeId != destinationCategory.TransactionTypeId)
                return BadRequest("Source and destination categories must be of the same type (payment - income)");

            var transactions = _unitOfWork.Transactions
                .Find(t => t.UserId == userId && t.TransactionCategoryId == sourceCategoryId);

            foreach (var transaction in transactions)
                transaction.TransactionCategoryId = destinationCategoryId;

            _unitOfWork.Complete();

            return Ok();
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

        [HttpDelete("DeleteWalletTransactions")]
        public IActionResult DeleteWalletTransactions(int walletId)
        {
            var userId = GetLoggedInUserId();
            var walletKeys = _unitOfWork.Wallets.GetUserWalletIds(userId);

            if (!walletKeys.Contains(walletId))
                return BadRequest("Unavailable wallet");

            var transactions = _unitOfWork.Transactions
                .Find(t => t.UserId == userId && t.WalletId == walletId);

            _unitOfWork.Transactions.RemoveRange(transactions);

            _unitOfWork.Complete();

            return Ok();

        }


        [HttpDelete("DeleteCategoryTransactions")]
        public IActionResult DeleteCategoryTransactions(int categoryId)
        {
            var userId = GetLoggedInUserId();
            var categoryKeys = _unitOfWork.TransactionCategories.GetUserTransactionCategoryIds(userId);

            if (!categoryKeys.Contains(categoryId))
                return BadRequest("Unavailable category");

            var transactions = _unitOfWork.Transactions
                .Find(t => t.UserId == userId && t.TransactionCategoryId == categoryId);

            _unitOfWork.Transactions.RemoveRange(transactions);

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
