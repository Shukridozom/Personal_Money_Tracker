using Microsoft.EntityFrameworkCore;
using PersonalMoneyTracker.Models;

namespace PersonalMoneyTracker
{
    public class AppContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<TransactionCategory> PaymentCategories { get; set; }
        public DbSet<Transaction> Payments { get; set; }
        public DbSet<TransactionType> TransactionTypes { get; set; }

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
                .HasMany(u => u.PaymentCategories)
                .WithOne(i => i.User)
                .HasForeignKey(i => i.UserId);


            modelBuilder.Entity<Wallet>()
                .Property(w => w.Name)
                .HasMaxLength(128);

            modelBuilder.Entity<TransactionCategory>()
                .ToTable("TransactionCategories");

            modelBuilder.Entity<TransactionCategory>()
                .Property(i => i.Name)
                .HasMaxLength(128);

            modelBuilder.Entity<Transaction>()
                .ToTable("Transactions");

            modelBuilder.Entity<Transaction>()
                .Property(p => p.Reason)
                .HasMaxLength(255);

            modelBuilder.Entity<Transaction>()
                .HasOne(p => p.User)
                .WithMany(u => u.Payments)
                .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<Transaction>()
                .HasOne(p => p.Wallet)
                .WithMany(w => w.Payments)
                .HasForeignKey(p => p.WalletId);

            modelBuilder.Entity<Transaction>()
                .HasOne(p => p.PaymentCategory)
                .WithMany(pc => pc.Payments)
                .HasForeignKey(p => p.PaymentCategoryId);


            modelBuilder.Entity<TransactionType>()
                .Property(t => t.Name)
                .HasMaxLength(64);

            modelBuilder.Entity<TransactionType>()
                .HasMany(tt => tt.Transactions)
                .WithOne(t => t.TransactionType)
                .HasForeignKey(t => t.TransactionTypeId);

            modelBuilder.Entity<TransactionType>()
                .HasMany(tt => tt.TransactionCategories)
                .WithOne(tc => tc.TransactionType)
                .HasForeignKey(tc => tc.TransactionTypeId);

        }
    }
}
