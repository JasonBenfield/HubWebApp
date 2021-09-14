using Microsoft.EntityFrameworkCore.Migrations;

namespace XTI_HubDB.EF.SqlServer
{
    public partial class MoveModifiertoUserRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ModifierID",
                table: "UserRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.Sql(
                @"
merge modifiers
using
(
	select modifierCategories.id modCatID
	from apps
	inner join modifiercategories
	on apps.id = modifiercategories.appid
	where modifierCategories.name = 'default'
) appModCats
on modifiers.categoryid = appModCats.modCatID and modifiers.modkey = ''
when not matched then
	insert (categoryid, modkey, targetkey, displaytext)
	values (appModCats.modCatID, '', '', '');
");

            migrationBuilder.Sql(
                @"
merge userRoles
using
(
	select a.id modifierID, d.id roleid
	from modifiers a
	inner join modifiercategories b
	on a.categoryid = b.id
	inner join apps c
	on b.appid = c.id
	inner join roles d
	on c.id = d.appid
	where a.modkey = '' and b.name = 'default'
) appMods
on userRoles.roleid = appMods.roleid
when matched then
	update set userRoles.ModifierID = appMods.ModifierID;
");

            migrationBuilder.DropTable(
                name: "ModifierCategoryAdmins");

            migrationBuilder.DropTable(
                name: "UserModifiers");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_ModifierID",
                table: "UserRoles",
                column: "ModifierID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Modifiers_ModifierID",
                table: "UserRoles",
                column: "ModifierID",
                principalTable: "Modifiers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Modifiers_ModifierID",
                table: "UserRoles");

            migrationBuilder.DropIndex(
                name: "IX_UserRoles_ModifierID",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "ModifierID",
                table: "UserRoles");

            migrationBuilder.CreateTable(
                name: "ModifierCategoryAdmins",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModCategoryID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModifierCategoryAdmins", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ModifierCategoryAdmins_ModifierCategories_ModCategoryID",
                        column: x => x.ModCategoryID,
                        principalTable: "ModifierCategories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ModifierCategoryAdmins_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserModifiers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModifierID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserModifiers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserModifiers_Modifiers_ModifierID",
                        column: x => x.ModifierID,
                        principalTable: "Modifiers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserModifiers_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ModifierCategoryAdmins_ModCategoryID_UserID",
                table: "ModifierCategoryAdmins",
                columns: new[] { "ModCategoryID", "UserID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ModifierCategoryAdmins_UserID",
                table: "ModifierCategoryAdmins",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserModifiers_ModifierID",
                table: "UserModifiers",
                column: "ModifierID");

            migrationBuilder.CreateIndex(
                name: "IX_UserModifiers_UserID",
                table: "UserModifiers",
                column: "UserID");
        }
    }
}
