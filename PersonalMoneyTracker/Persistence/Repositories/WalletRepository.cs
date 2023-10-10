using Microsoft.EntityFrameworkCore;
using PersonalMoneyTracker.Core.Models;
using PersonalMoneyTracker.Core.Repositories;

namespace PersonalMoneyTracker.Persistence.Repositories
{
    public class WalletRepository : Repository<Wallet>, IWalletRepository
    {
        public WalletRepository(DbContext context)
            :base(context)
        {

        }

        public IEnumerable<Wallet> GetUserWallets(int userId)
        {
            return AppDbContext.Wallets.Where(w => w.UserId == userId).ToList();
        }

        public Wallet GetWalletWithTransactions(int walletId)
        {
            return AppDbContext.Wallets.Include(w => w.Transactions).SingleOrDefault(w => w.Id == walletId);
        }

        public IEnumerable<int> GetUserWalletIds(int userId)
        {
            return AppDbContext.Wallets
                    .Where(w => w.UserId == userId)
                    .Select(w => w.Id)
                    .ToList();
        }
        public double GetWalletCarryOver(int walletId, DateTime day)
        {
            double carryOver = 0;
            var transactions = AppDbContext.Transactions.
                Include(t => t.TransactionCategory)
                .Where(t => t.WalletId == walletId && t.Date < day.Date)
                .ToList();

            foreach(var t in transactions)
            {
                if (t.TransactionCategory.TransactionTypeId == 1) // Income
                    carryOver += t.Amount;
                else // Payment
                    carryOver -= t.Amount;
            }

            return carryOver;
        }

        public double GetWalletBalance(int walletId)
        {
            double balance = 0;
            var transactions = AppDbContext.Transactions.
                Include(t => t.TransactionCategory)
                .Where(t => t.WalletId == walletId)
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

        public AppDbContext AppDbContext
        {
            get
            {
                return Context as AppDbContext;
            }
        }
    }
}
