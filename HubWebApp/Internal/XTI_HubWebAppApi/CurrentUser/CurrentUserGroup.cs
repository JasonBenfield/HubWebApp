namespace XTI_HubWebAppApi.CurrentUser;

public sealed class CurrentUserGroup : AppApiGroupWrapper
{
    public CurrentUserGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        Index = source.AddAction(nameof(Index), () => sp.GetRequiredService<IndexAction>());
    }

    public AppApiAction<EmptyRequest, WebViewResult> Index { get; }
}