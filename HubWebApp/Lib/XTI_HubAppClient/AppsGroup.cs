// Generated Code
namespace XTI_HubAppClient;
public sealed partial class AppsGroup : AppClientGroup
{
    public AppsGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "Apps")
    {
        Actions = new AppsGroupActions(Index: CreateGetAction<EmptyRequest>("Index"), GetApps: CreatePostAction<EmptyRequest, AppModel[]>("GetApps"), GetAppDomains: CreatePostAction<EmptyRequest, AppDomainModel[]>("GetAppDomains"));
    }

    public AppsGroupActions Actions { get; }

    public Task<AppModel[]> GetApps(CancellationToken ct = default) => Actions.GetApps.Post("", new EmptyRequest(), ct);
    public Task<AppDomainModel[]> GetAppDomains(CancellationToken ct = default) => Actions.GetAppDomains.Post("", new EmptyRequest(), ct);
    public sealed record AppsGroupActions(AppClientGetAction<EmptyRequest> Index, AppClientPostAction<EmptyRequest, AppModel[]> GetApps, AppClientPostAction<EmptyRequest, AppDomainModel[]> GetAppDomains);
}