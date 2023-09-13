using Microsoft.EntityFrameworkCore;

namespace PersonalMoneyTracker
{
    public class AppContext : DbContext
    {

        private readonly IConfiguration _config;
        public AppContext(IConfiguration config)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(_config["ConnectionStrings:Url"]);
        }
    }
}
