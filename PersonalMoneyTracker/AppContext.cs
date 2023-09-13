using Microsoft.EntityFrameworkCore;
using PersonalMoneyTracker.Models;

namespace PersonalMoneyTracker
{
    public class AppContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<IncomeCategory> IncomeCategories { get; set; }
        public DbSet<PaymentCategory> PaymentCategories { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Incoming> Incomings { get; set; }

        private readonly IConfiguration _config;
        public AppContext(IConfiguration config)
        {
            _config = config;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(_config["ConnectionStrings:Url"]);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>()
                .Property(u => u.Username)
                .HasMaxLength(255);

            modelBuilder.Entity<User>()
                .Property(u => u.PasswordHash)
                .HasMaxLength(128);

            modelBuilder.Entity<User>()
                .HasIndex(c => c.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasMany(u => u.Wallets)
                .WithOne(w => w.User)
                .HasForeignKey(w => w.UserId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.IncomeCategories)
                .WithOne(i => i.User)
                .HasForeignKey(i => i.UserId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.PaymentCategories)
                .WithOne(i => i.User)
                .HasForeignKey(i => i.UserId);


            modelBuilder.Entity<Wallet>()
                .Property(w => w.Name)
                .HasMaxLength(128);


            modelBuilder.Entity<IncomeCategory>()
                .Property(i => i.Name)
                .HasMaxLength(128);

            modelBuilder.Entity<PaymentCategory>()
                .Property(i => i.Name)
                .HasMaxLength(128);

            modelBuilder.Entity<Payment>()
                .Property(p => p.Reason)
                .HasMaxLength(255);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.User)
                .WithMany(u => u.Payments)
                .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Wallet)
                .WithMany(w => w.Payments)
                .HasForeignKey(p => p.WalletId);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.PaymentCategory)
                .WithMany(pc => pc.Payments)
                .HasForeignKey(p => p.PaymentCategoryId);


            modelBuilder.Entity<Incoming>()
                .Property(i => i.Reason)
                .HasMaxLength(255);

            modelBuilder.Entity<Incoming>()
                .HasOne(i => i.User)
                .WithMany(u => u.Incomings)
                .HasForeignKey(i => i.UserId);

            modelBuilder.Entity<Incoming>()
                .HasOne(i => i.Wallet)
                .WithMany(w => w.Incomings)
                .HasForeignKey(i => i.WalletId);

            modelBuilder.Entity<Incoming>()
                .HasOne(i => i.IncomeCategory)
                .WithMany(ic => ic.Incomings)
                .HasForeignKey(i => i.IncomeCategoryId);
        }
    }
}
