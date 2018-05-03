using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HaroldAdviser.Migrations
{
    public partial class AddTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Log_Pipelines_PipelineId",
                table: "Log");

            migrationBuilder.DropForeignKey(
                name: "FK_Repositories_RepositorySettings_SettingsId",
                table: "Repositories");

            migrationBuilder.DropForeignKey(
                name: "FK_Warning_Pipelines_PipelineId",
                table: "Warning");

            migrationBuilder.DropForeignKey(
                name: "FK_Warning_Repositories_RepositoryId",
                table: "Warning");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Warning",
                table: "Warning");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RepositorySettings",
                table: "RepositorySettings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Log",
                table: "Log");

            migrationBuilder.RenameTable(
                name: "Warning",
                newName: "Warnings");

            migrationBuilder.RenameTable(
                name: "RepositorySettings",
                newName: "Settings");

            migrationBuilder.RenameTable(
                name: "Log",
                newName: "Logs");

            migrationBuilder.RenameIndex(
                name: "IX_Warning_RepositoryId",
                table: "Warnings",
                newName: "IX_Warnings_RepositoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Warning_PipelineId",
                table: "Warnings",
                newName: "IX_Warnings_PipelineId");

            migrationBuilder.RenameIndex(
                name: "IX_Log_PipelineId",
                table: "Logs",
                newName: "IX_Logs_PipelineId");

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Logs",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Warnings",
                table: "Warnings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Settings",
                table: "Settings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Logs",
                table: "Logs",
                column: "Type");

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_Pipelines_PipelineId",
                table: "Logs",
                column: "PipelineId",
                principalTable: "Pipelines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Repositories_Settings_SettingsId",
                table: "Repositories",
                column: "SettingsId",
                principalTable: "Settings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Warnings_Pipelines_PipelineId",
                table: "Warnings",
                column: "PipelineId",
                principalTable: "Pipelines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Warnings_Repositories_RepositoryId",
                table: "Warnings",
                column: "RepositoryId",
                principalTable: "Repositories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logs_Pipelines_PipelineId",
                table: "Logs");

            migrationBuilder.DropForeignKey(
                name: "FK_Repositories_Settings_SettingsId",
                table: "Repositories");

            migrationBuilder.DropForeignKey(
                name: "FK_Warnings_Pipelines_PipelineId",
                table: "Warnings");

            migrationBuilder.DropForeignKey(
                name: "FK_Warnings_Repositories_RepositoryId",
                table: "Warnings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Warnings",
                table: "Warnings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Settings",
                table: "Settings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Logs",
                table: "Logs");

            migrationBuilder.RenameTable(
                name: "Warnings",
                newName: "Warning");

            migrationBuilder.RenameTable(
                name: "Settings",
                newName: "RepositorySettings");

            migrationBuilder.RenameTable(
                name: "Logs",
                newName: "Log");

            migrationBuilder.RenameIndex(
                name: "IX_Warnings_RepositoryId",
                table: "Warning",
                newName: "IX_Warning_RepositoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Warnings_PipelineId",
                table: "Warning",
                newName: "IX_Warning_PipelineId");

            migrationBuilder.RenameIndex(
                name: "IX_Logs_PipelineId",
                table: "Log",
                newName: "IX_Log_PipelineId");

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Log",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Warning",
                table: "Warning",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RepositorySettings",
                table: "RepositorySettings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Log",
                table: "Log",
                column: "Type");

            migrationBuilder.AddForeignKey(
                name: "FK_Log_Pipelines_PipelineId",
                table: "Log",
                column: "PipelineId",
                principalTable: "Pipelines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Repositories_RepositorySettings_SettingsId",
                table: "Repositories",
                column: "SettingsId",
                principalTable: "RepositorySettings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Warning_Pipelines_PipelineId",
                table: "Warning",
                column: "PipelineId",
                principalTable: "Pipelines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Warning_Repositories_RepositoryId",
                table: "Warning",
                column: "RepositoryId",
                principalTable: "Repositories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
