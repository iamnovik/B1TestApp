using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace B1TestApp.Migrations
{
    /// <inheritdoc />
    public partial class Banks_ReportDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankAccounts_Files_FileId",
                table: "BankAccounts");

            migrationBuilder.RenameColumn(
                name: "FileId",
                table: "BankAccounts",
                newName: "BankId");

            migrationBuilder.RenameIndex(
                name: "IX_BankAccounts_FileId",
                table: "BankAccounts",
                newName: "IX_BankAccounts_BankId");

            migrationBuilder.AddColumn<long>(
                name: "BankAccountNumber",
                table: "BankAccounts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ReportDate",
                table: "BankAccounts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.CreateTable(
                name: "Bank",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    FileId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bank", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bank_Files_FileId",
                        column: x => x.FileId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bank_FileId",
                table: "Bank",
                column: "FileId");

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccounts_Bank_BankId",
                table: "BankAccounts",
                column: "BankId",
                principalTable: "Bank",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankAccounts_Bank_BankId",
                table: "BankAccounts");

            migrationBuilder.DropTable(
                name: "Bank");

            migrationBuilder.DropColumn(
                name: "BankAccountNumber",
                table: "BankAccounts");

            migrationBuilder.DropColumn(
                name: "ReportDate",
                table: "BankAccounts");

            migrationBuilder.RenameColumn(
                name: "BankId",
                table: "BankAccounts",
                newName: "FileId");

            migrationBuilder.RenameIndex(
                name: "IX_BankAccounts_BankId",
                table: "BankAccounts",
                newName: "IX_BankAccounts_FileId");

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccounts_Files_FileId",
                table: "BankAccounts",
                column: "FileId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
