using Microsoft.Extensions.Caching.Memory;
using XTI_App.Abstractions;
using XTI_Hub.Abstractions;
using XTI_WebApp.Abstractions;

namespace XTI_HubAppClient.Extensions;

internal sealed class HubClientAppClientDomain : IAppClientDomain
{
    private readonly IMemoryCache cache;
    private readonly HubAppClient hubClient;

    public HubClientAppClientDomain(IMemoryCache cache, HubAppClient hubClient)
    {
        this.cache = cache;
        this.hubClient = hubClient;
    }

    public async Task<string> Value(string appName, string version)
    {
        if (!cache.TryGetValue<AppDomainModel[]>("appDomains", out var appDomains))
        {
            appDomains = await hubClient.Apps.GetAppDomains();
            cache.Set("appDomains", appDomains, TimeSpan.FromHours(1));
        }
        var appKey = AppKey.WebApp(appName);
        var appDomain = appDomains.FirstOrDefault
        (
            ad => ad.AppKey.Equals(appKey)
        );
        return appDomain?.Domain ?? "";
    }
}