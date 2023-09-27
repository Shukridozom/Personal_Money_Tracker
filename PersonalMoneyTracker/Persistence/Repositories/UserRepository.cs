using Microsoft.EntityFrameworkCore;
using PersonalMoneyTracker.Core.Models;
using PersonalMoneyTracker.Core.Repositories;

namespace PersonalMoneyTracker.Persistence.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext context)
            :base(context)
        {

        }

        public void DeleteUser(int userId)
        {

            var user = AppDbContext.Users.SingleOrDefault(u => u.Id == userId);
            if (user == null)
                return;

            var userTransactions = AppDbContext.Transactions
                .Where(t => t.UserId == userId)
                .ToList();

            var userTransactionCategories = AppDbContext.TransactionCategories
                .Where(tc => tc.UserId == userId)
                .ToList();

            var userWallets = AppDbContext.Wallets
                .Where(w => w.UserId == userId)
                .ToList();

            AppDbContext.Transactions.RemoveRange(userTransactions);
            AppDbContext.TransactionCategories.RemoveRange(userTransactionCategories);
            AppDbContext.Wallets.RemoveRange(userWallets);
            AppDbContext.Users.Remove(user);
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
