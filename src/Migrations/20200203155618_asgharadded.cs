using Microsoft.EntityFrameworkCore.Migrations;

namespace booking.Migrations
{
    public partial class asgharadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "asghar",
                table: "seat",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "asghar",
                table: "seat");
        }
    }
}
