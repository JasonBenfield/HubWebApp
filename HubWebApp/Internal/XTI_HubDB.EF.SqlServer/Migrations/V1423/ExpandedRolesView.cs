namespace XTI_HubDB.EF.SqlServer.V1423;

internal static class ExpandedRolesView
{
    public static readonly string Sql = """
CREATE OR ALTER view ExpandedRoles as
select 
	Roles.ID RoleID, Roles.Name RoleName, Roles.DisplayText RoleDisplayText,
    dbo.GetLocalDateTime(Roles.TimeDeactivated) TimeRoleDeactivated,
	Apps.ID AppID, 
	dbo.AppKeyDisplayText(Apps.DisplayText, Apps.Type) AppKey,
	Apps.DisplayText AppName, dbo.GetLocalDateTime(Apps.TimeAdded) TimeAppAdded, Apps.Title AppTitle, 
	Apps.Type AppTypeValue,
	dbo.AppTypeDisplayText(Apps.Type) AppType
from Roles
inner join Apps
on Roles.AppID = Apps.ID
""";
}
