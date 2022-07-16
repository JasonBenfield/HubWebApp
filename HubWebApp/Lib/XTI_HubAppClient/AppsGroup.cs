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
    public Task<AppDomainModel[]> GetAppDomains() => Post<AppDomainModel[], EmptyRequest>("GetAppDomains", "", new EmptyRequest());
}