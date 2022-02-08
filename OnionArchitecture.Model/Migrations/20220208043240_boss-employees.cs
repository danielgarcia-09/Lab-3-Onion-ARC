using Microsoft.EntityFrameworkCore.Migrations;

namespace OnionArchitecture.Model.Migrations
{
    public partial class bossemployees : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeCount",
                table: "Bosses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeCount",
                table: "Bosses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
