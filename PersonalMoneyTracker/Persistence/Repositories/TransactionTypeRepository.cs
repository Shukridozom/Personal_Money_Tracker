using Microsoft.EntityFrameworkCore;
using PersonalMoneyTracker.Core.Models;
using PersonalMoneyTracker.Core.Repositories;

namespace PersonalMoneyTracker.Persistence.Repositories
{
    public class TransactionTypeRepository : Repository<TransactionType>, ITransactionTypeRepository
    {

        public TransactionTypeRepository(DbContext context)
            :base(context)
        {

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
