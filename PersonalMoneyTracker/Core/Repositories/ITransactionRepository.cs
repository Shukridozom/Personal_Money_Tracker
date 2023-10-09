using PersonalMoneyTracker.Core.Models;

namespace PersonalMoneyTracker.Core.Repositories
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        IEnumerable<Transaction> GetUserTransactions(int userId);

        double GetTransactionsCarryOver(int userId, DateTime day);

        double GetTransactionsBalance(int userId);
        IEnumerable<Transaction> GetTransactionsBetweenTwoDays(int userId, DateTime from, DateTime to);
    }
}
