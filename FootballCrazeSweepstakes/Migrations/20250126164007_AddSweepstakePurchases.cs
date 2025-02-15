using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballCrazeSweepstakes.Migrations
{
    /// <inheritdoc />
    public partial class AddSweepstakePurchases : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ETicket",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TicketNumber = table.Column<string>(type: "TEXT", maxLength: 25, nullable: false),
                    FileType = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    FileData = table.Column<byte[]>(type: "BLOB", nullable: false),
                    Year = table.Column<int>(type: "INTEGER", nullable: false),
                    Purchased = table.Column<bool>(type: "INTEGER", nullable: false),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Modified = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ETicket", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SweepstakePurchase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    MemberName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    MemberFirstName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    MemberLastName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Purchase = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    PurchaseOption = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    CustomerCharged = table.Column<decimal>(type: "TEXT", nullable: false),
                    Refunded = table.Column<decimal>(type: "TEXT", nullable: false),
                    PaymentMethod = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    TransactionId = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    City = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    State = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Zip = table.Column<string>(type: "TEXT", maxLength: 25, nullable: false),
                    TicketEmailedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SweepstakePurchase", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SweepstakePurchaseETicket",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SweepstakePurchaseId = table.Column<int>(type: "INTEGER", nullable: false),
                    ETicketId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SweepstakePurchaseETicket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SweepstakePurchaseETicket_ETicket_ETicketId",
                        column: x => x.ETicketId,
                        principalTable: "ETicket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SweepstakePurchaseETicket_SweepstakePurchase_SweepstakePurchaseId",
                        column: x => x.SweepstakePurchaseId,
                        principalTable: "SweepstakePurchase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SweepstakePurchaseETicket_ETicketId",
                table: "SweepstakePurchaseETicket",
                column: "ETicketId");

            migrationBuilder.CreateIndex(
                name: "IX_SweepstakePurchaseETicket_SweepstakePurchaseId",
                table: "SweepstakePurchaseETicket",
                column: "SweepstakePurchaseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SweepstakePurchaseETicket");

            migrationBuilder.DropTable(
                name: "ETicket");

            migrationBuilder.DropTable(
                name: "SweepstakePurchase");
        }
    }
}
