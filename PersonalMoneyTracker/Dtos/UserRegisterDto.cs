using PersonalMoneyTracker.Core.Models;
using PersonalMoneyTracker.CustomValidationAttributes;
using PersonalMoneyTracker.Persistence;
using System.ComponentModel.DataAnnotations;

namespace PersonalMoneyTracker.Dtos
{
    public class UserRegisterDto
    {
        [UniqueUsername]
        public string Username { get; set; }
        public string Password { get; set; }
        [Compare("Password",ErrorMessage ="Password and Confirm Password must match")]
        public string ConfirmPassword { get; set; }
    }
}
