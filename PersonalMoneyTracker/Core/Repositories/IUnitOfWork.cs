namespace PersonalMoneyTracker.Core.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        ITransactionRepository Transactions { get; }
        ITransactionTypeRepository TransactionTypes { get; }
        ITransactionCategoryRepository TransactionCategories { get; }
        IWalletRepository Wallets { get; }

        void Complete();
    }
}
