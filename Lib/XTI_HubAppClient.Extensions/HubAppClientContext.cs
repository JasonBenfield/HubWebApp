using Microsoft.Extensions.DependencyInjection;
using XTI_App.Abstractions;
using XTI_WebAppClient;

namespace XTI_HubAppClient.Extensions;

internal sealed class HubAppClientContext
{
    public HubAppClientContext(IServiceProvider sp)
    {
        var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
        var xtiTokenAccessor = sp.GetRequiredService<XtiTokenAccessor>();
        var clientVersion = sp.GetRequiredService<HubAppClientVersion>();
        var clientUrl = sp.GetRequiredService<AppClientUrl>();
        var hubClient = new HubAppClient(httpClientFactory, xtiTokenAccessor, clientUrl, clientVersion);
        var appKey = sp.GetRequiredService<AppKey>();
        var versionKey = sp.GetRequiredService<AppVersionKey>();
        AppContext = new HubClientAppContext(hubClient, appKey, versionKey);
        UserContext = new HubClientUserContext(hubClient, AppContext);
    }

    public HubClientAppContext AppContext { get; }
    public HubClientUserContext UserContext { get; }
}