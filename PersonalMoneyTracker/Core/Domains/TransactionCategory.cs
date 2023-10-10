namespace PersonalMoneyTracker.Core.Models
{
    public class TransactionCategory
    {
        public TransactionCategory()
        {
            Transactions = new List<Transaction>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public IList<Transaction> Transactions { get; set; }
        public TransactionType TransactionType { get; set; }
        public int TransactionTypeId { get; set; }
    }
}
