using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineBankingApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAccountCustomerFundTransfer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_Customer_CustomerID",
                table: "Account");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerID",
                table: "FundTransfer",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerID",
                table: "Account",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateIndex(
                name: "IX_FundTransfer_CustomerID",
                table: "FundTransfer",
                column: "CustomerID");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_Customer_CustomerID",
                table: "Account");

            migrationBuilder.DropForeignKey(
                name: "FK_FundTransfer_Customer_CustomerID",
                table: "FundTransfer");

            migrationBuilder.DropIndex(
                name: "IX_FundTransfer_CustomerID",
                table: "FundTransfer");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerID",
                table: "FundTransfer",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerID",
                table: "Account",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Account_Customer_CustomerID",
                table: "Account",
                column: "CustomerID",
                principalTable: "Customer",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
