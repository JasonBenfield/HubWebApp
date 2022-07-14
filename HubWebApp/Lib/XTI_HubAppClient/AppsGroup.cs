// Generated Code
namespace XTI_HubAppClient;
public sealed partial class AppsGroup : AppClientGroup
{
    public AppsGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl) : base(httpClientFactory, xtiTokenAccessor, clientUrl, "Apps")
    {
        Actions = new AppsActions(clientUrl);
    }

    public AppsActions Actions { get; }

    public Task<AppWithModKeyModel[]> GetApps() => Post<AppWithModKeyModel[], EmptyRequest>("GetApps", "", new EmptyRequest());
    public Task<AppWithModKeyModel> GetAppByID(GetAppByIDRequest model) => Post<AppWithModKeyModel, GetAppByIDRequest>("GetAppByID", "", model);
    public Task<AppWithModKeyModel> GetAppByAppKey(GetAppByAppKeyRequest model) => Post<AppWithModKeyModel, GetAppByAppKeyRequest>("GetAppByAppKey", "", model);
    public Task<AppDomainModel[]> GetAppDomains() => Post<AppDomainModel[], EmptyRequest>("GetAppDomains", "", new EmptyRequest());
}