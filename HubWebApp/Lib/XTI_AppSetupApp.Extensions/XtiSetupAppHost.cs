﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_Core;
using XTI_Core.Extensions;
using XTI_HubAppClient;
using XTI_HubAppClient.Extensions;
using XTI_Secrets.Extensions;

namespace XTI_AppSetupApp.Extensions;

public static class XtiSetupAppHost
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
                    var xtiEnv = XtiEnvironment.Parse(hostContext.HostingEnvironment.EnvironmentName);
                    services.AddSingleton(_ => xtiEnv);
                    services.AddSingleton(_ => appKey);
                    services.AddMemoryCache();
                    services.AddFileSecretCredentials(xtiEnv);
                    services.AddHubClientServices();
                    services.AddSystemUserXtiToken();
                    services.AddInstallationUserXtiToken();
                    services.AddXtiTokenAccessor((sp, tokenAccessor) =>
                    {
                        tokenAccessor.AddToken(() => sp.GetRequiredService<SystemUserXtiToken>());
                        tokenAccessor.AddToken(() => sp.GetRequiredService<InstallationUserXtiToken>());
                        tokenAccessor.UseToken<InstallationUserXtiToken>();
                    });
                    services.AddScoped<ISourceAppContext>(sp => sp.GetRequiredService<HcAppContext>());
                    services.AddScoped<ISourceUserContext>(sp => sp.GetRequiredService<HcUserContext>());
                    services.AddConfigurationOptions<SetupOptions>();
                    services.AddScoped<DefaultAppSetup>();
                    services.AddHostedService<SetupHostedService>();
                }
            );
        return builder;
    }
}