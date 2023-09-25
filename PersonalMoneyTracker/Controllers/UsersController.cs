using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uintOfWork;

        public UsersController(IMapper mapper, IUnitOfWork uintOfWork)
        {
            _mapper = mapper;
            _uintOfWork = uintOfWork;
        }

        [HttpPost("/api/login")]
        public async Task<IActionResult> Login(UserLoginDto userDto)
        {
            var user = _mapper.Map<UserLoginDto, User>(userDto);
            user.PasswordHash = HashPassword(userDto.Password);
            
            var userFromDb = _uintOfWork.Users.SingleOrDefault(u => u.Username == userDto.Username);
            if (userFromDb == null)
                return BadRequest("Username or password is incorrect");

            if (userFromDb.PasswordHash != user.PasswordHash)
                return BadRequest("Username or password is incorrect");

            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.Username));
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
            user.PasswordHash = HashPassword(userDto.Password);
            user.RegisterDate = DateTime.Now;

            _uintOfWork.Users.Add(user);
            _uintOfWork.Complete();

            return Created(new Uri(Request.GetDisplayUrl() + "/" + user.Id), null);
        }

        [HttpPost("/api/logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return NoContent();
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
    }
}
