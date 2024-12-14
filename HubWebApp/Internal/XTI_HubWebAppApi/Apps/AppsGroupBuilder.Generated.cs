using XTI_HubWebAppApiActions.AppList;

// Generated Code
namespace XTI_HubWebAppApi.Apps;
public sealed partial class AppsGroupBuilder
{
    private readonly AppApiGroup source;
    internal AppsGroupBuilder(AppApiGroup source)
    {
        this.source = source;
        GetAppDomains = source.AddAction<EmptyRequest, AppDomainModel[]>("GetAppDomains").WithExecution<GetAppDomainsAction>();
        GetApps = source.AddAction<EmptyRequest, AppModel[]>("GetApps").WithExecution<GetAppsAction>();
        Index = source.AddAction<EmptyRequest, WebViewResult>("Index").WithExecution<IndexAction>();
        Configure();
    }

    partial void Configure();
    public AppApiActionBuilder<EmptyRequest, AppDomainModel[]> GetAppDomains { get; }
    public AppApiActionBuilder<EmptyRequest, AppModel[]> GetApps { get; }
    public AppApiActionBuilder<EmptyRequest, WebViewResult> Index { get; }

    public AppsGroup Build() => new AppsGroup(source, this);
}