using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalMoneyTracker.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultTransactionCategoriesTable : Migration
    {
        private readonly string _tableName = "DefaultTransactionCategories";
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"CREATE TABLE {_tableName} LIKE TransactionCategories");
            migrationBuilder.Sql($"ALTER TABLE {_tableName} DROP COLUMN UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"DROP TABLE IF EXISTS {_tableName}");
        }
    }
}
