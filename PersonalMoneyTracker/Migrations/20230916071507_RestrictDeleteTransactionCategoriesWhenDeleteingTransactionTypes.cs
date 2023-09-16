using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalMoneyTracker.Migrations
{
    /// <inheritdoc />
    public partial class RestrictDeleteTransactionCategoriesWhenDeleteingTransactionTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionCategories_TransactionTypes_TransactionTypeId",
                table: "TransactionCategories");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionCategories_TransactionTypes_TransactionTypeId",
                table: "TransactionCategories",
                column: "TransactionTypeId",
                principalTable: "TransactionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionCategories_TransactionTypes_TransactionTypeId",
                table: "TransactionCategories");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionCategories_TransactionTypes_TransactionTypeId",
                table: "TransactionCategories",
                column: "TransactionTypeId",
                principalTable: "TransactionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
