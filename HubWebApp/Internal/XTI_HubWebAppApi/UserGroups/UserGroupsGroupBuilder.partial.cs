namespace XTI_HubWebAppApi.UserGroups;

partial class UserGroupsGroupBuilder
{
    partial void Configure()
    {
        AddUserGroupIfNotExists.WithAllowed(HubInfo.Roles.AddUserGroup);
        GetUserDetailOrAnon.ResetAccessWithAllowed(HubInfo.Roles.UserViewerRoles);
        GetUserGroups.ResetAccess();
        Index.ResetAccess();
        UserQuery.ResetAccess();
    }
}
