using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineBankingApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCustomerAsIdentityUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_Customer_CustomerID",
                table: "Account");

            migrationBuilder.DropForeignKey(
                name: "FK_FundTransfer_Customer_CustomerID",
                table: "FundTransfer");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Customer");

            migrationBuilder.RenameColumn(
                name: "CustomerID",
                table: "FundTransfer",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_FundTransfer_CustomerID",
                table: "FundTransfer",
                newName: "IX_FundTransfer_CustomerId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Customer",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CustomerID",
                table: "Account",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Account_CustomerID",
                table: "Account",
                newName: "IX_Account_CustomerId");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Customer",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "Customer",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "Customer",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "Customer",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "Customer",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "Customer",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "Customer",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "Customer",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Customer",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Customer",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "Customer",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "Customer",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "Customer",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Customer",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Account_Customer_CustomerId",
                table: "Account",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FundTransfer_Customer_CustomerId",
                table: "FundTransfer",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_Customer_CustomerId",
                table: "Account");

            migrationBuilder.DropForeignKey(
                name: "FK_FundTransfer_Customer_CustomerId",
                table: "FundTransfer");

            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Customer");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "FundTransfer",
                newName: "CustomerID");

            migrationBuilder.RenameIndex(
                name: "IX_FundTransfer_CustomerId",
                table: "FundTransfer",
                newName: "IX_FundTransfer_CustomerID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Customer",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Account",
                newName: "CustomerID");

            migrationBuilder.RenameIndex(
                name: "IX_Account_CustomerId",
                table: "Account",
                newName: "IX_Account_CustomerID");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Customer",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Customer",
                type: "TEXT",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_Customer_CustomerID",
                table: "Account",
                column: "CustomerID",
                principalTable: "Customer",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_FundTransfer_Customer_CustomerID",
                table: "FundTransfer",
                column: "CustomerID",
                principalTable: "Customer",
                principalColumn: "ID");
        }
    }
}
