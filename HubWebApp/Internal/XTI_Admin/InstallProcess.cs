using XTI_App.Abstractions;
using XTI_Core;
using XTI_GitHub;
using XTI_Hub;
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
        if (options.IsInitiatedRemotely)
        {
            if (options.InstallConfigurationID <= 0)
            {
                throw new Exception("Install Configuration ID is required.");
            }
            var installConfig = await hubAdministration.InstallConfiguration(options.InstallConfigurationID, ct);
            var installMachineName = GetLocalMachineName();
            var versionName = versionNameAccessor.Value;
            using var publishedAssets = publishedAssetsFactory.Create(options.GetInstallationSource(xtiEnv));
            var newInstallation = await NewInstallation
            (
                installConfig.AppKey,
                installMachineName,
                versionName,
                installConfig.Template.Domain,
                installConfig.Template.SiteName,
                ct
            );
            await localInstallProcess.Run(newInstallation, publishedAssets, ct);
        }
        else
        {
            var appKeys = selectedAppKeys.Values()
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
                var appKeysNotFound = appKeys
                    .Where(a => !installConfigs.Any(c => c.AppKey.Equals(a)))
                    .ToArray();
                if (appKeysNotFound.Any())
                {
                    var joinedAppKeys = string.Join(", ", appKeys.Select(a => a.Format()));
                    Console.WriteLine($"Install Configuration not found: {joinedAppKeys}");
                }
                foreach (var installConfig in installConfigs)
                {
                    var isLocal = string.IsNullOrWhiteSpace(installConfig.Template.DestinationMachineName);
                    var installMachineName = isLocal ?
                        GetLocalMachineName() :
                        installConfig.Template.DestinationMachineName;
                    if (isLocal)
                    {
                        var newInstallation = await NewInstallation
                        (
                            installConfig.AppKey,
                            installMachineName,
                            versionName,
                            installConfig.Template.Domain,
                            installConfig.Template.SiteName,
                            ct
                        );
                        await localInstallProcess.Run(newInstallation, publishedAssets, ct);
                    }
                    else
                    {
                        var remoteOptions = options.Copy();
                        remoteOptions.Command = CommandNames.Install;
                        remoteOptions.InstallConfigurationID = installConfig.ID;
                        remoteOptions.IsInitiatedRemotely = true;
                        remoteOptions.VersionName = versionName.DisplayText;
                        remoteOptions.DestinationMachine = "";
                        remoteOptions.HubAdministrationType = options.HubAdministrationType == HubAdministrationTypes.Default && installConfig.AppKey.Equals(HubInfo.AppKey) ?
                            HubAdministrationTypes.DB :
                            options.HubAdministrationType;
                        Console.WriteLine($"Starting remote install {installConfig.AppKey.Name.DisplayText} {installConfig.AppKey.Type.DisplayText} {versionKey.DisplayText}");
                        await remoteCommandService.Run
                        (
                            installConfig.Template.DestinationMachineName,
                            CommandNames.FromRemote.ToString(),
                            remoteOptions
                        );
                    }
                    await Task.Delay(TimeSpan.FromSeconds(15), ct);
                }
            }
        }
    }

    private static string GetLocalMachineName()
    {
        var domain = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName;
        return string.IsNullOrWhiteSpace(domain) ? 
            Environment.MachineName : 
            $"{Environment.MachineName}.{domain}";
    }

    private Task<NewInstallationResult> NewInstallation(AppKey appKey, string machineName, AppVersionName versionName, string domain, string siteName, CancellationToken ct)
    {
        Console.WriteLine($"New installation {appKey.Name.DisplayText} {appKey.Type.DisplayText} {machineName} {versionName.DisplayText}");
        return hubAdministration.NewInstallation(versionName, appKey, machineName, domain, siteName, ct);
    }
}