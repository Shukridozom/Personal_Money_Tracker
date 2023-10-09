using Microsoft.EntityFrameworkCore;
using PersonalMoneyTracker.Core.Models;
using PersonalMoneyTracker.Core.Repositories;

namespace PersonalMoneyTracker.Persistence.Repositories
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {

        public TransactionRepository(DbContext context)
            :base(context)
        {

        }

        public IEnumerable<Transaction> GetUserTransactions(int userId)
        {
            return AppDbContext.Transactions.Where(w => w.UserId == userId).ToList();
        }

        public double GetTransactionsCarryOver(int userId, DateTime day)
        {
            double carryOver = 0;
            var transactions = AppDbContext.Transactions.
                Include(t => t.TransactionCategory)
                .Where(t => t.UserId == userId && t.Date < day.Date)
                .ToList();

            foreach (var t in transactions)
            {
                if (t.TransactionCategory.TransactionTypeId == 1) // Income
                    carryOver += t.Amount;
                else // Payment
                    carryOver -= t.Amount;
            }

            return carryOver;
        }

        public double GetTransactionsBalance(int userId)
        {
            double balance = 0;
            var transactions = AppDbContext.Transactions.
                Include(t => t.TransactionCategory)
                .Where(t => t.UserId == userId)
                .ToList();

            foreach (var t in transactions)
            {
                if (t.TransactionCategory.TransactionTypeId == 1) // Income
                    balance += t.Amount;
                else // Payment
                    balance -= t.Amount;
            }

            return balance;
        }

        public IEnumerable<Transaction> GetTransactionsBetweenTwoDays(int userId, DateTime from, DateTime to)
        {
            from = from.Date;
            to = to.Date;

            return AppDbContext.Transactions
                .Where(t => t.UserId == userId && t.Date >= from && t.Date <= to)
                .ToList();
        }

        public IEnumerable<Transaction> GetWalletTransactions(int walletId)
        {
            return AppDbContext.Transactions
                .Where(t => t.WalletId == walletId)
                .ToList();
        }

        public IEnumerable<Transaction> GetWalletTransactionsBetweenTwoDays(int walletId, DateTime from, DateTime to)
        {
            from = from.Date;
            to = to.Date;

            return AppDbContext.Transactions
                .Where(t => t.WalletId == walletId && t.Date >= from && t.Date <= to)
                .ToList();
        }

        public AppDbContext AppDbContext
        {
            get
            {
                return Context as AppDbContext;
            }
        }
    }
}
