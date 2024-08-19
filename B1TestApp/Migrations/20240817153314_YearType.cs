using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace B1TestApp.Migrations
{
    /// <inheritdoc />
    public partial class YearType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankAccounts_Bank_BankId",
                table: "BankAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_IncomingBalances_BankAccounts_BankAccountId",
                table: "IncomingBalances");

            migrationBuilder.DropForeignKey(
                name: "FK_OutcomingBalances_BankAccounts_BankAccountId",
                table: "OutcomingBalances");

            migrationBuilder.DropForeignKey(
                name: "FK_Turnovers_BankAccounts_BankAccountId",
                table: "Turnovers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BankAccounts",
                table: "BankAccounts");

            migrationBuilder.DropColumn(
                name: "ReportDate",
                table: "BankAccounts");

            migrationBuilder.RenameTable(
                name: "BankAccounts",
                newName: "BankAccountsData");

            migrationBuilder.RenameIndex(
                name: "IX_BankAccounts_BankId",
                table: "BankAccountsData",
                newName: "IX_BankAccountsData_BankId");

            migrationBuilder.AddColumn<long>(
                name: "ReportYear",
                table: "BankAccountsData",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BankAccountsData",
                table: "BankAccountsData",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccountsData_Bank_BankId",
                table: "BankAccountsData",
                column: "BankId",
                principalTable: "Bank",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IncomingBalances_BankAccountsData_BankAccountId",
                table: "IncomingBalances",
                column: "BankAccountId",
                principalTable: "BankAccountsData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OutcomingBalances_BankAccountsData_BankAccountId",
                table: "OutcomingBalances",
                column: "BankAccountId",
                principalTable: "BankAccountsData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Turnovers_BankAccountsData_BankAccountId",
                table: "Turnovers",
                column: "BankAccountId",
                principalTable: "BankAccountsData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankAccountsData_Bank_BankId",
                table: "BankAccountsData");

            migrationBuilder.DropForeignKey(
                name: "FK_IncomingBalances_BankAccountsData_BankAccountId",
                table: "IncomingBalances");

            migrationBuilder.DropForeignKey(
                name: "FK_OutcomingBalances_BankAccountsData_BankAccountId",
                table: "OutcomingBalances");

            migrationBuilder.DropForeignKey(
                name: "FK_Turnovers_BankAccountsData_BankAccountId",
                table: "Turnovers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BankAccountsData",
                table: "BankAccountsData");

            migrationBuilder.DropColumn(
                name: "ReportYear",
                table: "BankAccountsData");

            migrationBuilder.RenameTable(
                name: "BankAccountsData",
                newName: "BankAccounts");

            migrationBuilder.RenameIndex(
                name: "IX_BankAccountsData_BankId",
                table: "BankAccounts",
                newName: "IX_BankAccounts_BankId");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ReportDate",
                table: "BankAccounts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddPrimaryKey(
                name: "PK_BankAccounts",
                table: "BankAccounts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccounts_Bank_BankId",
                table: "BankAccounts",
                column: "BankId",
                principalTable: "Bank",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IncomingBalances_BankAccounts_BankAccountId",
                table: "IncomingBalances",
                column: "BankAccountId",
                principalTable: "BankAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OutcomingBalances_BankAccounts_BankAccountId",
                table: "OutcomingBalances",
                column: "BankAccountId",
                principalTable: "BankAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Turnovers_BankAccounts_BankAccountId",
                table: "Turnovers",
                column: "BankAccountId",
                principalTable: "BankAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
