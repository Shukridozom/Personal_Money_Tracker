namespace PersonalMoneyTracker.Core.Models
{
    public class Wallet
    {
        public Wallet()
        {
            Transactions = new List<Transaction>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public IList<Transaction> Transactions { get; set; }
    }
}
