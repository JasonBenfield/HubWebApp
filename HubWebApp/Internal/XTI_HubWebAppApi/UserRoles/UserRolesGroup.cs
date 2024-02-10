namespace XTI_HubWebAppApi.UserRoles;

public sealed class UserRolesGroup : AppApiGroupWrapper
{
    public UserRolesGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        DeleteUserRole = source.AddAction
        (
            nameof(DeleteUserRole),
            () => sp.GetRequiredService<DeleteUserRoleAction>(),
            access: new(HubInfo.Roles.UserEditorRoles)
        );
        Index = source.AddAction
        (
            nameof(Index),
            () => sp.GetRequiredService<IndexPage>()
        );
        GetUserRoleDetail = source.AddAction
        (
            nameof(GetUserRoleDetail),
            () => sp.GetRequiredService<GetUserRoleDetailAction>(),
            access: new(HubInfo.Roles.AppViewerRoles.Union(HubInfo.Roles.UserViewerRoles).ToArray())
        );
        UserRole = source.AddAction
        (
            nameof(UserRole),
            () => sp.GetRequiredService<UserRolePage>(),
            access: new(HubInfo.Roles.AppViewerRoles.Union(HubInfo.Roles.UserViewerRoles).ToArray())
        );
    }

    public AppApiAction<UserRoleIDRequest, EmptyActionResult> DeleteUserRole { get; }
    public AppApiAction<UserRoleQueryRequest, WebViewResult> Index { get; }
    public AppApiAction<UserRoleIDRequest, UserRoleDetailModel> GetUserRoleDetail { get; }
    public AppApiAction<UserRoleIDRequest, WebViewResult> UserRole { get; }
}