using LocalInstallApp;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XTI_App.Secrets;
using XTI_Configuration.Extensions;
using XTI_Core;
using XTI_Hub;
using XTI_HubAppClient.Extensions;
using XTI_HubDB.Extensions;
using XTI_Secrets.Extensions;
using XTI_WebAppClient;

await Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration
    (
        (hostContext, configuration) =>
        {
            configuration.UseXtiConfiguration(hostContext.HostingEnvironment, args);
        }
    )
    .ConfigureServices
    (
        (hostContext, services) =>
        {
            services.AddMemoryCache();
            services.Configure<InstallOptions>(hostContext.Configuration);
            services.AddSingleton<XtiFolder>();
            services.AddScoped<GitFactory>();
            services.AddFileSecretCredentials(hostContext.HostingEnvironment);
            services.AddSingleton<InstallationUserCredentials>();
            services.AddSingleton<IInstallationUserCredentials>(sp => sp.GetRequiredService<InstallationUserCredentials>());
            services.AddHubClientServices(hostContext.Configuration);
            services.AddScoped(sp =>
            {
                var credentials = sp.GetRequiredService<IInstallationUserCredentials>();
                return new XtiTokenFactory(credentials);
            });
            services.AddHubDbContextForSqlServer(hostContext.Configuration);
            services.AddScoped<AppFactory>();
            services.AddScoped<InstallationServiceFactory>();
            services.AddHostedService<InstallHostedService>();
        }
    )
    .Build()
    .RunAsync();