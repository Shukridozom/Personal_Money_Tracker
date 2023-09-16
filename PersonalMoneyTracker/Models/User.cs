namespace PersonalMoneyTracker.Models
{
    public class User
    {
        public User()
        {
            Wallets= new List<Wallet>();
            TransactionCategories = new List<TransactionCategory>();
            Transactions = new List<Transaction>();
        }
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public DateTime RegisterDate { get; set; }
        public IList<Wallet> Wallets { get; set; }
        public IList<TransactionCategory> TransactionCategories { get; set; }
        public IList<Transaction> Transactions { get; set; }
    }
}
