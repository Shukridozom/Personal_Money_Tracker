namespace PersonalMoneyTracker.Models
{
    public class IncomeCategory
    {
        public IncomeCategory()
        {
            Incomings = new List<Incoming>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public IList<Incoming> Incomings { get; set; }

    }
}
