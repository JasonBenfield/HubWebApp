namespace XTI_HubWebAppApi.UserRoles;

public sealed class UserRolesGroup : AppApiGroupWrapper
{
    public UserRolesGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        Index = source.AddAction(nameof(Index), () => sp.GetRequiredService<IndexAction>());
        GetUserRoleDetail = source.AddAction(nameof(GetUserRoleDetail), () => sp.GetRequiredService<GetUserRoleDetailAction>());
    }

    public AppApiAction<UserRoleQueryRequest, WebViewResult> Index { get; }
    public AppApiAction<int, UserRoleDetailModel> GetUserRoleDetail { get; }
}