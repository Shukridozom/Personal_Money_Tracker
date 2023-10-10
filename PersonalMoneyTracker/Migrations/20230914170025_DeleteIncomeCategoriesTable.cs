using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace PersonalMoneyTracker.Migrations
{
    /// <inheritdoc />
    public partial class DeleteIncomeCategoriesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Incomings_IncomeCategories_IncomeCategoryId",
                table: "Incomings");

            migrationBuilder.DropTable(
                name: "IncomeCategories");

            migrationBuilder.DropIndex(
                name: "IX_Incomings_IncomeCategoryId",
                table: "Incomings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IncomeCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IncomeCategories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Incomings_IncomeCategoryId",
                table: "Incomings",
                column: "IncomeCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_IncomeCategories_UserId",
                table: "IncomeCategories",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Incomings_IncomeCategories_IncomeCategoryId",
                table: "Incomings",
                column: "IncomeCategoryId",
                principalTable: "IncomeCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
