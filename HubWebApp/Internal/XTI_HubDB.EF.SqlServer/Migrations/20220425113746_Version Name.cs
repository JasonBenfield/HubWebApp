using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XTI_HubDB.EF.SqlServer
{
    public partial class VersionName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GroupName",
                table: "XtiVersions",
                newName: "VersionName");

            migrationBuilder.RenameIndex(
                name: "IX_XtiVersions_GroupName_VersionKey",
                table: "XtiVersions",
                newName: "IX_XtiVersions_VersionName_VersionKey");

            migrationBuilder.AddColumn<string>(
                name: "VersionName",
                table: "Apps",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.Sql
            (
                @"
update apps
set versionName = 
(
    select top 1 versionName 
    from xtiversions 
    inner join appxtiversions
    on 
        xtiversions.ID = appxtiversions.VersionID
        and apps.ID = appxtiversions.AppID
    order by appxtiversions.id desc
)
where 
exists
(
    select top 1 1
    from appXtiVersions 
    where apps.id = appXtiVersions.AppID
)
"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VersionName",
                table: "Apps");

            migrationBuilder.RenameColumn(
                name: "VersionName",
                table: "XtiVersions",
                newName: "GroupName");

            migrationBuilder.RenameIndex(
                name: "IX_XtiVersions_VersionName_VersionKey",
                table: "XtiVersions",
                newName: "IX_XtiVersions_GroupName_VersionKey");
        }
    }
}
