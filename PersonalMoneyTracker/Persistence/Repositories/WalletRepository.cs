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
        public AppDbContext AppDbContext
        {
            get
            {
                return Context as AppDbContext;
            }
        }
    }
}
