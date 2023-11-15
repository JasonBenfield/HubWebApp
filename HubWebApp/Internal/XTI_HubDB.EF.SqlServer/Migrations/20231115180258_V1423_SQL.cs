using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XTIHubDB.EF.SqlServer;

public partial class V1423_SQL : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql(XTI_HubDB.EF.SqlServer.V1423.GetLocalDateTime.Sql);
        migrationBuilder.Sql(XTI_HubDB.EF.SqlServer.V1423.ExpandedRequestsView.Sql);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
    }
}
