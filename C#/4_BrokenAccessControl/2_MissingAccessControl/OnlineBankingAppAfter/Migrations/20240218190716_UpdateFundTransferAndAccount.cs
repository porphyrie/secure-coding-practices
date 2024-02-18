using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineBankingApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFundTransferAndAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FundTransfer_AspNetUsers_CustomerId",
                table: "FundTransfer");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "FundTransfer",
                newName: "CustomerID");

            migrationBuilder.RenameIndex(
                name: "IX_FundTransfer_CustomerId",
                table: "FundTransfer",
                newName: "IX_FundTransfer_CustomerID");

            migrationBuilder.AlterColumn<Guid>(
                name: "AccountTo",
                table: "FundTransfer",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<Guid>(
                name: "AccountFrom",
                table: "FundTransfer",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<Guid>(
                name: "ID",
                table: "Account",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddForeignKey(
                name: "FK_FundTransfer_AspNetUsers_CustomerID",
                table: "FundTransfer",
                column: "CustomerID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FundTransfer_AspNetUsers_CustomerID",
                table: "FundTransfer");

            migrationBuilder.RenameColumn(
                name: "CustomerID",
                table: "FundTransfer",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_FundTransfer_CustomerID",
                table: "FundTransfer",
                newName: "IX_FundTransfer_CustomerId");

            migrationBuilder.AlterColumn<int>(
                name: "AccountTo",
                table: "FundTransfer",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "AccountFrom",
                table: "FundTransfer",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "ID",
                table: "Account",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddForeignKey(
                name: "FK_FundTransfer_AspNetUsers_CustomerId",
                table: "FundTransfer",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
