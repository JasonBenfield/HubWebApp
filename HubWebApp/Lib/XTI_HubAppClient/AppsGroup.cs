// Generated Code
namespace XTI_HubAppClient;
public sealed partial class AppsGroup : AppClientGroup
{
    public AppsGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "Apps")
    {
        Actions = new AppsGroupActions(Index: CreateGetAction<EmptyRequest>("Index"), GetApps: CreatePostAction<EmptyRequest, AppModel[]>("GetApps"), GetAppDomains: CreatePostAction<EmptyRequest, AppDomainModel[]>("GetAppDomains"));
    }

    public AppsGroupActions Actions { get; }

    public Task<AppModel[]> GetApps() => Actions.GetApps.Post("", new EmptyRequest());
    public Task<AppDomainModel[]> GetAppDomains() => Actions.GetAppDomains.Post("", new EmptyRequest());
    public sealed record AppsGroupActions(AppClientGetAction<EmptyRequest> Index, AppClientPostAction<EmptyRequest, AppModel[]> GetApps, AppClientPostAction<EmptyRequest, AppDomainModel[]> GetAppDomains);
}