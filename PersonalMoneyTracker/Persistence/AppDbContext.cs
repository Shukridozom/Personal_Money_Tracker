using Microsoft.EntityFrameworkCore;
using PersonalMoneyTracker.Core.Models;

namespace PersonalMoneyTracker.Persistence
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _config;

        public DbSet<User> Users { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<TransactionCategory> TransactionCategories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionType> TransactionTypes { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration config)
            : base(options)
        {
            _config = config;
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
                .HasMany(u => u.TransactionCategories)
                .WithOne(tc => tc.User)
                .HasForeignKey(tc => tc.UserId);


            modelBuilder.Entity<Wallet>()
                .Property(w => w.Name)
                .HasMaxLength(128);

            modelBuilder.Entity<Wallet>()
                .HasIndex(w => new { w.UserId, w.Name })
                .IsUnique();


            modelBuilder.Entity<TransactionCategory>()
                .Property(i => i.Name)
                .HasMaxLength(128);

            modelBuilder.Entity<TransactionCategory>()
                .HasIndex(tc => new { tc.UserId, tc.TransactionTypeId, tc.Name })
                .IsUnique();

            modelBuilder.Entity<Transaction>()
                .Property(p => p.Reason)
                .HasMaxLength(255);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.User)
                .WithMany(u => u.Transactions)
                .HasForeignKey(t => t.UserId);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Wallet)
                .WithMany(w => w.Transactions)
                .HasForeignKey(t => t.WalletId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.TransactionCategory)
                .WithMany(tc => tc.Transactions)
                .HasForeignKey(t => t.TransactionCategoryId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<TransactionType>()
                .Property(t => t.Name)
                .HasMaxLength(64);


            modelBuilder.Entity<TransactionType>()
                .HasMany(tt => tt.TransactionCategories)
                .WithOne(tc => tc.TransactionType)
                .HasForeignKey(tc => tc.TransactionTypeId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
