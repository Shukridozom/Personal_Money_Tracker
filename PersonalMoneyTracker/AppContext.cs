using Microsoft.EntityFrameworkCore;
using PersonalMoneyTracker.Models;

namespace PersonalMoneyTracker
{
    public class AppContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<TransactionCategory> TransactionCategories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionType> TransactionTypes { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = @"Server=localhost;Port=3306;Database=Personal_Money_Tracker;Uid=root;Pwd=testpassword;";
            optionsBuilder.UseMySQL(connectionString);
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


            modelBuilder.Entity<TransactionCategory>()
                .Property(i => i.Name)
                .HasMaxLength(128);


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
                .HasForeignKey(t => t.WalletId);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.TransactionCategory)
                .WithMany(tc => tc.Transactions)
                .HasForeignKey(t => t.TransactionCategoryId);


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
