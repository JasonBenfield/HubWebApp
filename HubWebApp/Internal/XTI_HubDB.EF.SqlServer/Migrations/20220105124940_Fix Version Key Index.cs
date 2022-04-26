using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XTI_HubDB.EF.SqlServer
{
    public partial class FixVersionKeyIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Versions_AppID",
                table: "Versions");

            migrationBuilder.DropIndex(
                name: "IX_Versions_VersionKey",
                table: "Versions");

            migrationBuilder.CreateIndex(
                name: "IX_Versions_AppID_VersionKey",
                table: "Versions",
                columns: new[] { "AppID", "VersionKey" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Versions_AppID_VersionKey",
                table: "Versions");

            migrationBuilder.CreateIndex(
                name: "IX_Versions_AppID",
                table: "Versions",
                column: "AppID");

            migrationBuilder.CreateIndex(
                name: "IX_Versions_VersionKey",
                table: "Versions",
                column: "VersionKey",
                unique: true);
        }
    }
}
