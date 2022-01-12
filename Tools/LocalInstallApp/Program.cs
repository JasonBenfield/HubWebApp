using LocalInstallApp;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using XTI_App.Abstractions;
using XTI_App.Secrets;
using XTI_Configuration.Extensions;
using XTI_Core;
using XTI_Git.Abstractions;
using XTI_Git.Secrets;
using XTI_GitHub;
using XTI_GitHub.Web;
using XTI_Hub;
using XTI_HubAppClient.Extensions;
using XTI_HubDB.Extensions;
using XTI_Secrets.Extensions;

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
            services.AddFileSecretCredentials(hostContext.HostingEnvironment);
            services.AddScoped<IGitHubCredentialsAccessor, SecretGitHubCredentialsAccessor>();
            services.AddScoped<IGitHubFactory, WebGitHubFactory>();
            services.AddSingleton(sp =>
            {
                var options = sp.GetRequiredService<IOptions<InstallOptions>>().Value;
                return new AppKey(new AppName(options.AppName), AppType.Values.Value(options.AppType));
            });
            services.AddSingleton(sp =>
            {
                var options = sp.GetRequiredService<IOptions<InstallOptions>>().Value;
                return AppVersionKey.Parse(options.VersionKey);
            });
            services.AddSingleton<SystemUserCredentials>();
            services.AddSingleton<ISystemUserCredentials>(sp => sp.GetRequiredService<SystemUserCredentials>());
            services.AddSingleton<InstallationUserCredentials>();
            services.AddSingleton<IInstallationUserCredentials>(sp => sp.GetRequiredService<InstallationUserCredentials>());
            services.AddHubClientServices(hostContext.Configuration);
            services.AddXtiTokenAccessor((sp, tokenAccessor) =>
            {
                tokenAccessor.AddToken(() => sp.GetRequiredService<InstallationUserXtiToken>());
                tokenAccessor.UseToken<InstallationUserXtiToken>();
            });
            services.AddHubDbContextForSqlServer(hostContext.Configuration);
            services.AddScoped<AppFactory>();
            services.AddScoped<InstallationServiceFactory>();
            services.AddHostedService<InstallHostedService>();
        }
    )
    .Build()
    .RunAsync();