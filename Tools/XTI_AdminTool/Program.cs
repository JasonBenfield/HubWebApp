using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Caching.Memory;
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
using XTI_PermanentLog;
using XTI_Secrets;
using XTI_Secrets.Extensions;
using XTI_Secrets.Files;
using XTI_TempLog;
using XTI_TempLog.Abstractions;
using XTI_TempLog.Extensions;
using XTI_WebAppClient;

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
            services.AddXtiDataProtection();
            services.AddScoped<ISecretCredentialsFactory>(sp =>
            {
                var xtiFolder = sp.GetRequiredService<XtiFolder>();
                var dataProtector = sp.GetDataProtector(new[] { "XTI_Secrets" });
                return new FileSecretCredentialsFactory(xtiFolder, dataProtector);
            });
            services.AddScoped(sp => (SecretCredentialsFactory)sp.GetRequiredService<ISecretCredentialsFactory>());
            services.AddScoped<ISharedSecretCredentialsFactory>(sp =>
            {
                var xtiFolder = sp.GetRequiredService<XtiFolder>();
                var dataProtector = sp.GetDataProtector(new[] { "XTI_Secrets" });
                return new SharedFileSecretCredentialsFactory(xtiFolder, dataProtector);
            });
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
            services.AddScoped<HubFactory>();
            services.AddScoped<IHashedPasswordFactory, Md5HashedPasswordFactory>();
            services.AddScoped(sp =>
            {
                var config = sp.GetRequiredService<IXtiConfiguration>();
                return config.Source.Get<AdminOptions>();
            });
            var slnDir = Environment.CurrentDirectory;
            services.AddScoped(sp => new GitRepoInfo(sp.GetRequiredService<AdminOptions>(), slnDir));
            services.AddScoped<AppVersionNameAccessor>();
            services.AddScoped<IGitHubCredentialsAccessor, SecretGitHubCredentialsAccessor>();
            services.AddScoped<GitLibCredentials>();
            services.AddScoped<IGitHubFactory, WebGitHubFactory>();
            services.AddScoped(sp =>
            {
                var gitRepoInfo = sp.GetRequiredService<GitRepoInfo>();
                var gitHubFactory = sp.GetRequiredService<IGitHubFactory>();
                return gitHubFactory.CreateGitHubRepository(gitRepoInfo.RepoOwner, gitRepoInfo.RepoName);
            });
            services.AddScoped<IXtiGitFactory, GitLibFactory>();
            services.AddScoped
            (
                sp => sp.GetRequiredService<IXtiGitFactory>().CreateRepository(slnDir)
            );
            services.AddScoped(sp => new SlnFolder(sp.GetRequiredService<XtiEnvironment>(), slnDir));
            services.AddScoped<InstallOptionsAccessor>();
            services.AddScoped<SelectedAppKeys>();
            services.AddScoped<ITempLogs>(sp =>
            {
                var dataProtector = sp.GetDataProtector("XTI_TempLog");
                var appDataFolder = sp.GetRequiredService<XtiFolder>().AppDataFolder();
                return new DiskTempLogs(dataProtector, appDataFolder.Path(), "TempLogs");
            });
            services.AddScoped<PublishedFolder>();
            services.AddScoped<FolderPublishedAssets>();
            services.AddScoped<GitHubPublishedAssets>();
            services.AddTransient(sp =>
            {
                var options = sp.GetRequiredService<AdminOptions>();
                var xtiEnv = sp.GetRequiredService<XtiEnvironment>();
                var installationSource = options.GetInstallationSource(xtiEnv);
                IPublishedAssets publishedAssets;
                if (installationSource == InstallationSources.Folder)
                {
                    publishedAssets = sp.GetRequiredService<FolderPublishedAssets>();
                }
                else if (installationSource == InstallationSources.GitHub)
                {
                    publishedAssets = sp.GetRequiredService<GitHubPublishedAssets>();
                }
                else
                {
                    throw new NotSupportedException($"Installation Source {installationSource} is not supported");
                }
                return publishedAssets;
            });
            services.AddHubClientServices();
            var existingTokenAccessor = services.FirstOrDefault(s => s.ImplementationType == typeof(XtiTokenAccessor));
            if (existingTokenAccessor != null)
            {
                services.Remove(existingTokenAccessor);
            }
            services.AddScoped
            (
                sp =>
                {
                    var cache = sp.GetRequiredService<IMemoryCache>();
                    var xtiEnv = sp.GetRequiredService<XtiEnvironment>();
                    var xtiTokenAccessor = new XtiTokenAccessor(cache, xtiEnv.EnvironmentName);
                    xtiTokenAccessor.AddToken(() => sp.GetRequiredService<InstallationUserXtiToken>());
                    xtiTokenAccessor.UseToken<InstallationUserXtiToken>();
                    return xtiTokenAccessor;
                }
            );
            services.AddScoped<IPermanentLogClient, PermanentLogClient>();
            services.AddScoped
            (
                sp => new TempToPermanentLog
                (
                    sp.GetRequiredService<ITempLogs>(),
                    sp.GetRequiredService<IPermanentLogClient>(),
                    sp.GetRequiredService<IClock>(),
                    0
                )
            );
            services.AddScoped(sp =>
            {
                var config = sp.GetRequiredService<IXtiConfiguration>();
                return config.Source.GetSection(HubClientOptions.HubClient).Get<HubClientOptions>();
            });
            services.AddScoped<InstallationUserCredentials>();
            services.AddScoped<IInstallationUserCredentials>(sp => sp.GetRequiredService<InstallationUserCredentials>());
            services.AddScoped<InstallationUserXtiToken>();
            services.AddScoped<DbHubAdministration>();
            services.AddScoped<HcHubAdministration>();
            services.AddScoped
            (
                sp =>
                {
                    IHubAdministration hubAdministration;
                    var options = sp.GetRequiredService<AdminOptions>();
                    if (options.HubAdministrationType == HubAdministrationTypes.Default)
                    {
                        var appKeys = sp.GetRequiredService<SelectedAppKeys>();
                        if (appKeys.Values.Any(appKey => appKey.Equals(HubInfo.AppKey)))
                        {
                            options.HubAdministrationType = HubAdministrationTypes.DB;
                        }
                        else
                        {
                            options.HubAdministrationType = HubAdministrationTypes.HubClient;
                        }
                    }
                    if (options.HubAdministrationType == HubAdministrationTypes.DB)
                    {
                        hubAdministration = sp.GetRequiredService<DbHubAdministration>();
                    }
                    else if (options.HubAdministrationType == HubAdministrationTypes.HubClient)
                    {
                        hubAdministration = sp.GetRequiredService<HcHubAdministration>();
                    }
                    else
                    {
                        throw new NotSupportedException($"'{options.HubAdministrationType}' is not supported.");
                    }
                    return hubAdministration;
                }
            );
            services.AddSingleton<CommandFactory>();
            services.AddHostedService<HostedService>();
        }
    )
    .RunConsoleAsync();