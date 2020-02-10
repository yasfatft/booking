using Microsoft.EntityFrameworkCore.Migrations;

namespace booking.Migrations
{
    public partial class asgharremoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "asghar",
                table: "seat");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "asghar",
                table: "seat",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
