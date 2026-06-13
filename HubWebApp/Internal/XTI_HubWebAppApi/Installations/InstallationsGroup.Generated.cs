using XTI_HubWebAppApiActions.Installations;

// Generated Code
#nullable enable
namespace XTI_HubWebAppApi.Installations;
public sealed partial class InstallationsGroup : AppApiGroupWrapper
{
    internal InstallationsGroup(AppApiGroup source, InstallationsGroupBuilder builder) : base(source)
    {
        AddInstallCommand = builder.AddInstallCommand.Build();
        Index = builder.Index.Build();
        Configure();
    }

    partial void Configure();
    public AppApiAction<AddInstallCommandRequest, AppInstallCommandDetailModel> AddInstallCommand { get; }
    public AppApiAction<InstallationQueryRequest, WebViewResult> Index { get; }
}