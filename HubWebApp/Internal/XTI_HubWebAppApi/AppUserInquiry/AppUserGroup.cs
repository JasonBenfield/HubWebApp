namespace XTI_HubWebAppApi.AppUserInquiry;

public sealed class AppUserGroup : AppApiGroupWrapper
{
    public AppUserGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        Index = source.AddAction(nameof(Index), () => sp.GetRequiredService<IndexAction>());
        GetExplicitUserAccess = source.AddAction
        (
            nameof(GetExplicitUserAccess), 
            () => sp.GetRequiredService<GetExplicitUserAccessAction>()
        );
        GetAssignedRoles = source.AddAction
        (
            nameof(GetAssignedRoles),
            () => sp.GetRequiredService<GetAssignedRolesAction>()
        );
        GetExplicitlyUnassignedRoles = source.AddAction
        (
            nameof(GetExplicitlyUnassignedRoles), 
            () => sp.GetRequiredService<GetExplicitlyUnassignedRolesAction>()
        );
    }

    public AppApiAction<GetAppUserRequest, WebViewResult> Index { get; }
    public AppApiAction<UserModifierKey, UserAccessModel> GetExplicitUserAccess { get; }
    public AppApiAction<UserModifierKey, AppRoleModel[]> GetAssignedRoles { get; }
    public AppApiAction<UserModifierKey, AppRoleModel[]> GetExplicitlyUnassignedRoles { get; }
}