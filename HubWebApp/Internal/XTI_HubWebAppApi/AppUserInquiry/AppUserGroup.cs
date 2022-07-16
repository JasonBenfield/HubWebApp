namespace XTI_HubWebAppApi.AppUserInquiry;

public sealed class AppUserGroup : AppApiGroupWrapper
{
    public AppUserGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        Index = source.AddAction(nameof(Index), () => sp.GetRequiredService<IndexAction>());
        GetUserAccess = source.AddAction(nameof(GetUserAccess), () => sp.GetRequiredService<GetUserAccessAction>());
        GetUnassignedRoles = source.AddAction(nameof(GetUnassignedRoles), () => sp.GetRequiredService<GetUnassignedRolesAction>());
    }
    public AppApiAction<int, WebViewResult> Index { get; }
    public AppApiAction<UserModifierKey, UserAccessModel> GetUserAccess { get; }
    public AppApiAction<UserModifierKey, AppRoleModel[]> GetUnassignedRoles { get; }
}