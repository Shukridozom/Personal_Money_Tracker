﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalMoneyTracker.Migrations
{
    /// <inheritdoc />
    public partial class RestrictDeletingTransactionsWhenDeletingWallet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Wallets_WalletId",
                table: "Transactions");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Wallets_WalletId",
                table: "Transactions",
                column: "WalletId",
                principalTable: "Wallets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Wallets_WalletId",
                table: "Transactions");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Wallets_WalletId",
                table: "Transactions",
                column: "WalletId",
                principalTable: "Wallets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
