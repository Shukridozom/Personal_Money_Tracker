namespace PersonalMoneyTracker.Models
{
    public class Incoming
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public string? Reason { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int WalletId { get; set; }
        public Wallet Wallet { get; set; }
        public int IncomeCategoryId { get; set; }
        public IncomeCategory IncomeCategory { get; set; }

    }
}
