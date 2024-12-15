namespace XTI_HubWebAppApi.UserRoles;

partial class UserRolesGroupBuilder
{
    partial void Configure()
    {
        DeleteUserRole.ResetAccessWithAllowed(HubInfo.Roles.UserEditorRoles);
        GetUserRoleDetail.ResetAccessWithAllowed
        (
            HubInfo.Roles.AppViewerRoles
                .Union(HubInfo.Roles.UserViewerRoles)
                .ToArray()
        );
        UserRole.ResetAccessWithAllowed
        (
            HubInfo.Roles.AppViewerRoles
                .Union(HubInfo.Roles.UserViewerRoles)
                .ToArray()
        );
    }
}
