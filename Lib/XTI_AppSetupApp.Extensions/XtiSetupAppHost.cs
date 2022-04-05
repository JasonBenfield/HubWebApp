using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_App.Secrets;
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
                    services.AddMemoryCache();
                    services.AddFileSecretCredentials();
                    services.AddHubClientServices();
                    services.AddScoped<InstallationUserCredentials>();
                    services.AddScoped<IInstallationUserCredentials>(sp => sp.GetRequiredService<InstallationUserCredentials>());
                    services.AddScoped<InstallationUserXtiToken>();
                    services.AddXtiTokenAccessor((sp, tokenAccessor) =>
                    {
                        tokenAccessor.AddToken(() => sp.GetRequiredService<InstallationUserXtiToken>());
                        tokenAccessor.UseToken<InstallationUserXtiToken>();
                    });
                    services.AddScoped<ISourceAppContext>(sp => sp.GetRequiredService<HubClientAppContext>());
                    services.AddScoped<ISourceUserContext>(sp => sp.GetRequiredService<HubClientUserContext>());
                    services.AddConfigurationOptions<SetupOptions>();
                    services.AddScoped(sp =>
                    {
                        var options = sp.GetRequiredService<IOptions<SetupOptions>>().Value;
                        return new VersionReader(options.VersionsPath);
                    });
                    services.AddScoped<DefaultAppSetup>();
                    services.AddHostedService<SetupHostedService>();
                }
            );
        return builder;
    }
}