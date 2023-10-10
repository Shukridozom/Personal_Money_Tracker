using PersonalMoneyTracker.CustomValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace PersonalMoneyTracker.Dtos
{
    public class UserChangePasswordDto
    {
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
