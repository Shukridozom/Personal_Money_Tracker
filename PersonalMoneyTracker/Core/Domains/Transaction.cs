namespace PersonalMoneyTracker.Core.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public string? Reason { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int WalletId { get; set; }
        public Wallet Wallet { get; set; }
        public int TransactionCategoryId { get; set; }
        public TransactionCategory TransactionCategory { get; set; }
        public TransactionType TransactionType { get; set; }
        public int TransactionTypeId { get; set; }

    }
}
