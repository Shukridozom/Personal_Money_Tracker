using PersonalMoneyTracker.Core.Models;
using PersonalMoneyTracker.Persistence.Repositories;

namespace PersonalMoneyTracker.Core.Repositories
{
    public interface IWalletRepository : IRepository<Wallet>
    {
        IEnumerable<Wallet> GetUserWallets(int userId);
        Wallet GetWalletWithTransactions(int walletId);

        IEnumerable<int> GetUserWalletIds(int userId);

        double GetWalletCarryOver(int walletId, DateTime day);

        double GetWalletBalance(int walletId);
    }
}
