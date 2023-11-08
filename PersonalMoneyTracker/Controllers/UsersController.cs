using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using PersonalMoneyTracker.Core.Models;
using PersonalMoneyTracker.Core.Repositories;
using PersonalMoneyTracker.Dtos;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ZstdSharp.Unsafe;

namespace PersonalMoneyTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : AppBaseController
    {
        public UsersController(IMapper mapper, IUnitOfWork unitOfWork)
            :base(mapper, unitOfWork)
        {

        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = _unitOfWork.Users.Get(id);
            if (user == null)
                return NotFound();

            return Ok(_mapper.Map<User, UserDto>(user));
        }

        [HttpPost("/api/login")]
        public async Task<IActionResult> Login(UserLoginDto userDto)
        {
            var user = _mapper.Map<UserLoginDto, User>(userDto);
            user.PasswordHash = HashPassword(userDto.Password);
            
            var userFromDb = _unitOfWork.Users.SingleOrDefault(u => u.Username == userDto.Username);
            if (userFromDb == null)
                return BadRequest("Username or password is incorrect");

            if (userFromDb.PasswordHash != user.PasswordHash)
                return BadRequest("Username or password is incorrect");

            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, userFromDb.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, userFromDb.Username));
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties() { IsPersistent = false }
                );

            return Ok();
        }


        [HttpPost("/api/register")]
        public IActionResult Register(UserRegisterDto userDto)
        {
            var user = _mapper.Map<UserRegisterDto, User>(userDto);

            var userFromDb = _unitOfWork.Users.SingleOrDefault(u => u.Username == userDto.Username);
            if (userFromDb != null)
                return BadRequest("Username already exists");

            user.PasswordHash = HashPassword(userDto.Password);
            user.RegisterDate = DateTime.Now;

            _unitOfWork.Users.Add(user);
            _unitOfWork.Complete();

            AddDefaultWalletsToUser(user.Id);
            AddDefaultTransactionCategoriesToUser(user.Id);

            var userInfo = _mapper.Map<User, UserDto>(user);

            return CreatedAtAction(nameof(Get), new { id = user.Id }, userInfo);
        }


        [HttpPost("/api/logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return NoContent();
        }



        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            var userId = GetLoggedInUserId();

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            _unitOfWork.Users.DeleteUser(userId);
            _unitOfWork.Complete();

            return Ok();
        }

        [Authorize]
        [HttpPut("changepassword")]
        public IActionResult ChangePassword(UserChangePasswordDto userDto)
        {
            var userId = GetLoggedInUserId();

            var user = _unitOfWork.Users.Get(userId);
            user.PasswordHash = HashPassword(userDto.Password);
            _unitOfWork.Complete();

            return Ok();
        }
        private string HashPassword(string password)
        {
            string hash;
            using (SHA512 sha512Hash = SHA512.Create())
            {
                byte[] sourceBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha512Hash.ComputeHash(sourceBytes);
                hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);

            }

            return hash;
        }

        private void AddDefaultWalletsToUser(int id)
        {
            List<string> walletNames = new List<string>()
            {
                "Payment Card",
                "Cash"
            };

            foreach(var wallet in walletNames)
                _unitOfWork.Wallets.Add(new Wallet()
                {
                    UserId = id,
                    Name = wallet
                });

            _unitOfWork.Complete();
        }

        private void AddDefaultTransactionCategoriesToUser(int id)
        {
            List<string> paymentCategoriesNames = new List<string>()
            {
                "House",
                "Food",
                "Transport",
                "Health",
                "Communications"
            };

            List<string> incomeCategoriesNames = new List<string>()
            {
                "Salary",
                "Savings"
            };

            foreach(var category in paymentCategoriesNames)
                _unitOfWork.TransactionCategories.Add(new TransactionCategory()
                {
                    UserId = id,
                    TransactionTypeId = 2, // Payment
                    Name = category
                });

            foreach (var category in incomeCategoriesNames)
                _unitOfWork.TransactionCategories.Add(new TransactionCategory()
                {
                    UserId = id,
                    TransactionTypeId = 1, // Income
                    Name = category
                });

            _unitOfWork.Complete();
        }

    }
}
