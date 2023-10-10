using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalMoneyTracker.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureRelationshipsForTransactionTypesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TransactionTypeId",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TransactionTypeId",
                table: "TransactionCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_TransactionTypeId",
                table: "Transactions",
                column: "TransactionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionCategories_TransactionTypeId",
                table: "TransactionCategories",
                column: "TransactionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionCategories_TransactionTypes_TransactionTypeId",
                table: "TransactionCategories",
                column: "TransactionTypeId",
                principalTable: "TransactionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_TransactionTypes_TransactionTypeId",
                table: "Transactions",
                column: "TransactionTypeId",
                principalTable: "TransactionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionCategories_TransactionTypes_TransactionTypeId",
                table: "TransactionCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_TransactionTypes_TransactionTypeId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_TransactionTypeId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_TransactionCategories_TransactionTypeId",
                table: "TransactionCategories");

            migrationBuilder.DropColumn(
                name: "TransactionTypeId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TransactionTypeId",
                table: "TransactionCategories");
        }
    }
}
