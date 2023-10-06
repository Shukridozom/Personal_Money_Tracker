using PersonalMoneyTracker.Core.Models;

namespace PersonalMoneyTracker.Core.Repositories
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        IEnumerable<Transaction> GetUserTransactions(int userId);
    }
}
