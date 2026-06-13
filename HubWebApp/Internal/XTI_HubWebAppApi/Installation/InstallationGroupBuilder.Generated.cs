using XTI_HubWebAppApiActions.Installation;

// Generated Code
#nullable enable
namespace XTI_HubWebAppApi.Installation;
public sealed partial class InstallationGroupBuilder
{
    private readonly AppApiGroup source;
    internal InstallationGroupBuilder(AppApiGroup source)
    {
        this.source = source;
        AddDeleteCommand = source.AddAction<InstallationIDRequest, AppDeleteCommandDetailModel>("AddDeleteCommand").WithExecution<AddDeleteCommandAction>().WithValidation<AddDeleteCommandValidation>();
        BeginDelete = source.AddAction<InstallationIDRequest, EmptyActionResult>("BeginDelete").WithExecution<BeginDeleteAction>().WithValidation<BeginDeleteValidation>();
        Deleted = source.AddAction<InstallationIDRequest, EmptyActionResult>("Deleted").WithExecution<DeletedAction>().WithValidation<DeletedValidation>();
        GetInstallationDetail = source.AddAction<InstallationIDRequest, InstallationDetailModel>("GetInstallationDetail").WithExecution<GetInstallationDetailAction>().WithValidation<GetInstallationDetailValidation>();
        Index = source.AddAction<InstallationViewRequest, WebViewResult>("Index").WithExecution<IndexPage>();
        Installed = source.AddAction<InstallationIDRequest, EmptyActionResult>("Installed").WithExecution<InstalledAction>().WithValidation<InstalledValidation>();
        Configure();
    }

    partial void Configure();
    public AppApiActionBuilder<InstallationIDRequest, AppDeleteCommandDetailModel> AddDeleteCommand { get; }
    public AppApiActionBuilder<InstallationIDRequest, EmptyActionResult> BeginDelete { get; }
    public AppApiActionBuilder<InstallationIDRequest, EmptyActionResult> Deleted { get; }
    public AppApiActionBuilder<InstallationIDRequest, InstallationDetailModel> GetInstallationDetail { get; }
    public AppApiActionBuilder<InstallationViewRequest, WebViewResult> Index { get; }
    public AppApiActionBuilder<InstallationIDRequest, EmptyActionResult> Installed { get; }

    public InstallationGroup Build() => new InstallationGroup(source, this);
}