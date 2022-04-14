using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XTI_Admin;
using XTI_AdminTool;
using XTI_App.Abstractions;
using XTI_App.Secrets;
using XTI_Core;
using XTI_Core.Extensions;
using XTI_DB;
using XTI_Git;
using XTI_Git.Abstractions;
using XTI_Git.GitLib;
using XTI_Git.Secrets;
using XTI_GitHub;
using XTI_GitHub.Web;
using XTI_Hub;
using XTI_Hub.Abstractions;
using XTI_HubAppClient;
using XTI_HubAppClient.Extensions;
using XTI_HubDB.Extensions;
using XTI_Secrets.Extensions;

await Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration
    (
        (hostContext, configuration) =>
        {
            configuration.UseXtiConfiguration(hostContext.HostingEnvironment, "", "", args);
        }
    )
    .ConfigureServices
    (
        (hostContext, services) =>
        {
            services.AddScoped(sp =>
            {
                var xtiEnvironmentAccessor = new XtiEnvironmentAccessor();
                var hostEnv = sp.GetService<IHostEnvironment>();
                if (hostEnv != null)
                {
                    xtiEnvironmentAccessor.UseEnvironment(hostEnv.EnvironmentName);
                }
                return xtiEnvironmentAccessor;
            });
            services.AddScoped(sp => sp.GetRequiredService<XtiEnvironmentAccessor>().Environment);
            services.AddScoped<XtiFolder>();
            services.AddScoped<IXtiConfiguration>(sp =>
            {
                var xtiEnv = sp.GetRequiredService<XtiEnvironment>();
                var configuration = new ConfigurationBuilder()
                    .UseXtiConfiguration(xtiEnv, "", "", args)
                    .Build();
                return new XtiConfiguration(configuration);
            });
            services.AddFileSecretCredentials();
            services.AddMemoryCache();
            services.AddHttpClient();
            services.AddSingleton<Scopes>();
            services.AddSingleton<IClock, UtcClock>();
            services.AddHubDbContextForSqlServer();
            services.AddScoped(sp =>
            {
                var config = sp.GetRequiredService<IXtiConfiguration>();
                return config.Source.GetSection(DbOptions.DB).Get<DbOptions>();
            });
            services.AddScoped<AppFactory>();
            services.AddScoped<IHashedPasswordFactory, Md5HashedPasswordFactory>();
            services.AddScoped(sp =>
            {
                var config = sp.GetRequiredService<IXtiConfiguration>();
                return config.Source.Get<AdminOptions>();
            });
            services.AddScoped<IGitHubCredentialsAccessor, SecretGitHubCredentialsAccessor>();
            services.AddScoped<GitLibCredentials>();
            services.AddScoped<IGitHubFactory, WebGitHubFactory>();
            services.AddScoped<GitRepoInfo>();
            services.AddScoped(sp =>
            {
                var gitRepoInfo = sp.GetRequiredService<GitRepoInfo>();
                var gitHubFactory = sp.GetRequiredService<IGitHubFactory>();
                return gitHubFactory.CreateGitHubRepository(gitRepoInfo.RepoOwner, gitRepoInfo.RepoName);
            });
            services.AddScoped<IXtiGitFactory, GitLibFactory>();
            services.AddScoped
            (
                sp => sp.GetRequiredService<IXtiGitFactory>().CreateRepository(Environment.CurrentDirectory)
            );
            services.AddScoped(sp => new PublishableFolder(Environment.CurrentDirectory));
            services.AddScoped<SelectedAppKeys>();
            services.AddHubClientServices();
            services.AddScoped<InstallationUserCredentials>();
            services.AddScoped<IInstallationUserCredentials>(sp => sp.GetRequiredService<InstallationUserCredentials>());
            services.AddScoped<InstallationUserXtiToken>();
            services.AddXtiTokenAccessor((sp, tokenAccessor) =>
            {
                tokenAccessor.AddToken(() => sp.GetRequiredService<InstallationUserXtiToken>());
                tokenAccessor.UseToken<InstallationUserXtiToken>();
            });
            services.AddScoped<DbHubAdministration>();
            services.AddScoped<HcHubAdministration>();
            services.AddScoped
            (
                sp =>
                {
                    IHubAdministration hubAdministration;
                    var options = sp.GetRequiredService<AdminOptions>();
                    var hubAdministrationType = options.HubAdministrationType;
                    if (hubAdministrationType == HubAdministrationTypes.Default)
                    {
                        var appKeys = sp.GetRequiredService<SelectedAppKeys>();
                        if (appKeys.Values.Any(appKey => appKey.Equals(HubInfo.AppKey)))
                        {
                            hubAdministrationType = HubAdministrationTypes.DB;
                        }
                        else
                        {
                            hubAdministrationType = HubAdministrationTypes.HubClient;
                        }
                    }
                    if (hubAdministrationType == HubAdministrationTypes.DB)
                    {
                        hubAdministration = sp.GetRequiredService<DbHubAdministration>();
                    }
                    else if (hubAdministrationType == HubAdministrationTypes.HubClient)
                    {
                        hubAdministration = sp.GetRequiredService<HcHubAdministration>();
                    }
                    else
                    {
                        throw new NotSupportedException($"'{hubAdministrationType}' is not supported.");
                    }
                    return hubAdministration;
                }
            );
            services.AddSingleton<CommandFactory>();
            services.AddHostedService<HostedService>();
        }
    )
    .RunConsoleAsync();