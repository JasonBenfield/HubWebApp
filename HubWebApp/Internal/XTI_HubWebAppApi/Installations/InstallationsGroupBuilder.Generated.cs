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
        AddInstallCommand = source.AddAction<AddInstallCommandRequest, AppInstallCommandDetailModel>("AddInstallCommand").WithExecution<AddInstallCommandAction>().WithValidation<AddInstallCommandValidation>();
        Index = source.AddAction<InstallationQueryRequest, WebViewResult>("Index").WithExecution<IndexPage>();
        Configure();
    }

    partial void Configure();
    public AppApiActionBuilder<AddInstallCommandRequest, AppInstallCommandDetailModel> AddInstallCommand { get; }
    public AppApiActionBuilder<InstallationQueryRequest, WebViewResult> Index { get; }

    public InstallationsGroup Build() => new InstallationsGroup(source, this);
}