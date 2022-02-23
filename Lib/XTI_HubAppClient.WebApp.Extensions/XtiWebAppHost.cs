using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_HubAppClient.Extensions;
using XTI_App.Extensions;
using XTI_Core.Extensions;

namespace XTI_HubAppClient.WebApp.Extensions;

public static class XtiWebAppHost
{
    public static WebApplicationBuilder CreateDefault(AppKey appKey, string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Configuration.UseXtiConfiguration(builder.Environment, appKey.Name.DisplayText, appKey.Type.DisplayText, args);
        builder.Services.AddResponseCaching();
        builder.Services.AddAppServices();
        builder.Services.AddHubClientServices();
        XTI_WebApp.Extensions.WebAppExtensions.AddWebAppServices(builder.Services);
        builder.Services.AddAppClients((sp, domains) => { });
        builder.Services.AddAppClientDomainSelector((sp, domains) => { });
        builder.Services.AddScoped<ISourceAppContext>(sp => sp.GetRequiredService<HubClientAppContext>());
        builder.Services.AddScoped<ISourceUserContext>(sp => sp.GetRequiredService<HubClientUserContext>());
        return builder;
    }
}
