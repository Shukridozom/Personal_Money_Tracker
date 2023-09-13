namespace PersonalMoneyTracker.Models
{
    public class PaymentCategory
    {
        public PaymentCategory()
        {
            Payments = new List<Payment>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public IList<Payment> Payments { get; set; }
    }
}
