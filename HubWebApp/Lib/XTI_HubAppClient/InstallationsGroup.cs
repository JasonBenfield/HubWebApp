// Generated Code
namespace XTI_HubAppClient;
public sealed partial class InstallationsGroup : AppClientGroup
{
    public InstallationsGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "Installations")
    {
        Actions = new InstallationsGroupActions(BeginDelete: CreatePostAction<GetInstallationRequest, EmptyActionResult>("BeginDelete"), Deleted: CreatePostAction<GetInstallationRequest, EmptyActionResult>("Deleted"), GetInstallationActivities: CreatePostAction<GetInstallationActivitiesRequest, InstallationActivitiesResult>("GetInstallationActivities"), GetInstallationDetail: CreatePostAction<int, InstallationDetailModel>("GetInstallationDetail"), Index: CreateGetAction<InstallationQueryRequest>("Index"), Installation: CreateGetAction<InstallationViewRequest>("Installation"), RequestDelete: CreatePostAction<GetInstallationRequest, EmptyActionResult>("RequestDelete"));
        Configure();
    }

    partial void Configure();
    public InstallationsGroupActions Actions { get; }

    public Task<EmptyActionResult> BeginDelete(GetInstallationRequest requestData, CancellationToken ct = default) => Actions.BeginDelete.Post("", requestData, ct);
    public Task<EmptyActionResult> Deleted(GetInstallationRequest requestData, CancellationToken ct = default) => Actions.Deleted.Post("", requestData, ct);
    public Task<InstallationActivitiesResult> GetInstallationActivities(GetInstallationActivitiesRequest requestData, CancellationToken ct = default) => Actions.GetInstallationActivities.Post("", requestData, ct);
    public Task<InstallationDetailModel> GetInstallationDetail(int requestData, CancellationToken ct = default) => Actions.GetInstallationDetail.Post("", requestData, ct);
    public Task<EmptyActionResult> RequestDelete(GetInstallationRequest requestData, CancellationToken ct = default) => Actions.RequestDelete.Post("", requestData, ct);
    public sealed record InstallationsGroupActions(AppClientPostAction<GetInstallationRequest, EmptyActionResult> BeginDelete, AppClientPostAction<GetInstallationRequest, EmptyActionResult> Deleted, AppClientPostAction<GetInstallationActivitiesRequest, InstallationActivitiesResult> GetInstallationActivities, AppClientPostAction<int, InstallationDetailModel> GetInstallationDetail, AppClientGetAction<InstallationQueryRequest> Index, AppClientGetAction<InstallationViewRequest> Installation, AppClientPostAction<GetInstallationRequest, EmptyActionResult> RequestDelete);
}