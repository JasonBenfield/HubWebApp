using HubAppClientTest;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XTI_App.Secrets;
using XTI_Core;
using XTI_Core.Extensions;
using XTI_HubAppClient.Extensions;
using XTI_Secrets.Extensions;

await Host.CreateDefaultBuilder()
    .ConfigureAppConfiguration
    (
        (hostContext, config) =>
        {
            config.UseXtiConfiguration(hostContext.HostingEnvironment, "", "", new string[0]);
        }
    )
    .ConfigureServices
    (
        (hostContext, services) =>
        {
            services.AddHttpClient();
            services.AddMemoryCache();
            services.AddFileSecretCredentials(XtiEnvironment.Parse(hostContext.HostingEnvironment.EnvironmentName));
            services.AddHubClientServices();
            services.AddScoped<IInstallationUserCredentials, InstallationUserCredentials>();
            services.AddScoped<InstallationUserXtiToken>();
            services.AddXtiTokenAccessor((sp, tokenAccessor) =>
            {
                tokenAccessor.AddToken(() => sp.GetRequiredService<InstallationUserXtiToken>());
                tokenAccessor.UseToken<InstallationUserXtiToken>();
            });
            services.AddHostedService<HostedService>();
        }
    )
    .RunConsoleAsync();