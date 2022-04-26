using Microsoft.EntityFrameworkCore.Migrations;

namespace XTI_HubDB.EF.SqlServer
{
    public partial class AddResourceGroupRolesAndResourceRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAnonymousAllowed",
                table: "Resources",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAnonymousAllowed",
                table: "ResourceGroups",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ResourceGroupRoles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupID = table.Column<int>(type: "int", nullable: false),
                    RoleID = table.Column<int>(type: "int", nullable: false),
                    IsAllowed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceGroupRoles", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ResourceGroupRoles_ResourceGroups_GroupID",
                        column: x => x.GroupID,
                        principalTable: "ResourceGroups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ResourceGroupRoles_Roles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ResourceRoles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResourceID = table.Column<int>(type: "int", nullable: false),
                    RoleID = table.Column<int>(type: "int", nullable: false),
                    IsAllowed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceRoles", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ResourceRoles_Resources_ResourceID",
                        column: x => x.ResourceID,
                        principalTable: "Resources",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ResourceRoles_Roles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResourceGroupRoles_GroupID_RoleID",
                table: "ResourceGroupRoles",
                columns: new[] { "GroupID", "RoleID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResourceGroupRoles_RoleID",
                table: "ResourceGroupRoles",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceRoles_ResourceID_RoleID",
                table: "ResourceRoles",
                columns: new[] { "ResourceID", "RoleID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResourceRoles_RoleID",
                table: "ResourceRoles",
                column: "RoleID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResourceGroupRoles");

            migrationBuilder.DropTable(
                name: "ResourceRoles");

            migrationBuilder.DropColumn(
                name: "IsAnonymousAllowed",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "IsAnonymousAllowed",
                table: "ResourceGroups");
        }
    }
}
