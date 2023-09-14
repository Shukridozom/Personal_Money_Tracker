namespace PersonalMoneyTracker.Models
{
    public class TransactionType
    {
        public TransactionType()
        {
            Transactions = new List<Transaction>();
            TransactionCategories = new List<TransactionCategory>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<TransactionCategory> TransactionCategories { get; set; }
        public IList<Transaction> Transactions { get; set; }

    }
}
