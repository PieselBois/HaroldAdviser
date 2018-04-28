using Microsoft.EntityFrameworkCore.Migrations;

namespace HaroldAdviser.Migrations
{
    public partial class ChangeTokenToApiKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Token",
                table: "Repositories",
                newName: "ApiKey");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ApiKey",
                table: "Repositories",
                newName: "Token");
        }
    }
}
