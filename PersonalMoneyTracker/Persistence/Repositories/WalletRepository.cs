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

        public IEnumerable<string> GetUserWalletsNames(int userId)
        {
            return AppDbContext.Wallets.Where(w => w.UserId == userId).Select(w => w.Name).ToList();
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
