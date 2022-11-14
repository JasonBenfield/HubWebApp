namespace XTI_HubWebAppApi.CurrentUser;

public sealed class CurrentUserGroup : AppApiGroupWrapper
{
    public CurrentUserGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        GetUser = source.AddAction(nameof(GetUser), () => sp.GetRequiredService<GetUserAction>());
        Index = source.AddAction(nameof(Index), () => sp.GetRequiredService<IndexAction>());
    }

    public AppApiAction<EmptyRequest, AppUserModel> GetUser { get; }
    public AppApiAction<EmptyRequest, WebViewResult> Index { get; }
}