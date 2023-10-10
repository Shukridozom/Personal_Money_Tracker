using PersonalMoneyTracker.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace PersonalMoneyTracker.Dtos
{
    public class WalletDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
