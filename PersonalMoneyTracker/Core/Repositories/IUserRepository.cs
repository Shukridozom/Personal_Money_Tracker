using PersonalMoneyTracker.Core.Models;
using PersonalMoneyTracker.Persistence.Repositories;

namespace PersonalMoneyTracker.Core.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        void DeleteUser(int userId);
    }
}
