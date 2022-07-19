namespace XTI_HubWebAppApi.AppUserInquiry;

public sealed class AppUserGroup : AppApiGroupWrapper
{
    public AppUserGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        Index = source.AddAction(nameof(Index), () => sp.GetRequiredService<IndexAction>());
        GetUserAccess = source.AddAction(nameof(GetUserAccess), () => sp.GetRequiredService<GetUserAccessByUserModifierAction>());
        GetUnassignedRoles = source.AddAction(nameof(GetUnassignedRoles), () => sp.GetRequiredService<GetUnassignedRolesAction>());
    }
    public AppApiAction<GetUserRequest, WebViewResult> Index { get; }
    public AppApiAction<UserModifierKey, UserAccessModel> GetUserAccess { get; }
    public AppApiAction<UserModifierKey, AppRoleModel[]> GetUnassignedRoles { get; }
}