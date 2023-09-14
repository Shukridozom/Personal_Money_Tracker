﻿namespace PersonalMoneyTracker.Models
{
    public class Wallet
    {
        public Wallet()
        {
            Payments = new List<Transaction>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public IList<Transaction> Payments { get; set; }
    }
}
