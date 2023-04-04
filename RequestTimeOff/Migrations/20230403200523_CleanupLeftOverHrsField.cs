using Microsoft.EntityFrameworkCore.Migrations;

namespace RequestTimeOff.Migrations
{
    public partial class CleanupLeftOverHrsField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hrs",
                table: "TimeOffs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Hrs",
                table: "TimeOffs",
                nullable: false,
                defaultValue: 0);
        }
    }
}
