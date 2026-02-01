namespace XTI_HubDB.EF.SqlServer.Migrations.Current;

internal static class ExpandedUsers
{
    internal static readonly string Sql =
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
        """;
}
