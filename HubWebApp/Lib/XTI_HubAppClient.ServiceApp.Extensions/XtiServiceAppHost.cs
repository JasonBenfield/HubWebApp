using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_App.Extensions;
using XTI_App.Hosting;
using XTI_App.Secrets;
using XTI_Core;
using XTI_Core.Extensions;
using XTI_HubAppClient.Extensions;
using XTI_Secrets.Extensions;
using XTI_TempLog;

namespace XTI_HubAppClient.ServiceApp.Extensions;

public static class XtiServiceAppHost
{
    public static IHostBuilder CreateDefault(AppKey appKey, string[] args)
    {
        var builder = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration
            (
                (hostContext, config) => config.UseXtiConfiguration(hostContext.HostingEnvironment, appKey.Name.DisplayText, appKey.Type.DisplayText, args)
            )
            .ConfigureServices
            (
                (hostContext, services) =>
                {
                    services.AddSingleton(_ => appKey);
                    services.AddAppServices();
                    services.AddFileSecretCredentials(XtiEnvironment.Parse(hostContext.HostingEnvironment.EnvironmentName));
                    services.AddScoped(sp =>
                    {
                        var xtiFolder = sp.GetRequiredService<XtiFolder>();
                        var appKey = sp.GetRequiredService<AppKey>();
                        return xtiFolder.AppDataFolder(appKey);
                    });
                    services.AddSingleton<CurrentSession>();
                    services.AddScoped<ActionRunnerXtiPathAccessor>();
                    services.AddScoped<IXtiPathAccessor>(sp => sp.GetRequiredService<ActionRunnerXtiPathAccessor>());
                    services.AddScoped<IActionRunnerFactory, ActionRunnerFactory>();
                    services.AddSingleton<ISystemUserCredentials, SystemUserCredentials>();
                    services.AddSingleton<ICurrentUserName, SystemCurrentUserName>();
                    services.AddSingleton<IAppEnvironmentContext, AppEnvironmentContext>();
                    services.AddHostedService<AppAgendaHostedService>();
                    services.AddHubClientServices();
                    services.AddScoped<SystemUserXtiToken>();
                    services.AddXtiTokenAccessor((sp, tokenAccessor) =>
                    {
                        tokenAccessor.AddToken(() => sp.GetRequiredService<SystemUserXtiToken>());
                        tokenAccessor.UseToken<SystemUserXtiToken>();
                    });
                    services.AddScoped<ISourceAppContext>(sp => sp.GetRequiredService<HcAppContext>());
                    services.AddScoped<ISourceUserContext>(sp => sp.GetRequiredService<HcUserContext>());
                }
            );
        return builder;
    }
}
