using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalMoneyTracker.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultWalletsTable : Migration
    {
        private readonly string _tableName = "DefaultWallets";
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"CREATE TABLE {_tableName} LIKE Wallets");
            migrationBuilder.Sql($"ALTER TABLE {_tableName} DROP COLUMN UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"DROP TABLE IF EXISTS {_tableName}");
        }
    }
}
