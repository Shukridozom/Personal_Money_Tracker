using Microsoft.EntityFrameworkCore;
using PersonalMoneyTracker.Core.Models;
using PersonalMoneyTracker.Core.Repositories;

namespace PersonalMoneyTracker.Persistence.Repositories
{
    public class TransactionCategoryRepository : Repository<TransactionCategory>, ITransactionCategoryRepository
    {
        public TransactionCategoryRepository(DbContext context)
            :base(context)
        {

        }

        public IEnumerable<TransactionCategory> GetUserTransactionCategories(int userId)
        {
            return AppDbContext.TransactionCategories
                .Where(tc => tc.UserId == userId)
                .ToList();
        }

        public IEnumerable<TransactionCategory> GetUserPaymentTransactionCategories(int userId)
        {
            return AppDbContext.TransactionCategories
                .Where(tc => tc.UserId == userId && tc.TransactionTypeId == 2) // Payment Transaction Type Id
                .ToList();
        }
        public IEnumerable<TransactionCategory> GetUserIncomeTransactionCategories(int userId)
        {
            return AppDbContext.TransactionCategories
                .Where(tc => tc.UserId == userId && tc.TransactionTypeId == 1) // Income Transaction Type Id
                .ToList();
        }

        public TransactionCategory GetTransactionCategoryWithTransactions(int id)
        {
            return AppDbContext.TransactionCategories
                .Include(tc => tc.Transactions)
                .SingleOrDefault(tc => tc.Id == id);
        }

        public IEnumerable<int> GetUserTransactionCategoryIds(int userId)
        {
            return AppDbContext.TransactionCategories
                    .Where(tc => tc.UserId == userId)
                    .Select(tc => tc.Id)
                    .ToList();
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
