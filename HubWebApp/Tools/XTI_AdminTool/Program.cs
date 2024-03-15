using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XTI_Admin;
using XTI_AdminTool;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_App.Extensions;
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
using XTI_HubDB.EF;
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
            services.AddKeyedScoped<IConfiguration>
            (
                "XTI",
                (sp, key) =>
                {
                    var xtiEnv = sp.GetRequiredService<XtiEnvironment>();
                    return new ConfigurationBuilder()
                        .UseXtiConfiguration(xtiEnv, "", "", args)
                        .Build();
                }
            );
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
            services.AddConfigurationOptions<AdminToolOptions>();
            services.AddScoped<DbAdmin<HubDbContext>>();
            services.AddScoped(sp =>
            {
                var config = sp.GetRequiredKeyedService<IConfiguration>("XTI");
                return config.Get<AdminToolOptions>()?.DB ?? new DbOptions();
            });
            services.AddScoped<HubFactory>();
            services.AddScoped<IHashedPasswordFactory, Md5HashedPasswordFactory>();
            services.AddSingleton<IAppClientRequestKey, EmptyAppClientRequestKey>();
            services.AddScoped(sp =>
            {
                var config = sp.GetRequiredKeyedService<IConfiguration>("XTI");
                return config.Get<AdminOptions>() ?? new AdminOptions();
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
            services.AddScoped(sp => new SlnFolder(slnDir));
            services.AddScoped<SelectedAppKeys>();
            services.AddScoped<ITempLogs>(sp =>
            {
                var dataProtector = sp.GetDataProtector("XTI_TempLog");
                var appDataFolder = sp.GetRequiredService<XtiFolder>().AppDataFolder();
                return new DiskTempLogs(dataProtector, appDataFolder.Path(), "TempLogs");
            });
            services.AddScoped(sp =>
            {
                var xtiFolder = sp.GetRequiredService<XtiFolder>();
                var xtiEnv = sp.GetRequiredService<XtiEnvironment>();
                if (xtiEnv.IsTest())
                {
                    xtiEnv = XtiEnvironment.Development;
                }
                else if (xtiEnv.IsStaging())
                {
                    xtiEnv = XtiEnvironment.Production;
                }
                var appVersionNameAccessor = sp.GetRequiredService<AppVersionNameAccessor>();
                return new PublishedFolder(xtiFolder, xtiEnv, appVersionNameAccessor);
            });
            services.AddScoped<PublishedAssetsFactory>();
            services.AddHubClientServices();
            services.AddSingleton(sp =>
            {
                var scopes = sp.GetRequiredService<Scopes>();
                var options = scopes.GetRequiredService<AdminOptions>();
                return string.IsNullOrWhiteSpace(options.HubAppVersionKey)
                    ? HubAppClientVersion.Version(AppVersionKey.Current.DisplayText)
                    : HubAppClientVersion.Version(options.HubAppVersionKey);
            });
            var existingTokenAccessor = services.FirstOrDefault(s => !s.IsKeyedService && s.ImplementationType == typeof(XtiTokenAccessor));
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
                    var xtiTokenAccessorFactory = new XtiTokenAccessorFactory(cache);
                    xtiTokenAccessorFactory.SetDefaultIdentifier(xtiEnv.EnvironmentName);
                    xtiTokenAccessorFactory.AddToken(() => sp.GetRequiredService<InstallationUserXtiToken>());
                    xtiTokenAccessorFactory.UseDefaultToken<InstallationUserXtiToken>();
                    xtiTokenAccessorFactory.AddToken(() => sp.GetRequiredService<AnonymousXtiToken>());
                    return xtiTokenAccessorFactory;
                }
            );
            services.AddScoped<IAdminTokenAccessor, AdminTokenAccessor>();
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
                var config = sp.GetRequiredKeyedService<IConfiguration>("XTI");
                return config.Get<AdminToolOptions>()?.HubClient ?? new HubClientOptions();
            });
            services.AddScoped<InstallationUserCredentials>();
            services.AddScoped<IInstallationUserCredentials>(sp => sp.GetRequiredService<InstallationUserCredentials>());
            services.AddScoped<InstallationUserXtiToken>();
            services.AddScoped<EfHubAdministration>();
            services.AddScoped<HcHubAdministration>();
            services.AddScoped
            (
                sp =>
                {
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
                    return new HubDbTypeAccessor(hubAdministrationType);
                }
            );
            services.AddScoped
            (
                sp =>
                {
                    IHubAdministration hubAdministration;
                    var hubDbType = sp.GetRequiredService<HubDbTypeAccessor>().Value;
                    if (hubDbType == HubAdministrationTypes.DB)
                    {
                        hubAdministration = sp.GetRequiredService<EfHubAdministration>();
                    }
                    else if (hubDbType == HubAdministrationTypes.HubClient)
                    {
                        hubAdministration = sp.GetRequiredService<HcHubAdministration>();
                    }
                    else
                    {
                        throw new NotSupportedException($"'{hubDbType}' is not supported.");
                    }
                    return hubAdministration;
                }
            );
            services.AddScoped<EfStoredObjectDB>();
            services.AddScoped<HcStoredObjectDB>();
            services.AddScoped
            (
                sp =>
                {
                    IStoredObjectDB storedObjectDB;
                    var hubDbType = sp.GetRequiredService<HubDbTypeAccessor>().Value;
                    if (hubDbType == HubAdministrationTypes.DB)
                    {
                        storedObjectDB = sp.GetRequiredService<EfStoredObjectDB>();
                    }
                    else if (hubDbType == HubAdministrationTypes.HubClient)
                    {
                        storedObjectDB = sp.GetRequiredService<HcStoredObjectDB>();
                    }
                    else
                    {
                        throw new NotSupportedException($"'{hubDbType}' is not supported.");
                    }
                    return storedObjectDB;
                }
            );
            services.AddScoped<StoredObjectFactory>();
            services.AddScoped<RemoteCommandService>();
            services.AddScoped
            (
                sp => new ProductionHubAdmin
                (
                    sp.GetRequiredService<Scopes>().Production().GetRequiredService<IHubAdministration>()
                )
            );
            services.AddScoped<CurrentVersion>();
            services.AddScoped<VersionKeyFromCurrentBranch>();
            services.AddScoped<PublishLibProcess>();
            services.AddScoped<InstallWebAppProcess>();
            services.AddScoped<InstallServiceAppProcess>();
            services.AddScoped<LocalInstallProcess>();
            services.AddScoped<InstallProcess>();
            services.AddScoped<BranchVersion>();
            services.AddScoped<BuildProcess>();
            services.AddScoped<PublishProcess>();
            services.AddScoped<PublishSetupProcess>();
            services.AddScoped<BeginPublishProcess>();
            services.AddScoped<CompleteVersionProcess>();
            services.AddSingleton<CommandFactory>();
            services.AddHostedService<HostedService>();
        }
    )
    .RunConsoleAsync();