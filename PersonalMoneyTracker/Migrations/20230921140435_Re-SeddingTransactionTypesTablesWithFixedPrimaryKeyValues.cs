using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalMoneyTracker.Migrations
{
    /// <inheritdoc />
    public partial class ReSeddingTransactionTypesTablesWithFixedPrimaryKeyValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM TransactionTypes");

            migrationBuilder.Sql("INSERT INTO TransactionTypes(Id, Name) VALUES(1, 'Income')");
            migrationBuilder.Sql("INSERT INTO TransactionTypes(Id, Name) VALUES(2, 'Payment')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM TransactionTypes");

            migrationBuilder.Sql("INSERT INTO TransactionTypes(Name) VALUES('Income')");
            migrationBuilder.Sql("INSERT INTO TransactionTypes(Name) VALUES('Payment')");
        }
    }
}
