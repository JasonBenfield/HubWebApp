using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XTIHubDB.EF.SqlServer;

public partial class ExpandedUsers : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql
        (
            """
            create or alter view ExpandedUsers
            as
            select
                u.ID UserID, u.UserName, u.Name PersonName,
                u.Email, u.TimeAdded TimeUserAdded, 
                ug.DisplayText UserGroupName, u.TimeDeactivated TimeUserDeactivated,
                case year(u.TimeDeactivated) when 9999 then 1 else 0 end IsActive,
                u.GroupID UserGroupID
            from Users u
            inner join UserGroups ug
            on u.GroupID = ug.ID
            """
        );
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
    }
}
