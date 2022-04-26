﻿using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using XTI_App.Abstractions;
using XTI_WebAppClient;

namespace XTI_HubAppClient.Extensions;

public sealed class HubAppClientContext
{
    public HubAppClientContext(IServiceProvider sp)
    {
        var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
        var clientVersion = sp.GetRequiredService<HubAppClientVersion>();
        var clientUrl = sp.GetRequiredService<AppClientUrl>();
        var cache = sp.GetRequiredService<IMemoryCache>();
        var xtiTokenAccessor = new XtiTokenAccessor(cache);
        xtiTokenAccessor.AddToken(() => sp.GetRequiredService<SystemUserXtiToken>());
        xtiTokenAccessor.UseToken<SystemUserXtiToken>();
        var hubClient = new HubAppClient(httpClientFactory, xtiTokenAccessor, clientUrl, clientVersion);
        var appKey = sp.GetRequiredService<AppKey>();
        var versionKey = sp.GetRequiredService<AppVersionKey>();
        AppContext = new HcAppContext(hubClient, appKey, versionKey);
        UserContext = new HcUserContext(hubClient, AppContext);
    }

    public HcAppContext AppContext { get; }

    public HcUserContext UserContext { get; }
}