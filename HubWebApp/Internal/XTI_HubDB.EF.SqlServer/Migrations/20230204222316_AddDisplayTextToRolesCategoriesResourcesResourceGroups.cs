using Microsoft.EntityFrameworkCore.Migrations;
using XTI_HubDB.EF.SqlServer.Migrations.Views.V230204;

#nullable disable

namespace XTIHubDB.EF.SqlServer
{
    /// <inheritdoc />
    public partial class AddDisplayTextToRolesCategoriesResourcesResourceGroups : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DisplayText",
                table: "Roles",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DisplayText",
                table: "Resources",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DisplayText",
                table: "ResourceGroups",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ModKeyDisplayText",
                table: "Modifiers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DisplayText",
                table: "ModifierCategories",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.Sql
            (
                @"
update Roles
set DisplayText = Name
where DisplayText = ''
"
            );
            migrationBuilder.Sql
            (
                @"
update Resources
set DisplayText = Name
where DisplayText = ''
"
            );
            migrationBuilder.Sql
            (
                @"
update ResourceGroups
set DisplayText = Name
where DisplayText = ''
"
            );
            migrationBuilder.Sql
            (
                @"
update ModifierCategories
set DisplayText = Name
where DisplayText = ''
"
            );
            migrationBuilder.Sql
            (
                @"
update Modifiers
set ModKeyDisplayText = ModKey
where ModKeyDisplayText = ''
"
            );

            migrationBuilder.Sql(ExpandedRequests.Sql);
            migrationBuilder.Sql(ExpandedLogEntries.Sql);
            migrationBuilder.Sql(ExpandedInstallations.Sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayText",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "DisplayText",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "DisplayText",
                table: "ResourceGroups");

            migrationBuilder.DropColumn(
                name: "ModKeyDisplayText",
                table: "Modifiers");

            migrationBuilder.DropColumn(
                name: "DisplayText",
                table: "ModifierCategories");
        }
    }
}
