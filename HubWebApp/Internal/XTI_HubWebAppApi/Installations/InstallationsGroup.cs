namespace XTI_HubWebAppApi.Installations;

public sealed class InstallationsGroup : AppApiGroupWrapper
{
    public InstallationsGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        Index = source.AddAction(nameof(Index), () => sp.GetRequiredService<IndexAction>());
    }

    public AppApiAction<InstallationQueryRequest, WebViewResult> Index { get; }
}