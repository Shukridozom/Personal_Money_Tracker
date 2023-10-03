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
        public int UserId { get; set; }
        [Required]
        public int TransactionTypeId { get; set; }
    }
}
