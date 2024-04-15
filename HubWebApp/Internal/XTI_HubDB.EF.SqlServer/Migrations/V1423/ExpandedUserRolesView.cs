namespace XTI_HubDB.EF.SqlServer.V1423;

internal static class ExpandedUserRolesView
{
    public static readonly string Sql = """
CREATE OR ALTER view ExpandedUserRoles as
select 
	UserRoles.ID UserRoleID,
	UserGroups.ID UserGroupID, UserGroups.GroupName UserGroupName, UserGroups.DisplayText UserGroupDisplayText,
	users.ID UserID, users.UserName, users.Name UserPersonalName, users.Email, 
	dbo.GetLocalDateTime(users.TimeAdded) TimeUserAdded, 
	dbo.GetLocalDateTime(users.TimeLoggedIn) TimeUserLoggedIn,
	dbo.IsMaxDateTime(users.TimeDeactivated) IsUserActive,
	dbo.GetLocalDateTime(users.TimeDeactivated) TimeUserDeactivated,
	modcat.ID ModCategoryID, modcat.DisplayText ModCategoryName,
	mod.ID ModifierID, mod.ModKeyDisplayText ModKey, mod.TargetKey ModTargetKey, mod.DisplayText ModDisplayText,
	Roles.ID RoleID, Roles.Name RoleName, Roles.DisplayText RoleDisplayText,
	dbo.IsMaxDateTime(Roles.TimeDeactivated) IsRoleActive,
    dbo.GetLocalDateTime(Roles.TimeDeactivated) TimeRoleDeactivated,
	Apps.ID AppID, 
	dbo.AppKeyDisplayText(Apps.DisplayText, Apps.Type) AppKey,
	Apps.DisplayText AppName, dbo.GetLocalDateTime(Apps.TimeAdded) TimeAppAdded, Apps.Title AppTitle, 
	Apps.Type AppTypeValue,
	dbo.AppTypeDisplayText(Apps.Type) AppType
from UserRoles
inner join users
on UserRoles.userid = users.id
inner join UserGroups 
on users.GroupID = UserGroups.ID
inner join Modifiers mod
on UserRoles.ModifierID = mod.ID
inner join ModifierCategories modcat
on mod.CategoryID = modcat.ID
inner join Roles
on UserRoles.RoleID = Roles.ID
inner join Apps
on Roles.AppID = Apps.ID
""";
}
