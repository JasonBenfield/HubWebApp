// Generated Code
namespace XTI_HubAppClient;
public sealed partial class InstallationGroup : AppClientGroup
{
    public InstallationGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "Installation")
    {
        Actions = new InstallationGroupActions(AddDeleteCommand: CreatePostAction<InstallationIDRequest, AppDeleteCommandDetailModel>("AddDeleteCommand"), BeginDelete: CreatePostAction<InstallationIDRequest, EmptyActionResult>("BeginDelete"), Deleted: CreatePostAction<InstallationIDRequest, EmptyActionResult>("Deleted"), GetInstallationDetail: CreatePostAction<InstallationIDRequest, InstallationDetailModel>("GetInstallationDetail"), Index: CreateGetAction<InstallationViewRequest>("Index"), Installed: CreatePostAction<InstallationIDRequest, EmptyActionResult>("Installed"));
        Configure();
    }

    partial void Configure();
    public InstallationGroupActions Actions { get; }

    public Task<AppDeleteCommandDetailModel> AddDeleteCommand(string modifier, InstallationIDRequest requestData, CancellationToken ct = default) => Actions.AddDeleteCommand.Post(modifier, requestData, ct);
    public Task<EmptyActionResult> BeginDelete(string modifier, InstallationIDRequest requestData, CancellationToken ct = default) => Actions.BeginDelete.Post(modifier, requestData, ct);
    public Task<EmptyActionResult> Deleted(string modifier, InstallationIDRequest requestData, CancellationToken ct = default) => Actions.Deleted.Post(modifier, requestData, ct);
    public Task<InstallationDetailModel> GetInstallationDetail(string modifier, InstallationIDRequest requestData, CancellationToken ct = default) => Actions.GetInstallationDetail.Post(modifier, requestData, ct);
    public Task<EmptyActionResult> Installed(string modifier, InstallationIDRequest requestData, CancellationToken ct = default) => Actions.Installed.Post(modifier, requestData, ct);
    public sealed record InstallationGroupActions(AppClientPostAction<InstallationIDRequest, AppDeleteCommandDetailModel> AddDeleteCommand, AppClientPostAction<InstallationIDRequest, EmptyActionResult> BeginDelete, AppClientPostAction<InstallationIDRequest, EmptyActionResult> Deleted, AppClientPostAction<InstallationIDRequest, InstallationDetailModel> GetInstallationDetail, AppClientGetAction<InstallationViewRequest> Index, AppClientPostAction<InstallationIDRequest, EmptyActionResult> Installed);
}