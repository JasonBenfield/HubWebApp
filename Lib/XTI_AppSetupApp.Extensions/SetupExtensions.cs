using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using XTI_App.Api;
using XTI_App.Secrets;
using XTI_Core;
using XTI_HubAppClient;
using XTI_HubAppClient.Extensions;
using XTI_Secrets.Extensions;

namespace XTI_AppSetupApp.Extensions;

public static class SetupExtensions
{
    public static void AddAppSetupServices(this IServiceCollection services, IHostEnvironment hostEnv, IConfiguration config)
    {
        services.AddMemoryCache();
        services.AddSingleton<XtiFolder>();
        services.AddFileSecretCredentials(hostEnv);
        services.AddSingleton<InstallationUserCredentials>();
        services.AddSingleton<IInstallationUserCredentials>(sp => sp.GetRequiredService<InstallationUserCredentials>());
        services.AddScoped<InstallationUserXtiToken>();
        services.AddSingleton<SystemUserCredentials>();
        services.AddSingleton<ISystemUserCredentials>(sp => sp.GetRequiredService<SystemUserCredentials>());
        services.AddHubClientServices(config);
        services.AddScoped<ISourceAppContext>(sp => sp.GetRequiredService<HubClientAppContext>());
        services.AddScoped<ISourceUserContext>(sp => sp.GetRequiredService<HubClientUserContext>());
        services.AddXtiTokenAccessor((sp, tokenAccessor) =>
        {
            tokenAccessor.AddToken(() => sp.GetRequiredService<InstallationUserXtiToken>());
            tokenAccessor.UseToken<InstallationUserXtiToken>();
        });
        services.Configure<SetupOptions>(config);
        services.AddScoped(sp =>
        {
            var options = sp.GetRequiredService<IOptions<SetupOptions>>().Value;
            return new VersionReader(options.VersionsPath);
        });
        services.AddScoped<DefaultAppSetup>();
        services.AddHostedService<SetupHostedService>();
    }
}