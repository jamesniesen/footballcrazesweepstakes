using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballCrazeSweepstakes.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePurchaseETicket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "SweepstakePurchaseETicket",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Year",
                table: "SweepstakePurchaseETicket");
        }
    }
}
