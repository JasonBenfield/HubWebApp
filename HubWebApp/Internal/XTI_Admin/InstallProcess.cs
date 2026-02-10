using XTI_App.Abstractions;
using XTI_Core;
using XTI_Hub;
using XTI_Hub.Abstractions;
using XTI_Installation;

namespace XTI_Admin;

public sealed class InstallProcess
{
    private readonly AdminOptions options;
    private readonly SelectedAppKeys selectedAppKeys;
    private readonly AppVersionNameAccessor versionNameAccessor;
    private readonly XtiEnvironment xtiEnv;
    private readonly XtiFolder xtiFolder;
    private readonly IHubService hubService;
    private readonly GitRepoInfo gitRepoInfo;
    private readonly PublishedAssetsFactory publishedAssetsFactory;
    private readonly InstallAppProcessFactory installAppProcessFactory;
    private readonly RemoteCommandService remoteCommandService;

    public InstallProcess(AdminOptions options, SelectedAppKeys selectedAppKeys, AppVersionNameAccessor versionNameAccessor, XtiEnvironment xtiEnv, XtiFolder xtiFolder, IHubService hubService, GitRepoInfo gitRepoInfo, PublishedAssetsFactory publishedAssetsFactory, InstallAppProcessFactory installAppProcessFactory, RemoteCommandService remoteCommandService)
    {
        this.options = options;
        this.selectedAppKeys = selectedAppKeys;
        this.versionNameAccessor = versionNameAccessor;
        this.xtiEnv = xtiEnv;
        this.xtiFolder = xtiFolder;
        this.hubService = hubService;
        this.gitRepoInfo = gitRepoInfo;
        this.publishedAssetsFactory = publishedAssetsFactory;
        this.installAppProcessFactory = installAppProcessFactory;
        this.remoteCommandService = remoteCommandService;
    }

    public async Task Run(CancellationToken ct)
    {
        if (options.IsInitiatedRemotely)
        {
            if (options.CommandID <= 0)
            {
                throw new Exception("Command ID is required.");
            }
            var installCommandDetail = await hubService.GetInstallCommandDetail
            (
                options.CommandID,
                ct
            );
            var t = Task.Run(() => InstallFromRequest(installCommandDetail, ct));
            installCommandDetail = await new InstallCommandLog(hubService).WriteLog(installCommandDetail, ct);
            if (!installCommandDetail.Command.Status.Equals(AppCommandStatus.Values.Completed))
            {
                throw new Exception("Install Command Failed");
            }
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
                var versions = await GetVersions();
                Console.WriteLine("Adding or updating apps");
                await hubService.AddOrUpdateApps(versionName, gitRepoInfo.RepoOwner, gitRepoInfo.RepoName, appKeys, ct);
                Console.WriteLine("Adding or updating versions");
                await hubService.AddOrUpdateVersions
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
                var installConfigs = await hubService.InstallConfigurations
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
                    var isImmediate = options.IsImmediate ||
                        options.GetInstallationSource(xtiEnv) == InstallationSources.Folder;
                    var installCommandDetail = await hubService.AddInstallCommand
                    (
                        new AddInstallCommandRequest
                        (
                            appKey: installConfig.AppKey,
                            versionKey: versionKey,
                            installConfigurationID: options.CommandID,
                            installAsCurrent: true,
                            isAutoStartEnabled: isImmediate
                        ),
                        ct
                    );
                    if (isImmediate)
                    {
                        if (isLocal)
                        {
                            var t = Task.Run(() => InstallFromRequest(installCommandDetail, ct));
                        }
                        else
                        {
                            var remoteOptions = options.Copy();
                            remoteOptions.Command = CommandNames.Install;
                            remoteOptions.CommandID = installCommandDetail.Command.ID;
                            remoteOptions.IsImmediate = true;
                            remoteOptions.IsInitiatedRemotely = true;
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
                    }
                    installCommandDetail = await new InstallCommandLog(hubService).WriteLog(installCommandDetail, ct);
                    if (!installCommandDetail.Command.Status.Equals(AppCommandStatus.Values.Completed))
                    {
                        throw new Exception("Install Command Failed");
                    }
                }
            }
        }
    }

    private async Task<XtiVersionModel[]> GetVersions()
    {
        using var publishedAssets = publishedAssetsFactory.Create(options.GetInstallationSource(xtiEnv));
        var versionsPath = await publishedAssets.LoadVersions();
        var versionReader = new VersionReader(versionsPath);
        var versions = await versionReader.Versions();
        return versions;
    }

    private Task InstallFromRequest(AppInstallCommandDetailModel installCommandDetail, CancellationToken ct)
    {
        var installationSource = options.GetInstallationSource(xtiEnv);
        using var publishedAssets = publishedAssetsFactory.Create(installationSource);
        var installFromRequestProcess = new InstallFromRequestProcess
        (
            hubService: hubService,
            publishedAssets: publishedAssets,
            installFactory: installAppProcessFactory,
            xtiEnv: xtiEnv,
            xtiFolder: xtiFolder
        );
        return installFromRequestProcess.Run(installCommandDetail, ct);
    }

    private static string GetLocalMachineName()
    {
        var domain = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName;
        return string.IsNullOrWhiteSpace(domain) ?
            Environment.MachineName :
            $"{Environment.MachineName}.{domain}";
    }
}