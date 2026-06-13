using XTI_HubWebAppApiActions.Installation;

// Generated Code
#nullable enable
namespace XTI_HubWebAppApi.Installation;
public sealed partial class InstallationGroup : AppApiGroupWrapper
{
    internal InstallationGroup(AppApiGroup source, InstallationGroupBuilder builder) : base(source)
    {
        AddDeleteCommand = builder.AddDeleteCommand.Build();
        BeginDelete = builder.BeginDelete.Build();
        Deleted = builder.Deleted.Build();
        GetInstallationDetail = builder.GetInstallationDetail.Build();
        Index = builder.Index.Build();
        Installed = builder.Installed.Build();
        Configure();
    }

    partial void Configure();
    public AppApiAction<InstallationIDRequest, AppDeleteCommandDetailModel> AddDeleteCommand { get; }
    public AppApiAction<InstallationIDRequest, EmptyActionResult> BeginDelete { get; }
    public AppApiAction<InstallationIDRequest, EmptyActionResult> Deleted { get; }
    public AppApiAction<InstallationIDRequest, InstallationDetailModel> GetInstallationDetail { get; }
    public AppApiAction<InstallationViewRequest, WebViewResult> Index { get; }
    public AppApiAction<InstallationIDRequest, EmptyActionResult> Installed { get; }
}