using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XTI_HubDB.EF.SqlServer
{
    public partial class AuthenticatorsWithKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authenticators_Apps_AppID",
                table: "Authenticators");

            migrationBuilder.DropIndex(
                name: "IX_Authenticators_AppID",
                table: "Authenticators");

            migrationBuilder.AddColumn<string>(
                name: "AuthenticatorKey",
                table: "Authenticators",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AuthenticatorName",
                table: "Authenticators",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.Sql
            (
                @"
                merge into Authenticators tgt
                using
                (
                    select ID, Name
                    from apps
                ) as src
                on tgt.AppID = src.ID
                when matched then update
                    set tgt.AuthenticatorKey = src.Name, tgt.AuthenticatorName = src.Name;
"
            );

            migrationBuilder.DropColumn(
                name: "AppID",
                table: "Authenticators");

            migrationBuilder.CreateIndex(
                name: "IX_Authenticators_AuthenticatorKey",
                table: "Authenticators",
                column: "AuthenticatorKey",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Authenticators_AuthenticatorKey",
                table: "Authenticators");

            migrationBuilder.DropColumn(
                name: "AuthenticatorKey",
                table: "Authenticators");

            migrationBuilder.DropColumn(
                name: "AuthenticatorName",
                table: "Authenticators");

            migrationBuilder.AddColumn<int>(
                name: "AppID",
                table: "Authenticators",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Authenticators_AppID",
                table: "Authenticators",
                column: "AppID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Authenticators_Apps_AppID",
                table: "Authenticators",
                column: "AppID",
                principalTable: "Apps",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
