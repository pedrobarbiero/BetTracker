using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bankroll",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    InitialBalance = table.Column<decimal>(type: "decimal(10,4)", nullable: false),
                    CurrentBalance = table.Column<int>(type: "int", nullable: false),
                    StartedAt = table.Column<DateOnly>(type: "date", nullable: false),
                    StandardUnit = table.Column<decimal>(type: "decimal(10,4)", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bankroll", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BettingMarket",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Sport = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BettingMarket", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tipster",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BankrollId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipsterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BettingMarketId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PreMatch = table.Column<bool>(type: "bit", nullable: false),
                    Settled = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    TotalStake = table.Column<decimal>(type: "decimal(10,4)", nullable: false),
                    TotalReturn = table.Column<decimal>(type: "decimal(10,4)", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bets_Bankroll_BankrollId",
                        column: x => x.BankrollId,
                        principalTable: "Bankroll",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bets_BettingMarket_BettingMarketId",
                        column: x => x.BettingMarketId,
                        principalTable: "BettingMarket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bets_Tipster_TipsterId",
                        column: x => x.TipsterId,
                        principalTable: "Tipster",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Pick",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Sport = table.Column<int>(type: "int", nullable: false),
                    Odd = table.Column<decimal>(type: "decimal(10,4)", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pick", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pick_Bets_BetId",
                        column: x => x.BetId,
                        principalTable: "Bets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bets_BankrollId",
                table: "Bets",
                column: "BankrollId");

            migrationBuilder.CreateIndex(
                name: "IX_Bets_BettingMarketId",
                table: "Bets",
                column: "BettingMarketId");

            migrationBuilder.CreateIndex(
                name: "IX_Bets_TipsterId",
                table: "Bets",
                column: "TipsterId");

            migrationBuilder.CreateIndex(
                name: "IX_Pick_BetId",
                table: "Pick",
                column: "BetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pick");

            migrationBuilder.DropTable(
                name: "Bets");

            migrationBuilder.DropTable(
                name: "Bankroll");

            migrationBuilder.DropTable(
                name: "BettingMarket");

            migrationBuilder.DropTable(
                name: "Tipster");
        }
    }
}
