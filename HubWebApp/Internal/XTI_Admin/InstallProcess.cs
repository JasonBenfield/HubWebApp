using XTI_App.Abstractions;
using XTI_Core;
using XTI_Credentials;
using XTI_GitHub;
using XTI_Hub.Abstractions;

namespace XTI_Admin;

public sealed class InstallProcess
{
    private readonly AdminOptions options;
    private readonly SelectedAppKeys selectedAppKeys;
    private readonly AppVersionNameAccessor versionNameAccessor;
    private readonly XtiEnvironment xtiEnv;
    private readonly XtiGitHubRepository gitHubRepo;
    private readonly IHubAdministration hubAdministration;
    private readonly GitRepoInfo gitRepoInfo;
    private readonly PublishedAssetsFactory publishedAssetsFactory;
    private readonly RemoteCommandService remoteCommandService;
    private readonly LocalInstallProcess localInstallProcess;

    public InstallProcess(AdminOptions options, SelectedAppKeys selectedAppKeys, AppVersionNameAccessor versionNameAccessor, XtiEnvironment xtiEnv, XtiGitHubRepository gitHubRepo, IHubAdministration hubAdministration, GitRepoInfo gitRepoInfo, PublishedAssetsFactory publishedAssetsFactory, RemoteCommandService remoteCommandService, LocalInstallProcess localInstallProcess)
    {
        this.options = options;
        this.selectedAppKeys = selectedAppKeys;
        this.versionNameAccessor = versionNameAccessor;
        this.xtiEnv = xtiEnv;
        this.gitHubRepo = gitHubRepo;
        this.hubAdministration = hubAdministration;
        this.gitRepoInfo = gitRepoInfo;
        this.publishedAssetsFactory = publishedAssetsFactory;
        this.remoteCommandService = remoteCommandService;
        this.localInstallProcess = localInstallProcess;
    }

    public async Task Run(CancellationToken ct)
    {
        var appKeys = selectedAppKeys.Values
            .Where(a => !a.Type.Equals(AppType.Values.Package) && !a.Type.Equals(AppType.Values.WebPackage))
            .ToArray();
        if (appKeys.Any())
        {
            Console.WriteLine("Beginning Install");
            var versionName = versionNameAccessor.Value;
            using var publishedAssets = publishedAssetsFactory.Create(options.GetInstallationSource(xtiEnv));
            string release;
            if (publishedAssets is GitHubPublishedAssets)
            {
                var versionNumber = options.VersionNumber;
                if (string.IsNullOrWhiteSpace(versionNumber))
                {
                    var latestRelease = await gitHubRepo.LatestRelease();
                    release = latestRelease.TagName;
                }
                else
                {
                    release = $"v{versionNumber}";
                }
            }
            else
            {
                release = "";
            }
            var versionsPath = await publishedAssets.LoadVersions(release);
            var versionReader = new VersionReader(versionsPath);
            var versions = await versionReader.Versions();
            Console.WriteLine("Adding or updating apps");
            await hubAdministration.AddOrUpdateApps(versionName, appKeys, ct);
            Console.WriteLine("Adding or updating versions");
            await hubAdministration.AddOrUpdateVersions
            (
                appKeys,
                versions.Select(v => new AddVersionRequest(v)).ToArray(),
                ct
            );
            var versionKey = string.IsNullOrWhiteSpace(options.VersionKey) ? AppVersionKey.Current : AppVersionKey.Parse(options.VersionKey);
            if (xtiEnv.IsProduction() && versionKey.IsCurrent())
            {
                versionKey = versions.First(v => v.IsCurrent()).VersionKey;
            }
            var installConfigs = await hubAdministration.InstallConfigurations
            (
                new GetInstallConfigurationsRequest
                (
                    repoOwner: gitRepoInfo.RepoOwner,
                    repoName: gitRepoInfo.RepoName,
                    configurationName: options.InstallConfigurationName
                ),
                ct
            );
            var configurationNames = installConfigs
                .Select(c => c.ConfigurationName)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToArray();
            var installConfigsWithAppKey =
                configurationNames.SelectMany
                (
                    configName => appKeys
                        .Select
                        (
                            a =>
                            {
                                return new InstallConfigurationWithAppKey
                                (
                                    installConfigs.FirstOrDefault(c => c.IsMatch(a, configName)) ??
                                    new(),
                                    a
                                );
                            }
                        )
                        .OrderBy(c => c.Config.InstallSequence)
                );
            foreach (var installConfig in installConfigsWithAppKey)
            {
                if (installConfig.Config.IsFound())
                {
                    var isLocal = string.IsNullOrWhiteSpace(installConfig.Config.Template.DestinationMachineName);
                    var installMachineName = isLocal ?
                        GetLocalMachineName() :
                        installConfig.Config.Template.DestinationMachineName;
                    var instResult = await NewInstallation
                    (
                        installConfig.AppKey,
                        installMachineName,
                        versionName,
                        installConfig.Config.Template.Domain,
                        installConfig.Config.Template.SiteName,
                        ct
                    );
                    var installerCreds = await GetInstallerCredentials(hubAdministration, installMachineName, ct);
                    if (isLocal)
                    {
                        var adminInstallOptions = new AdminInstallOptions
                        (
                            AppKey: installConfig.AppKey,
                            VersionKey: versionKey,
                            RepoOwner: gitRepoInfo.RepoOwner,
                            RepoName: gitRepoInfo.RepoName,
                            Release: release,
                            CurrentInstallationID: instResult.CurrentInstallationID,
                            VersionInstallationID: instResult.VersionInstallationID,
                            InstallerUserName: installerCreds.UserName,
                            InstallerPassword: installerCreds.Password,
                            DestinationMachineName: installConfig.Config.Template.DestinationMachineName,
                            Domain: installConfig.Config.Template.Domain,
                            SiteName: installConfig.Config.Template.SiteName
                        );
                        await localInstallProcess.Run(adminInstallOptions, publishedAssets, ct);
                    }
                    else
                    {
                        var remoteOptions = options.Copy();
                        remoteOptions.DestinationMachine = "";
                        remoteOptions.Domain = installConfig.Config.Template.Domain;
                        remoteOptions.SiteName = installConfig.Config.Template.SiteName;
                        await remoteCommandService.Run
                        (
                            installConfig.Config.Template.DestinationMachineName,
                            CommandNames.FromRemote.ToString(),
                            remoteOptions
                        );
                    }
                    await Task.Delay(TimeSpan.FromSeconds(15));
                }
                else
                {
                    Console.WriteLine($"Install Config not found for '{installConfig.AppKey.Format()}'");
                }
            }
        }
    }

    private static string GetLocalMachineName()
    {
        var domain = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName;
        return string.IsNullOrWhiteSpace(domain)
            ? Environment.MachineName
            : $"{Environment.MachineName}.{domain}";
    }

    private readonly Dictionary<string, CredentialValue> machineCredentials = new();

    private async Task<CredentialValue> GetInstallerCredentials(IHubAdministration hubAdministration, string installMachineName, CancellationToken ct)
    {
        var dotIndex = installMachineName.IndexOf('.');
        if (dotIndex > -1)
        {
            installMachineName = installMachineName.Substring(0, dotIndex);
        }
        var key = installMachineName.ToLower();
        if (!machineCredentials.TryGetValue(key, out var installerCreds))
        {
            var password = Guid.NewGuid().ToString();
            var installationUser = await hubAdministration.AddOrUpdateInstallationUser(installMachineName, password, ct);
            installerCreds = new CredentialValue
            (
                installationUser.UserName.Value,
                password
            );
            machineCredentials.Add(key, installerCreds);
        }
        return installerCreds;
    }

    private Task<NewInstallationResult> NewInstallation(AppKey appKey, string machineName, AppVersionName versionName, string domain, string siteName, CancellationToken ct)
    {
        Console.WriteLine($"New installation {appKey.Name.DisplayText} {appKey.Type.DisplayText} {machineName} {versionName.DisplayText}");
        return hubAdministration.NewInstallation(versionName, appKey, machineName, domain, siteName, ct);
    }
}