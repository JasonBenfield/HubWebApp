using Microsoft.EntityFrameworkCore.Migrations;
using XTI_HubDB.EF.SqlServer.Migrations.V1423;
using XTI_HubDB.EF.SqlServer.V1423;

#nullable disable

namespace XTIHubDB.EF.SqlServer;

public partial class V1423_SQL : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql(GetLocalDateTime.Sql);
        migrationBuilder.Sql(GetMaxDateTime.Sql);
        migrationBuilder.Sql(IsMaxDatTime.Sql);
        migrationBuilder.Sql(PurgeLogs.Sql);
        migrationBuilder.Sql(ExpandedInstallationsView.Sql);
        migrationBuilder.Sql(ExpandedLogEntriesView.Sql);
        migrationBuilder.Sql(ExpandedModifiersView.Sql);
        migrationBuilder.Sql(ExpandedRequestsView.Sql);
        migrationBuilder.Sql(ExpandedRolesView.Sql);
        migrationBuilder.Sql(ExpandedSessionsView.Sql);
        migrationBuilder.Sql(ExpandedUserRolesView.Sql);
        migrationBuilder.Sql(InitialTimeLoggedIn.Sql);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {

    }
}
