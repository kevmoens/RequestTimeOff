using Microsoft.EntityFrameworkCore.Migrations;

namespace RequestTimeOff.Migrations
{
    public partial class TimeOffRangeAndManager : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<string>(
                name: "Manager",
                table: "TimeOffs",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Range",
                table: "TimeOffs",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Manager",
                table: "TimeOffs");

            migrationBuilder.DropColumn(
                name: "Range",
                table: "TimeOffs");

        }
    }
}
