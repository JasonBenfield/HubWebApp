// Generated Code
namespace XTI_HubAppClient;
public sealed partial class InstallationsGroup : AppClientGroup
{
    public InstallationsGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "Installations")
    {
        Actions = new InstallationsGroupActions(AddInstallCommand: CreatePostAction<AddInstallCommandRequest, AppInstallCommandDetailModel>("AddInstallCommand"), Index: CreateGetAction<InstallationQueryRequest>("Index"));
        Configure();
    }

    partial void Configure();
    public InstallationsGroupActions Actions { get; }

    public Task<AppInstallCommandDetailModel> AddInstallCommand(AddInstallCommandRequest requestData, CancellationToken ct = default) => Actions.AddInstallCommand.Post("", requestData, ct);
    public sealed record InstallationsGroupActions(AppClientPostAction<AddInstallCommandRequest, AppInstallCommandDetailModel> AddInstallCommand, AppClientGetAction<InstallationQueryRequest> Index);
}