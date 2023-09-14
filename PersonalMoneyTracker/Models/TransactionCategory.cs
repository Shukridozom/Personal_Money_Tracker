namespace PersonalMoneyTracker.Models
{
    public class TransactionCategory
    {
        public TransactionCategory()
        {
            Payments = new List<Transaction>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public IList<Transaction> Payments { get; set; }
        public TransactionType TransactionType { get; set; }
        public int TransactionTypeId { get; set; }
    }
}
