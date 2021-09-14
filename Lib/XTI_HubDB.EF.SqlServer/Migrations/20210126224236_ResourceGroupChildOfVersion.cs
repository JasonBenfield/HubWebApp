using Microsoft.EntityFrameworkCore.Migrations;

namespace XTI_HubDB.EF.SqlServer
{
    public partial class ResourceGroupChildOfVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from events");
            migrationBuilder.Sql("delete from requests");
            migrationBuilder.Sql("delete from sessions");
            migrationBuilder.Sql("delete from resourceroles");
            migrationBuilder.Sql("delete from resources");
            migrationBuilder.Sql("delete from resourcegrouproles");
            migrationBuilder.Sql("delete from resourcegroups");
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Versions_VersionID",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_ResourceGroups_Apps_AppID",
                table: "ResourceGroups");

            migrationBuilder.DropIndex(
                name: "IX_Requests_VersionID",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "VersionID",
                table: "Requests");

            migrationBuilder.RenameColumn(
                name: "AppID",
                table: "ResourceGroups",
                newName: "VersionID");

            migrationBuilder.RenameIndex(
                name: "IX_ResourceGroups_AppID_Name",
                table: "ResourceGroups",
                newName: "IX_ResourceGroups_VersionID_Name");

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceGroups_Versions_VersionID",
                table: "ResourceGroups",
                column: "VersionID",
                principalTable: "Versions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResourceGroups_Versions_VersionID",
                table: "ResourceGroups");

            migrationBuilder.RenameColumn(
                name: "VersionID",
                table: "ResourceGroups",
                newName: "AppID");

            migrationBuilder.RenameIndex(
                name: "IX_ResourceGroups_VersionID_Name",
                table: "ResourceGroups",
                newName: "IX_ResourceGroups_AppID_Name");

            migrationBuilder.AddColumn<int>(
                name: "VersionID",
                table: "Requests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_VersionID",
                table: "Requests",
                column: "VersionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Versions_VersionID",
                table: "Requests",
                column: "VersionID",
                principalTable: "Versions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceGroups_Apps_AppID",
                table: "ResourceGroups",
                column: "AppID",
                principalTable: "Apps",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
