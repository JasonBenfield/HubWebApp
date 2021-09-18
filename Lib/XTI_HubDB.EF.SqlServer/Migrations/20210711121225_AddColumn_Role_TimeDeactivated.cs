using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace XTI_HubDB.EF.SqlServer
{
    public partial class AddColumn_Role_TimeDeactivated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "TimeDeactivated",
                table: "Roles",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified).AddTicks(9999), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.CreateIndex(
                name: "IX_Modifiers_CategoryID_ModKey",
                table: "Modifiers",
                columns: new[] { "CategoryID", "ModKey" },
                unique: true,
                filter: "[ModKey] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Modifiers_CategoryID_ModKey",
                table: "Modifiers");

            migrationBuilder.DropColumn(
                name: "TimeDeactivated",
                table: "Roles");

            migrationBuilder.CreateIndex(
                name: "IX_Modifiers_ModKey",
                table: "Modifiers",
                column: "ModKey",
                unique: true,
                filter: "[ModKey] IS NOT NULL");
        }
    }
}
