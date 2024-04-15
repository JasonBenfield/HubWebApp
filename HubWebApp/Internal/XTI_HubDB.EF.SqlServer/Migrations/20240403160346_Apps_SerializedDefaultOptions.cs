using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XTIHubDB.EF.SqlServer
{
    /// <inheritdoc />
    public partial class Apps_SerializedDefaultOptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SerializedDefaultOptions",
                table: "Apps",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SerializedDefaultOptions",
                table: "Apps");
        }
    }
}
