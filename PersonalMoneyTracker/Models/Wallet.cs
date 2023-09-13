namespace PersonalMoneyTracker.Models
{
    public class Wallet
    {
        public Wallet()
        {
            Payments = new List<Payment>();
            Incomings = new List<Incoming>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public IList<Payment> Payments { get; set; }
        public IList<Incoming> Incomings { get; set; }
    }
}
