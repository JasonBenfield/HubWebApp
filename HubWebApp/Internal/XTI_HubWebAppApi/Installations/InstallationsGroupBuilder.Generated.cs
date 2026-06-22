using XTI_HubWebAppApiActions.Installations;

// Generated Code
#nullable enable
namespace XTI_HubWebAppApi.Installations;
public sealed partial class InstallationsGroupBuilder
{
    private readonly AppApiGroup source;
    internal InstallationsGroupBuilder(AppApiGroup source)
    {
        this.source = source;
        Index = source.AddAction<InstallationQueryRequest, WebViewResult>("Index").WithExecution<IndexPage>();
        Configure();
    }

    partial void Configure();
    public AppApiActionBuilder<InstallationQueryRequest, WebViewResult> Index { get; }

    public InstallationsGroup Build() => new InstallationsGroup(source, this);
}