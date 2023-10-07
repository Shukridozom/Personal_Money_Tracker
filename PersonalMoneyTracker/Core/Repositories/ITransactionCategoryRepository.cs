using PersonalMoneyTracker.Core.Models;

namespace PersonalMoneyTracker.Core.Repositories
{
    public interface ITransactionCategoryRepository : IRepository<TransactionCategory>
    {
        IEnumerable<TransactionCategory> GetUserTransactionCategories(int userId);
        IEnumerable<TransactionCategory> GetUserPaymentTransactionCategories(int userId);
        IEnumerable<TransactionCategory> GetUserIncomeTransactionCategories(int userId);
        TransactionCategory GetTransactionCategoryWithTransactions(int id);

        IEnumerable<int> GetUserTransactionCategoryIds(int userId);
    }
}
