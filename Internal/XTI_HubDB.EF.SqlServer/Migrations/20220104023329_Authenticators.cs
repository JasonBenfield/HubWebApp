using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XTI_HubDB.EF.SqlServer
{
    public partial class Authenticators : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Versions_VersionKey",
                table: "Versions");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserName",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_SessionKey",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_Roles_AppID_Name",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Resources_GroupID_Name",
                table: "Resources");

            migrationBuilder.DropIndex(
                name: "IX_ResourceGroups_VersionID_Name",
                table: "ResourceGroups");

            migrationBuilder.DropIndex(
                name: "IX_Requests_RequestKey",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Modifiers_CategoryID_ModKey",
                table: "Modifiers");

            migrationBuilder.DropIndex(
                name: "IX_Modifiers_CategoryID_TargetKey",
                table: "Modifiers");

            migrationBuilder.DropIndex(
                name: "IX_ModifierCategories_AppID_Name",
                table: "ModifierCategories");

            migrationBuilder.DropIndex(
                name: "IX_InstallLocations_QualifiedMachineName",
                table: "InstallLocations");

            migrationBuilder.DropIndex(
                name: "IX_Events_EventKey",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Apps_Name",
                table: "Apps");

            migrationBuilder.AlterColumn<string>(
                name: "VersionKey",
                table: "Versions",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Versions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Domain",
                table: "Versions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserAgent",
                table: "Sessions",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SessionKey",
                table: "Sessions",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RequesterKey",
                table: "Sessions",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RemoteAddress",
                table: "Sessions",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Roles",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Resources",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ResourceGroups",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RequestKey",
                table: "Requests",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Path",
                table: "Requests",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TargetKey",
                table: "Modifiers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModKey",
                table: "Modifiers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DisplayText",
                table: "Modifiers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ModifierCategories",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "QualifiedMachineName",
                table: "InstallLocations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "Events",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 5000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EventKey",
                table: "Events",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Detail",
                table: "Events",
                type: "nvarchar(max)",
                maxLength: 32000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 32000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Caption",
                table: "Events",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Apps",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Apps",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Domain",
                table: "Apps",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Authenticators",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authenticators", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Authenticators_Apps_AppID",
                        column: x => x.AppID,
                        principalTable: "Apps",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserAuthenticators",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    AuthenticatorID = table.Column<int>(type: "int", nullable: false),
                    ExternalUserKey = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAuthenticators", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserAuthenticators_Authenticators_AuthenticatorID",
                        column: x => x.AuthenticatorID,
                        principalTable: "Authenticators",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserAuthenticators_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Versions_VersionKey",
                table: "Versions",
                column: "VersionKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_SessionKey",
                table: "Sessions",
                column: "SessionKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_AppID_Name",
                table: "Roles",
                columns: new[] { "AppID", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Resources_GroupID_Name",
                table: "Resources",
                columns: new[] { "GroupID", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResourceGroups_VersionID_Name",
                table: "ResourceGroups",
                columns: new[] { "VersionID", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_RequestKey",
                table: "Requests",
                column: "RequestKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Modifiers_CategoryID_ModKey",
                table: "Modifiers",
                columns: new[] { "CategoryID", "ModKey" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Modifiers_CategoryID_TargetKey",
                table: "Modifiers",
                columns: new[] { "CategoryID", "TargetKey" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ModifierCategories_AppID_Name",
                table: "ModifierCategories",
                columns: new[] { "AppID", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InstallLocations_QualifiedMachineName",
                table: "InstallLocations",
                column: "QualifiedMachineName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_EventKey",
                table: "Events",
                column: "EventKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Apps_Name",
                table: "Apps",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Authenticators_AppID",
                table: "Authenticators",
                column: "AppID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAuthenticators_AuthenticatorID",
                table: "UserAuthenticators",
                column: "AuthenticatorID");

            migrationBuilder.CreateIndex(
                name: "IX_UserAuthenticators_ExternalUserKey_AuthenticatorID",
                table: "UserAuthenticators",
                columns: new[] { "ExternalUserKey", "AuthenticatorID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAuthenticators_UserID_AuthenticatorID",
                table: "UserAuthenticators",
                columns: new[] { "UserID", "AuthenticatorID" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAuthenticators");

            migrationBuilder.DropTable(
                name: "Authenticators");

            migrationBuilder.DropIndex(
                name: "IX_Versions_VersionKey",
                table: "Versions");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserName",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_SessionKey",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_Roles_AppID_Name",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Resources_GroupID_Name",
                table: "Resources");

            migrationBuilder.DropIndex(
                name: "IX_ResourceGroups_VersionID_Name",
                table: "ResourceGroups");

            migrationBuilder.DropIndex(
                name: "IX_Requests_RequestKey",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Modifiers_CategoryID_ModKey",
                table: "Modifiers");

            migrationBuilder.DropIndex(
                name: "IX_Modifiers_CategoryID_TargetKey",
                table: "Modifiers");

            migrationBuilder.DropIndex(
                name: "IX_ModifierCategories_AppID_Name",
                table: "ModifierCategories");

            migrationBuilder.DropIndex(
                name: "IX_InstallLocations_QualifiedMachineName",
                table: "InstallLocations");

            migrationBuilder.DropIndex(
                name: "IX_Events_EventKey",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Apps_Name",
                table: "Apps");

            migrationBuilder.DropColumn(
                name: "Domain",
                table: "Versions");

            migrationBuilder.DropColumn(
                name: "Domain",
                table: "Apps");

            migrationBuilder.AlterColumn<string>(
                name: "VersionKey",
                table: "Versions",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Versions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "UserAgent",
                table: "Sessions",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "SessionKey",
                table: "Sessions",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "RequesterKey",
                table: "Sessions",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "RemoteAddress",
                table: "Sessions",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Roles",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Resources",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ResourceGroups",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "RequestKey",
                table: "Requests",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Path",
                table: "Requests",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "TargetKey",
                table: "Modifiers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "ModKey",
                table: "Modifiers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "DisplayText",
                table: "Modifiers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ModifierCategories",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "QualifiedMachineName",
                table: "InstallLocations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "Events",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 5000);

            migrationBuilder.AlterColumn<string>(
                name: "EventKey",
                table: "Events",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Detail",
                table: "Events",
                type: "nvarchar(max)",
                maxLength: 32000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 32000);

            migrationBuilder.AlterColumn<string>(
                name: "Caption",
                table: "Events",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Apps",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Apps",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.CreateIndex(
                name: "IX_Versions_VersionKey",
                table: "Versions",
                column: "VersionKey",
                unique: true,
                filter: "[VersionKey] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName",
                unique: true,
                filter: "[UserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_SessionKey",
                table: "Sessions",
                column: "SessionKey",
                unique: true,
                filter: "[SessionKey] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_AppID_Name",
                table: "Roles",
                columns: new[] { "AppID", "Name" },
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_GroupID_Name",
                table: "Resources",
                columns: new[] { "GroupID", "Name" },
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceGroups_VersionID_Name",
                table: "ResourceGroups",
                columns: new[] { "VersionID", "Name" },
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_RequestKey",
                table: "Requests",
                column: "RequestKey",
                unique: true,
                filter: "[RequestKey] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Modifiers_CategoryID_ModKey",
                table: "Modifiers",
                columns: new[] { "CategoryID", "ModKey" },
                unique: true,
                filter: "[ModKey] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Modifiers_CategoryID_TargetKey",
                table: "Modifiers",
                columns: new[] { "CategoryID", "TargetKey" },
                unique: true,
                filter: "[TargetKey] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ModifierCategories_AppID_Name",
                table: "ModifierCategories",
                columns: new[] { "AppID", "Name" },
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_InstallLocations_QualifiedMachineName",
                table: "InstallLocations",
                column: "QualifiedMachineName",
                unique: true,
                filter: "[QualifiedMachineName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Events_EventKey",
                table: "Events",
                column: "EventKey",
                unique: true,
                filter: "[EventKey] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Apps_Name",
                table: "Apps",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");
        }
    }
}
