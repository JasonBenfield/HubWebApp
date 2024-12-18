// Generated Code
namespace XTI_HubAppClient;
public sealed partial class AppsGroup : AppClientGroup
{
    public AppsGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "Apps")
    {
        Actions = new AppsGroupActions(GetAppDomains: CreatePostAction<EmptyRequest, AppDomainModel[]>("GetAppDomains"), GetApps: CreatePostAction<EmptyRequest, AppModel[]>("GetApps"), Index: CreateGetAction<EmptyRequest>("Index"));
    }

    public AppsGroupActions Actions { get; }

    public Task<AppDomainModel[]> GetAppDomains(CancellationToken ct = default) => Actions.GetAppDomains.Post("", new EmptyRequest(), ct);
    public Task<AppModel[]> GetApps(CancellationToken ct = default) => Actions.GetApps.Post("", new EmptyRequest(), ct);
    public sealed record AppsGroupActions(AppClientPostAction<EmptyRequest, AppDomainModel[]> GetAppDomains, AppClientPostAction<EmptyRequest, AppModel[]> GetApps, AppClientGetAction<EmptyRequest> Index);
}