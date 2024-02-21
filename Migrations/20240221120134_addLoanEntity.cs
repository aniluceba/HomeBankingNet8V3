using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeBankingNet8V3.Migrations
{
    /// <inheritdoc />
    public partial class addLoanEntity : Migration
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "Loans",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AccountId",
                table: "Account",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
