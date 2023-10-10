using PersonalMoneyTracker.Core.Models;
using PersonalMoneyTracker.Dtos;
using PersonalMoneyTracker.Persistence;
using System.ComponentModel.DataAnnotations;

namespace PersonalMoneyTracker.CustomValidationAttributes
{
    public class SetToCurrentDateTimeIfNull : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            using (var context = new AppDbContext())
            {
                var date = ((TransactionDto)validationContext.ObjectInstance).Date;
                if (date == null)
                    ((TransactionDto)validationContext.ObjectInstance).Date = DateTime.Now;
            }

                return ValidationResult.Success;

        }
    }
}
