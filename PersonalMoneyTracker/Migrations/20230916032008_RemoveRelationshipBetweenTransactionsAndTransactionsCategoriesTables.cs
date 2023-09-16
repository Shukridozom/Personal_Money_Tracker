using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalMoneyTracker.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRelationshipBetweenTransactionsAndTransactionsCategoriesTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_TransactionCategories_PaymentCategoryId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_PaymentCategoryId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "PaymentCategoryId",
                table: "Transactions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentCategoryId",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_PaymentCategoryId",
                table: "Transactions",
                column: "PaymentCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_TransactionCategories_PaymentCategoryId",
                table: "Transactions",
                column: "PaymentCategoryId",
                principalTable: "TransactionCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
