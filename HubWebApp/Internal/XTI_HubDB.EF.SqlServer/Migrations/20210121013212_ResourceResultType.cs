using Microsoft.EntityFrameworkCore.Migrations;

namespace XTI_HubDB.EF.SqlServer
{
    public partial class ResourceResultType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupID",
                table: "Modifiers");

            migrationBuilder.AddColumn<int>(
                name: "ResultType",
                table: "Resources",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResultType",
                table: "Resources");

            migrationBuilder.AddColumn<int>(
                name: "GroupID",
                table: "Modifiers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
