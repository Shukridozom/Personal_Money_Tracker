using PersonalMoneyTracker.Core.Models;
using PersonalMoneyTracker.Core.Repositories;
using PersonalMoneyTracker.Persistence;
using System.ComponentModel.DataAnnotations;

namespace PersonalMoneyTracker.CustomValidationAttributes
{
    public class UniqueUsername : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            User username;
            using (var context = new AppDbContext())
            {
                username = context.Users.SingleOrDefault(u => u.Username == (string)validationContext.ObjectInstance);
            }

            if (username == null)
                return ValidationResult.Success;

            return new ValidationResult("Username already exists");
        }
    }
}
