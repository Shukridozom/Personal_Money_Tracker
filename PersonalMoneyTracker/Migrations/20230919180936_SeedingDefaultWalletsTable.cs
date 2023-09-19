using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalMoneyTracker.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDefaultWalletsTable : Migration
    {
        private readonly string _tableName = "DefaultWallets";
        List<string> defaultWallets = new List<string>() { "Cash", "Payment Card"};
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            foreach(var wallet in defaultWallets)
                migrationBuilder.Sql($"INSERT INTO {_tableName} (Name) VALUES ('{wallet}')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"DELETE FROM {_tableName}");
        }
    }
}
