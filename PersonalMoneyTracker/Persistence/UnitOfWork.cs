using PersonalMoneyTracker.Core.Models;
using PersonalMoneyTracker.Core.Repositories;
using PersonalMoneyTracker.Persistence.Repositories;
using System.Data;

namespace PersonalMoneyTracker.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Users = new UserRepository(context);
            Transactions = new TransactionRepository(context);
            TransactionTypes = new TransactionTypeRepository(context);
            TransactionCategories = new TransactionCategoryRepository(context);
            Wallets = new WalletRepository(context);
        }

        public IUserRepository Users { get; }
        public ITransactionRepository Transactions { get; }
        public ITransactionTypeRepository TransactionTypes { get; }
        public ITransactionCategoryRepository TransactionCategories { get; }
        public IWalletRepository Wallets { get; }

        public void Complete()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
