using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XTIHubDB.EF.SqlServer
{
    /// <inheritdoc />
    public partial class InstallConfigurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InstallConfigurationTemplates",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TemplateName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DestinationMachineName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Domain = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SiteName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstallConfigurationTemplates", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "InstallConfigurations",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RepoOwner = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RepoName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ConfigurationName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AppName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AppType = table.Column<int>(type: "int", nullable: false),
                    TemplateID = table.Column<int>(type: "int", nullable: false),
                    InstallSequence = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstallConfigurations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_InstallConfigurations_InstallConfigurationTemplates_TemplateID",
                        column: x => x.TemplateID,
                        principalTable: "InstallConfigurationTemplates",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InstallConfigurations_RepoOwner_RepoName_ConfigurationName_AppName_AppType",
                table: "InstallConfigurations",
                columns: new[] { "RepoOwner", "RepoName", "ConfigurationName", "AppName", "AppType" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InstallConfigurations_TemplateID",
                table: "InstallConfigurations",
                column: "TemplateID");

            migrationBuilder.CreateIndex(
                name: "IX_InstallConfigurationTemplates_TemplateName",
                table: "InstallConfigurationTemplates",
                column: "TemplateName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InstallConfigurations");

            migrationBuilder.DropTable(
                name: "InstallConfigurationTemplates");
        }
    }
}
