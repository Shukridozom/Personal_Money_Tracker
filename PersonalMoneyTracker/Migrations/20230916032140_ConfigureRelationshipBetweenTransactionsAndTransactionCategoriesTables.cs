using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalMoneyTracker.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureRelationshipBetweenTransactionsAndTransactionCategoriesTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TransactionCategoryId",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_TransactionCategoryId",
                table: "Transactions",
                column: "TransactionCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_TransactionCategories_TransactionCategoryId",
                table: "Transactions",
                column: "TransactionCategoryId",
                principalTable: "TransactionCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_TransactionCategories_TransactionCategoryId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_TransactionCategoryId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TransactionCategoryId",
                table: "Transactions");
        }
    }
}
