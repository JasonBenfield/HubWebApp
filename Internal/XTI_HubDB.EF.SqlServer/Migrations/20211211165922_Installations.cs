using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace XTI_HubDB.EF.SqlServer
{
    public partial class Installations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InstallLocations",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QualifiedMachineName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstallLocations", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Installations",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationID = table.Column<int>(type: "int", nullable: false),
                    VersionID = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IsCurrent = table.Column<bool>(type: "bit", nullable: false),
                    TimeAdded = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Installations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Installations_InstallLocations_LocationID",
                        column: x => x.LocationID,
                        principalTable: "InstallLocations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Installations_Versions_VersionID",
                        column: x => x.VersionID,
                        principalTable: "Versions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Installations_LocationID_VersionID_IsCurrent",
                table: "Installations",
                columns: new[] { "LocationID", "VersionID", "IsCurrent" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Installations_VersionID",
                table: "Installations",
                column: "VersionID");

            migrationBuilder.CreateIndex(
                name: "IX_InstallLocations_QualifiedMachineName",
                table: "InstallLocations",
                column: "QualifiedMachineName",
                unique: true,
                filter: "[QualifiedMachineName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Installations");

            migrationBuilder.DropTable(
                name: "InstallLocations");
        }
    }
}
