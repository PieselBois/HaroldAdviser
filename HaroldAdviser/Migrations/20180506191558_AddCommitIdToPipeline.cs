using Microsoft.EntityFrameworkCore.Migrations;

namespace HaroldAdviser.Migrations
{
    public partial class AddCommitIdToPipeline : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CommitId",
                table: "Pipelines",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommitId",
                table: "Pipelines");
        }
    }
}
