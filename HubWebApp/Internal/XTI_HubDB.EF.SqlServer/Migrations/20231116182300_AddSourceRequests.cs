using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XTIHubDB.EF.SqlServer
{
    /// <inheritdoc />
    public partial class AddSourceRequests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SourceLogEntries_SourceID",
                table: "SourceLogEntries");

            migrationBuilder.CreateTable(
                name: "SourceRequests",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SourceID = table.Column<int>(type: "int", nullable: false),
                    TargetID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SourceRequests", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SourceRequests_Requests_SourceID",
                        column: x => x.SourceID,
                        principalTable: "Requests",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SourceRequests_Requests_TargetID",
                        column: x => x.TargetID,
                        principalTable: "Requests",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SourceLogEntries_SourceID_TargetID",
                table: "SourceLogEntries",
                columns: new[] { "SourceID", "TargetID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SourceRequests_SourceID_TargetID",
                table: "SourceRequests",
                columns: new[] { "SourceID", "TargetID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SourceRequests_TargetID",
                table: "SourceRequests",
                column: "TargetID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SourceRequests");

            migrationBuilder.DropIndex(
                name: "IX_SourceLogEntries_SourceID_TargetID",
                table: "SourceLogEntries");

            migrationBuilder.CreateIndex(
                name: "IX_SourceLogEntries_SourceID",
                table: "SourceLogEntries",
                column: "SourceID");
        }
    }
}
