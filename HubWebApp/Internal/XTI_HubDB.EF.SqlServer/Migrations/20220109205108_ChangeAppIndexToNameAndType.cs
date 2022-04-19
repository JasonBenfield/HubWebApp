using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XTI_HubDB.EF.SqlServer
{
    public partial class ChangeAppIndexToNameAndType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Apps_Name",
                table: "Apps");

            migrationBuilder.CreateIndex(
                name: "IX_Apps_Name_Type",
                table: "Apps",
                columns: new[] { "Name", "Type" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Apps_Name_Type",
                table: "Apps");

            migrationBuilder.CreateIndex(
                name: "IX_Apps_Name",
                table: "Apps",
                column: "Name",
                unique: true);
        }
    }
}
