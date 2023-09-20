using Microsoft.EntityFrameworkCore.Migrations;
using Org.BouncyCastle.Security;

#nullable disable

namespace PersonalMoneyTracker.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDefaultTransactionTypesTable : Migration
    {
        int transactionTypeId_Income, transactionTypeId_Payment;
        List<string> defaultIncomeCategories = new List<string>() {"Transaction", "Carry Over", "Salary", "Savings"};
        List<string> defaultPaymentCategories = new List<string>() {"Transaction", "Carry Over", "House", "Food", "Transport", "Sports"};
        string _tableName = "DefaultTransactionCategories";

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            using (var context = new AppDbContext())
            {
                transactionTypeId_Income = context.TransactionTypes.Single(tc => tc.Name == "Income").Id;
                transactionTypeId_Payment = context.TransactionTypes.Single(tc => tc.Name == "Payment").Id;
            }

            foreach (var category in defaultIncomeCategories)
                migrationBuilder.Sql($"INSERT INTO {_tableName} (Name, TransactionTypeId) VALUES ('{category}', {transactionTypeId_Income})");

            foreach (var category in defaultPaymentCategories)
                migrationBuilder.Sql($"INSERT INTO {_tableName} (Name, TransactionTypeId) VALUES ('{category}', {transactionTypeId_Payment})");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"DELETE FROM {_tableName}");
        }
    }
}
