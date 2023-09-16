using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalMoneyTracker.Migrations
{
    /// <inheritdoc />
    public partial class CreateCompositeIndexForTransactionCategoriesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "TransactionCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TransactionCategories_UserId_TransactionTypeId_Name",
                table: "TransactionCategories",
                columns: new[] { "UserId", "TransactionTypeId", "Name" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TransactionCategories_UserId_TransactionTypeId_Name",
                table: "TransactionCategories");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TransactionCategories");
        }
    }
}
