using Microsoft.EntityFrameworkCore;
using PersonalMoneyTracker.Core.Repositories;
using PersonalMoneyTracker.Persistence.Repositories;

namespace PersonalMoneyTracker.Persistence
{
    public static class DbContextExtensions
    {
        public static IServiceCollection AddAppContext(
            this IServiceCollection services,
            IConfiguration config)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddDbContext<AppDbContext>(opt => opt
                .UseMySQL(config.GetConnectionString("MySQL")));

            return services;
        }
    }
}
