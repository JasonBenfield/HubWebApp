using XTI_HubWebAppApiActions.AppList;

// Generated Code
namespace XTI_HubWebAppApi.Apps;
public sealed partial class AppsGroup : AppApiGroupWrapper
{
    internal AppsGroup(AppApiGroup source, AppsGroupBuilder builder) : base(source)
    {
        GetAppDomains = builder.GetAppDomains.Build();
        GetApps = builder.GetApps.Build();
        Index = builder.Index.Build();
        Configure();
    }

    partial void Configure();
    public AppApiAction<EmptyRequest, AppDomainModel[]> GetAppDomains { get; }
    public AppApiAction<EmptyRequest, AppModel[]> GetApps { get; }
    public AppApiAction<EmptyRequest, WebViewResult> Index { get; }
}