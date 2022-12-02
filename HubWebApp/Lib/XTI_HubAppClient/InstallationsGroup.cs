// Generated Code
namespace XTI_HubAppClient;
public sealed partial class InstallationsGroup : AppClientGroup
{
    public InstallationsGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "Installations")
    {
        Actions = new InstallationsGroupActions(Index: CreateGetAction<InstallationQueryRequest>("Index"), GetPendingDeletes: CreatePostAction<GetPendingDeletesRequest, InstallationModel[]>("GetPendingDeletes"), RequestDelete: CreatePostAction<GetInstallationRequest, EmptyActionResult>("RequestDelete"), BeginDelete: CreatePostAction<GetInstallationRequest, EmptyActionResult>("BeginDelete"), Deleted: CreatePostAction<GetInstallationRequest, EmptyActionResult>("Deleted"));
    }

    public InstallationsGroupActions Actions { get; }

    public Task<InstallationModel[]> GetPendingDeletes(GetPendingDeletesRequest model, CancellationToken ct = default) => Actions.GetPendingDeletes.Post("", model, ct);
    public Task<EmptyActionResult> RequestDelete(GetInstallationRequest model, CancellationToken ct = default) => Actions.RequestDelete.Post("", model, ct);
    public Task<EmptyActionResult> BeginDelete(GetInstallationRequest model, CancellationToken ct = default) => Actions.BeginDelete.Post("", model, ct);
    public Task<EmptyActionResult> Deleted(GetInstallationRequest model, CancellationToken ct = default) => Actions.Deleted.Post("", model, ct);
    public sealed record InstallationsGroupActions(AppClientGetAction<InstallationQueryRequest> Index, AppClientPostAction<GetPendingDeletesRequest, InstallationModel[]> GetPendingDeletes, AppClientPostAction<GetInstallationRequest, EmptyActionResult> RequestDelete, AppClientPostAction<GetInstallationRequest, EmptyActionResult> BeginDelete, AppClientPostAction<GetInstallationRequest, EmptyActionResult> Deleted);
}