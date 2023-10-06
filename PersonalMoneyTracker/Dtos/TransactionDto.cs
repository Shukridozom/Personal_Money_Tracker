using PersonalMoneyTracker.Core.Models;
using PersonalMoneyTracker.CustomValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace PersonalMoneyTracker.Dtos
{
    public class TransactionDto
    {
        public int Id { get; set; }
        [SetToCurrentDateTimeIfNull]
        public DateTime? Date { get; set; }
        [Required]
        public double Amount { get; set; }
        public string? Reason { get; set; }
        [Required]
        public int WalletId { get; set; }
        [Required]
        public int TransactionCategoryId { get; set; }

    }
}
