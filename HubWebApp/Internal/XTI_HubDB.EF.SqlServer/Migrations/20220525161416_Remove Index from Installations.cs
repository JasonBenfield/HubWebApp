using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XTI_HubDB.EF.SqlServer
{
    public partial class RemoveIndexfromInstallations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Installations_LocationID_AppVersionID_IsCurrent",
                table: "Installations");

            migrationBuilder.CreateIndex(
                name: "IX_Installations_LocationID",
                table: "Installations",
                column: "LocationID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Installations_LocationID",
                table: "Installations");

            migrationBuilder.CreateIndex(
                name: "IX_Installations_LocationID_AppVersionID_IsCurrent",
                table: "Installations",
                columns: new[] { "LocationID", "AppVersionID", "IsCurrent" },
                unique: true);
        }
    }
}
