﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_App.Extensions;
using XTI_App.Hosting;
using XTI_App.Secrets;
using XTI_ConsoleApp;
using XTI_Core;
using XTI_Core.Extensions;
using XTI_HubAppClient.Extensions;
using XTI_Secrets.Extensions;
using XTI_TempLog;
using XTI_TempLog.Abstractions;
using XTI_TempLog.Extensions;

namespace XTI_HubAppClient.ConsoleApp.Extensions;

public sealed class XtiConsoleAppHost
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
                    AppExtensions.AddThrottledLog(services, (api, b) => { });
                    var xtiEnv = XtiEnvironment.Parse(hostContext.HostingEnvironment.EnvironmentName);
                    services.AddFileSecretCredentials(xtiEnv);
                    services.AddScoped(sp =>
                    {
                        var xtiFolder = sp.GetRequiredService<XtiFolder>();
                        var appKey = sp.GetRequiredService<AppKey>();
                        return xtiFolder.AppDataFolder(appKey);
                    });
                    services.AddScoped<StopApplicationAction>();
                    services.AddSingleton<CurrentSession>();
                    services.AddScoped<IActionRunnerFactory, ActionRunnerFactory>();
                    services.AddSingleton<ISystemUserCredentials, SystemUserCredentials>();
                    services.AddSingleton<ICurrentUserName, SystemCurrentUserName>();
                    services.AddSingleton<IAppEnvironmentContext, AppEnvironmentContext>();
                    services.AddAppAgenda((sp, a) => { });
                    services.AddHostedService<AppAgendaHostedService>();
                    services.AddHubClientServices();
                    services.AddHubClientContext();
                    services.AddScoped<SystemUserXtiToken>();
                    services.AddXtiTokenAccessorFactory((sp, tokenAccessorFactory) =>
                    {
                        tokenAccessorFactory.AddToken(() => sp.GetRequiredService<SystemUserXtiToken>());
                        tokenAccessorFactory.UseDefaultToken<SystemUserXtiToken>();
                    });
                    services.AddScoped<ISourceAppContext>(sp => sp.GetRequiredService<HcAppContext>());
                    services.AddScoped<ISourceUserContext>(sp => sp.GetRequiredService<HcUserContext>());
                    services.AddScoped<IAppApiUser, AppApiSuperUser>();
                    services.AddTempLogWriterHostedService();
                }
            );
        return builder;
    }
}
