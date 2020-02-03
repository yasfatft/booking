using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace booking.Migrations
{
    public partial class BookingAppDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "salon",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    SeatWidth = table.Column<int>(nullable: false),
                    SeatHeight = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_salon", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "seat",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SalonId = table.Column<int>(nullable: false),
                    X = table.Column<int>(nullable: false),
                    Y = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_seat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_seat_salon_SalonId",
                        column: x => x.SalonId,
                        principalTable: "salon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "show",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    Summary = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Price = table.Column<int>(nullable: false),
                    SalonId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_show", x => x.Id);
                    table.ForeignKey(
                        name: "FK_show_salon_SalonId",
                        column: x => x.SalonId,
                        principalTable: "salon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ticket",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(nullable: false),
                    SeatId = table.Column<int>(nullable: false),
                    ShowId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ticket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ticket_seat_SeatId",
                        column: x => x.SeatId,
                        principalTable: "seat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ticket_show_ShowId",
                        column: x => x.ShowId,
                        principalTable: "show",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_seat_SalonId",
                table: "seat",
                column: "SalonId");

            migrationBuilder.CreateIndex(
                name: "IX_show_SalonId",
                table: "show",
                column: "SalonId");

            migrationBuilder.CreateIndex(
                name: "IX_ticket_SeatId",
                table: "ticket",
                column: "SeatId");

            migrationBuilder.CreateIndex(
                name: "IX_ticket_ShowId",
                table: "ticket",
                column: "ShowId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ticket");

            migrationBuilder.DropTable(
                name: "seat");

            migrationBuilder.DropTable(
                name: "show");

            migrationBuilder.DropTable(
                name: "salon");
        }
    }
}
