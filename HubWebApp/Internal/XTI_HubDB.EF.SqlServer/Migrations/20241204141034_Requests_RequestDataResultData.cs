using Microsoft.EntityFrameworkCore.Migrations;
using XTI_HubDB.EF.SqlServer.Migrations.V1429;

#nullable disable

namespace XTIHubDB.EF.SqlServer
{
    /// <inheritdoc />
    public partial class Requests_RequestDataResultData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RequestData",
                table: "Requests",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ResultData",
                table: "Requests",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                defaultValue: "");
            migrationBuilder.Sql(ExpandedRequestsView.Sql);
            migrationBuilder.Sql(ExpandedLogEntriesView.Sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestData",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "ResultData",
                table: "Requests");
        }
    }
}
