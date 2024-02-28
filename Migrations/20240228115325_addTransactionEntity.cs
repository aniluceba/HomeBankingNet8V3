using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeBankingNet8V3.Migrations
{
    /// <inheritdoc />
    public partial class addTransactionEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Account");

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Transaction",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ClientId",
                table: "Loans",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Cards",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Color",
                table: "Cards",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Loans_ClientId",
                table: "Loans",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Clients_ClientId",
                table: "Loans",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Clients_ClientId",
                table: "Loans");

            migrationBuilder.DropIndex(
                name: "IX_Loans_ClientId",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Loans");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Transaction",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "Loans",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Cards",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "Cards",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<long>(
                name: "AccountId",
                table: "Account",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
