using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XTIHubDB.EF.SqlServer
{
    /// <inheritdoc />
    public partial class AddTimeDeactivatedToUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>
            (
                name: "TimeDeactivated",
                table: "Users",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: DateTimeOffset.MaxValue
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeDeactivated",
                table: "Users");
        }
    }
}
