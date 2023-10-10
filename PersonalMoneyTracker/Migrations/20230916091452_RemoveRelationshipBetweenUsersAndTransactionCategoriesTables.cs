using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalMoneyTracker.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRelationshipBetweenUsersAndTransactionCategoriesTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionCategories_Users_UserId",
                table: "TransactionCategories");

            migrationBuilder.DropIndex(
                name: "IX_TransactionCategories_UserId",
                table: "TransactionCategories");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TransactionCategories");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "TransactionCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TransactionCategories_UserId",
                table: "TransactionCategories",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionCategories_Users_UserId",
                table: "TransactionCategories",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
