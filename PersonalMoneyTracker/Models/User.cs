namespace PersonalMoneyTracker.Models
{
    public class User
    {
        public User()
        {
            Wallets= new List<Wallet>();
            IncomeCategories = new List<IncomeCategory>();
            PaymentCategories = new List<PaymentCategory>();
            Payments = new List<Payment>();
            Incomings = new List<Incoming>();
        }
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public DateTime RegisterDate { get; set; }
        public IList<Wallet> Wallets { get; set; }
        public IList<IncomeCategory> IncomeCategories { get; set; }
        public IList<PaymentCategory> PaymentCategories { get; set; }
        public IList<Payment> Payments { get; set; }
        public IList<Incoming> Incomings { get; set; }
    }
}
