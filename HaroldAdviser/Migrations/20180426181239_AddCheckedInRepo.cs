using Microsoft.EntityFrameworkCore.Migrations;

namespace HaroldAdviser.Migrations
{
    public partial class AddCheckedInRepo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Checked",
                table: "Repositories",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Checked",
                table: "Repositories");
        }
    }
}
