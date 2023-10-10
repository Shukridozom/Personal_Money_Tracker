using PersonalMoneyTracker.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace PersonalMoneyTracker.Dtos
{
    public class TransactionCategoryDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(1,2, ErrorMessage = "The field TransactionTypeId must be either 1 (for Income) or 2 (for Payment).")]
        public int TransactionTypeId { get; set; }
    }
}
