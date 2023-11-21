using HubAppClientTest;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XTI_App.Secrets;
using XTI_Core;
using XTI_Core.Extensions;
using XTI_HubAppClient.Extensions;
using XTI_Secrets.Extensions;

await Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration
    (
        (hostContext, config) =>
        {
            config.UseXtiConfiguration(hostContext.HostingEnvironment, "", "", args);
        }
    )
    .ConfigureServices
    (
        (hostContext, services) =>
        {
            services.AddHttpClient();
            services.AddMemoryCache();
            var xtiEnv = XtiEnvironment.Parse(hostContext.HostingEnvironment.EnvironmentName);
            services.AddSingleton(_ => xtiEnv);
            services.AddFileSecretCredentials(xtiEnv);
            services.AddHubClientServices();
            services.AddScoped<IInstallationUserCredentials, InstallationUserCredentials>();
            services.AddScoped<InstallationUserXtiToken>();
            services.AddXtiTokenAccessorFactory((sp, tokenAccessorFactory) =>
            {
                tokenAccessorFactory.AddToken(() => sp.GetRequiredService<InstallationUserXtiToken>());
                tokenAccessorFactory.UseDefaultToken<InstallationUserXtiToken>();
            });
            services.AddHostedService<HostedService>();
        }
    )
    .RunConsoleAsync();