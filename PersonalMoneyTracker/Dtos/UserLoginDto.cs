using PersonalMoneyTracker.CustomValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace PersonalMoneyTracker.Dtos
{
    public class UserLoginDto
    {
        public int? Id { get; set; }
        [Required]
        [MinLength(6)]
        [MaxLength(255)]
        public string Username { get; set; }
        [Required]
        [MinLength(6)]
        [MaxLength(255)]
        public string Password { get; set; }

    }
}
