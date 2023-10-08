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

    public class WalletsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public WalletsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var userId = GetLoggedInUserId();
            var wallets = _unitOfWork.Wallets.GetUserWallets(userId);

            List<WalletDto> walletDtos = new List<WalletDto>();

            foreach (var wallet in wallets)
                walletDtos.Add(_mapper.Map<Wallet, WalletDto>(wallet));

            return Ok(walletDtos);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var userId = GetLoggedInUserId();
            var wallet = _unitOfWork.Wallets.Get(id);

            if (wallet == null || wallet.UserId != userId)
                return NotFound();

            var walletDto = _mapper.Map<Wallet, WalletDto>(wallet);

            return Ok(walletDto);
        }

        [HttpPost]
        public IActionResult Post(WalletDto walletDto)
        {
            var userId = GetLoggedInUserId();
            var wallet = _mapper.Map<WalletDto, Wallet>(walletDto);
            wallet.UserId = userId;

            var walletFromDb = _unitOfWork.Wallets
                .SingleOrDefault(w => w.UserId == wallet.UserId && w.Name == wallet.Name);

            if (walletFromDb != null)
                return BadRequest("You have another wallet with the same name");


            _unitOfWork.Wallets.Add(wallet);
            _unitOfWork.Complete();

            return CreatedAtAction(nameof(Get), new { id = wallet.Id }, walletDto);
        }

        [HttpPut("renameWallet")]
        public IActionResult RenameWallet(int currentWalletId, WalletDto newWallet)
        {
            var userId = GetLoggedInUserId();
            var walletFromDb = _unitOfWork.Wallets
                .SingleOrDefault(w => w.Id == currentWalletId && w.UserId == userId);

            if (walletFromDb == null)
                return BadRequest();

            //check if the name is valid
            var walletWithTheSameNewName = _unitOfWork.Wallets
                .SingleOrDefault(w => w.UserId == userId && w.Name == newWallet.Name);
            if(walletWithTheSameNewName != null)
                return BadRequest("You have another wallet with the same name");


            walletFromDb.Name = newWallet.Name;

            _unitOfWork.Complete();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var userId = GetLoggedInUserId();

            var walletWithTransactions = _unitOfWork.Wallets
                .GetWalletWithTransactions(id);

            var userWalletKeysList = _unitOfWork.Wallets
                .GetUserWalletIds(userId);

            if (!userWalletKeysList.Contains(id)) // This wallet is not for this user
                return BadRequest();

            if (walletWithTransactions == null)
                return BadRequest();

            if (walletWithTransactions.Transactions == null)
                return BadRequest("This wallet is not empty, delete its transactions or move them to another wallet before deleting it");

            _unitOfWork.Wallets.Remove(walletWithTransactions);

            _unitOfWork.Complete();

            return Ok();
        }

        [HttpGet("GetWalletCarryOver")]
        public IActionResult GetWalletCarryOver(int walletId, DateTime day)
        {
            var userId = GetLoggedInUserId();
            var walletKeys = _unitOfWork.Wallets.GetUserWalletIds(userId);

            if (!walletKeys.Contains(walletId))
                return NotFound();

            var carryOver = _unitOfWork.Wallets.GetWalletCarryOver(walletId, day.Date);

            return Ok(carryOver);
        }

        [HttpGet("GetWalletBalance")]
        public IActionResult GetWalletBalance(int walletId)
        {
            var userId = GetLoggedInUserId();
            var walletKeys = _unitOfWork.Wallets.GetUserWalletIds(userId);

            if (!walletKeys.Contains(walletId))
                return NotFound();

            var balance = _unitOfWork.Wallets.GetWalletBalance(walletId);

            return Ok(balance);
        }
        private int GetLoggedInUserId()
        {
            var claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            return Int32.Parse(claim.Value);
        }
    }
}
