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
    public class TransactionCategoriesController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public TransactionCategoriesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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

            if (transactionCategory == null || transactionCategory.UserId != userId)
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


        private int GetLoggedInUserId()
        {
            var claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            return Int32.Parse(claim.Value);
        }

    }
}
