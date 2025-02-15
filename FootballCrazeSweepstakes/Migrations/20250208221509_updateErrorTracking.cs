using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballCrazeSweepstakes.Migrations
{
    /// <inheritdoc />
    public partial class updateErrorTracking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Tracked",
                table: "Error",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tracked",
                table: "Error");
        }
    }
}
