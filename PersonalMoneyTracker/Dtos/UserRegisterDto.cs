using PersonalMoneyTracker.Core.Models;
using PersonalMoneyTracker.CustomValidationAttributes;
using PersonalMoneyTracker.Persistence;
using System.ComponentModel.DataAnnotations;

namespace PersonalMoneyTracker.Dtos
{
    public class UserRegisterDto
    {
        [Required]
        [UniqueUsername]
        [MinLength(6)]
        [MaxLength(255)]
        public string Username { get; set; }
        [Required]
        [MinLength(6)]
        [MaxLength(255)]
        public string Password { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(255)]
        [Compare("Password", ErrorMessage = "Password and Confirm Password must match")]
        public string ConfirmPassword { get; set; }
    }
}