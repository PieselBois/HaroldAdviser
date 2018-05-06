using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace HaroldAdviser.Migrations
{
    public partial class Pipeline_Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Warnings_Repositories_RepositoryId",
                table: "Warnings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Warnings",
                table: "Warnings");

            migrationBuilder.RenameTable(
                name: "Warnings",
                newName: "Warning");

            migrationBuilder.RenameIndex(
                name: "IX_Warnings_RepositoryId",
                table: "Warning",
                newName: "IX_Warning_RepositoryId");

            migrationBuilder.AddColumn<Guid>(
                name: "PipelineId",
                table: "Warning",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SettingsId",
                table: "Repositories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CloneUrl",
                table: "Pipelines",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RepositoryId",
                table: "Pipelines",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Pipelines",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Warning",
                table: "Warning",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Log",
                columns: table => new
                {
                    Type = table.Column<int>(nullable: false),
                    Module = table.Column<string>(nullable: true),
                    PipelineId = table.Column<Guid>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log", x => x.Type);
                    table.ForeignKey(
                        name: "FK_Log_Pipelines_PipelineId",
                        column: x => x.PipelineId,
                        principalTable: "Pipelines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RepositorySettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepositorySettings", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Warning_PipelineId",
                table: "Warning",
                column: "PipelineId");

            migrationBuilder.CreateIndex(
                name: "IX_Repositories_SettingsId",
                table: "Repositories",
                column: "SettingsId");

            migrationBuilder.CreateIndex(
                name: "IX_Pipelines_RepositoryId",
                table: "Pipelines",
                column: "RepositoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Log_PipelineId",
                table: "Log",
                column: "PipelineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pipelines_Repositories_RepositoryId",
                table: "Pipelines",
                column: "RepositoryId",
                principalTable: "Repositories",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pipelines_Repositories_RepositoryId",
                table: "Pipelines");

            migrationBuilder.DropForeignKey(
                name: "FK_Repositories_RepositorySettings_SettingsId",
                table: "Repositories");

            migrationBuilder.DropForeignKey(
                name: "FK_Warning_Pipelines_PipelineId",
                table: "Warning");

            migrationBuilder.DropForeignKey(
                name: "FK_Warning_Repositories_RepositoryId",
                table: "Warning");

            migrationBuilder.DropTable(
                name: "Log");

            migrationBuilder.DropTable(
                name: "RepositorySettings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Warning",
                table: "Warning");

            migrationBuilder.DropIndex(
                name: "IX_Warning_PipelineId",
                table: "Warning");

            migrationBuilder.DropIndex(
                name: "IX_Repositories_SettingsId",
                table: "Repositories");

            migrationBuilder.DropIndex(
                name: "IX_Pipelines_RepositoryId",
                table: "Pipelines");

            migrationBuilder.DropColumn(
                name: "PipelineId",
                table: "Warning");

            migrationBuilder.DropColumn(
                name: "SettingsId",
                table: "Repositories");

            migrationBuilder.DropColumn(
                name: "CloneUrl",
                table: "Pipelines");

            migrationBuilder.DropColumn(
                name: "RepositoryId",
                table: "Pipelines");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Pipelines");

            migrationBuilder.RenameTable(
                name: "Warning",
                newName: "Warnings");

            migrationBuilder.RenameIndex(
                name: "IX_Warning_RepositoryId",
                table: "Warnings",
                newName: "IX_Warnings_RepositoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Warnings",
                table: "Warnings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Warnings_Repositories_RepositoryId",
                table: "Warnings",
                column: "RepositoryId",
                principalTable: "Repositories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
