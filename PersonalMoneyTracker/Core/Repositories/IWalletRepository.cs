﻿using PersonalMoneyTracker.Core.Models;
using PersonalMoneyTracker.Persistence.Repositories;

namespace PersonalMoneyTracker.Core.Repositories
{
    public interface IWalletRepository : IRepository<Wallet>
    {
        IEnumerable<Wallet> GetUserWallets(int userId);
    }
}
