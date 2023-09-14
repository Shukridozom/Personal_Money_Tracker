namespace PersonalMoneyTracker.Models
{
    public class User
    {
        public User()
        {
            Wallets= new List<Wallet>();
            PaymentCategories = new List<TransactionCategory>();
            Payments = new List<Transaction>();
        }
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public DateTime RegisterDate { get; set; }
        public IList<Wallet> Wallets { get; set; }
        public IList<TransactionCategory> PaymentCategories { get; set; }
        public IList<Transaction> Payments { get; set; }
    }
}
