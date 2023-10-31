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
    public class TransactionCategoriesController : AppBaseController
    {
        public TransactionCategoriesController(IMapper mapper, IUnitOfWork unitOfWork)
            :base(mapper, unitOfWork)
        {

        }

        [HttpGet]
        public IActionResult Get()
        {
            var userId = GetLoggedInUserId();

            var transactionCategories = _unitOfWork
                .TransactionCategories
                .GetUserTransactionCategories(userId);

            List<TransactionCategoryDto> transactionCategoryDtos = new List<TransactionCategoryDto>();

            foreach (var tc in transactionCategories)
                transactionCategoryDtos.Add(_mapper.Map<TransactionCategory, TransactionCategoryDto>(tc));

            return Ok(transactionCategoryDtos);
        }

        [HttpGet("getPaymentCategories")]
        public IActionResult GetPaymentCategories()
        {
            var userId = GetLoggedInUserId();

            var transactionCategories = _unitOfWork
                .TransactionCategories
                .GetUserPaymentTransactionCategories(userId);

            List<TransactionCategoryDto> transactionCategoryDtos = new List<TransactionCategoryDto>();

            foreach (var tc in transactionCategories)
                transactionCategoryDtos.Add(_mapper.Map<TransactionCategory, TransactionCategoryDto>(tc));

            return Ok(transactionCategoryDtos);
        }

        [HttpGet("getIncomeCategories")]
        public IActionResult GetIncomeCategories()
        {
            var userId = GetLoggedInUserId();

            var transactionCategories = _unitOfWork
                .TransactionCategories
                .GetUserIncomeTransactionCategories(userId);

            List<TransactionCategoryDto> transactionCategoryDtos = new List<TransactionCategoryDto>();

            foreach (var tc in transactionCategories)
                transactionCategoryDtos.Add(_mapper.Map<TransactionCategory, TransactionCategoryDto>(tc));

            return Ok(transactionCategoryDtos);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var userId = GetLoggedInUserId();

            var transactionCategory = _unitOfWork
                .TransactionCategories.Get(id);

            if (transactionCategory == null || transactionCategory?.UserId != userId)
                return NotFound();

            var transactionCategoryDto = _mapper.Map<TransactionCategory, TransactionCategoryDto>(transactionCategory);

            return Ok(transactionCategoryDto);
        }

        [HttpPost]
        public IActionResult Post(TransactionCategoryDto transactionCategoryDto)
        {
            var userId = GetLoggedInUserId();

            var transactionCategory = _mapper.Map<TransactionCategoryDto, TransactionCategory>(transactionCategoryDto);
            transactionCategory.UserId = userId;

            var transactionCategoryFromDb = _unitOfWork.TransactionCategories
                .SingleOrDefault(tc => tc.UserId == userId 
                && tc.Name == transactionCategory.Name
                && tc.TransactionTypeId == transactionCategory.TransactionTypeId
                );

            if (transactionCategoryFromDb != null)
                return BadRequest("You have another transaction category with the same name and Type");


            _unitOfWork.TransactionCategories.Add(transactionCategory);
            _unitOfWork.Complete();

            return CreatedAtAction(nameof(Get), new { id = transactionCategory.Id }, transactionCategoryDto);
        }

        [HttpPut("renameCategory")]
        public IActionResult RenameCategory(int currentCategoryId, TransactionCategoryDto newTransactionCategory)
        {
            var userId = GetLoggedInUserId();

            var CategoryFromDb = _unitOfWork.TransactionCategories
                .SingleOrDefault(tc => tc.Id == currentCategoryId && tc.UserId == userId);

            if (CategoryFromDb == null)
                return BadRequest();

            //check if the name is valid
            var categoryWithSameName = _unitOfWork.TransactionCategories
                .SingleOrDefault(tc => tc.UserId == userId 
                && tc.Name == newTransactionCategory.Name
                && tc.TransactionTypeId == CategoryFromDb.TransactionTypeId);
            if (categoryWithSameName != null)
                return BadRequest("You have another transaction category with the same name and Type");


            CategoryFromDb.Name = newTransactionCategory.Name;

            _unitOfWork.Complete();

            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var userId = GetLoggedInUserId();

            var categoryWithTransactions = _unitOfWork.TransactionCategories
                .GetTransactionCategoryWithTransactions(id);

            var userCategoryKeysList = _unitOfWork.TransactionCategories
                .GetUserTransactionCategoryIds(userId);

            if (!userCategoryKeysList.Contains(id)) // This category is not for this user
                return BadRequest();

            if (categoryWithTransactions == null)
                return BadRequest();

            if (categoryWithTransactions.Transactions == null)
                return BadRequest("This category is not empty, delete its transactions or move them to another category before deleting it");

            _unitOfWork.TransactionCategories.Remove(categoryWithTransactions);

            _unitOfWork.Complete();

            return Ok();
        }


    }
}
