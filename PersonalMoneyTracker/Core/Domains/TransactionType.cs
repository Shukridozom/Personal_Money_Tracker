namespace PersonalMoneyTracker.Core.Models
{
    public class TransactionType
    {
        public TransactionType()
        {
            TransactionCategories = new List<TransactionCategory>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<TransactionCategory> TransactionCategories { get; set; }
    }
}
