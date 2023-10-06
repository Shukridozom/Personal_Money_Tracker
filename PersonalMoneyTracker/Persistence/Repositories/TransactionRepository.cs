using Microsoft.EntityFrameworkCore;
using PersonalMoneyTracker.Core.Models;
using PersonalMoneyTracker.Core.Repositories;

namespace PersonalMoneyTracker.Persistence.Repositories
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {

        public TransactionRepository(DbContext context)
            :base(context)
        {

        }

        public IEnumerable<Transaction> GetUserTransactions(int userId)
        {
            return AppDbContext.Transactions.Where(w => w.UserId == userId).ToList();
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
