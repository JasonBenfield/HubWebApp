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
    private readonly InstallOptionsAccessor installOptionsAccessor;
    private readonly GitRepoInfo gitRepoInfo;
    private readonly PublishedAssetsFactory publishedAssetsFactory;
    private readonly RemoteCommandService remoteCommandService;
    private readonly LocalInstallProcess localInstallProcess;

    public InstallProcess(AdminOptions options, SelectedAppKeys selectedAppKeys, AppVersionNameAccessor versionNameAccessor, XtiEnvironment xtiEnv, XtiGitHubRepository gitHubRepo, IHubAdministration hubAdministration, InstallOptionsAccessor installOptionsAccessor, GitRepoInfo gitRepoInfo, PublishedAssetsFactory publishedAssetsFactory, RemoteCommandService remoteCommandService, LocalInstallProcess localInstallProcess)
    {
        this.options = options;
        this.selectedAppKeys = selectedAppKeys;
        this.versionNameAccessor = versionNameAccessor;
        this.xtiEnv = xtiEnv;
        this.gitHubRepo = gitHubRepo;
        this.hubAdministration = hubAdministration;
        this.installOptionsAccessor = installOptionsAccessor;
        this.gitRepoInfo = gitRepoInfo;
        this.publishedAssetsFactory = publishedAssetsFactory;
        this.remoteCommandService = remoteCommandService;
        this.localInstallProcess = localInstallProcess;
    }

    public async Task Run()
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
            if(publishedAssets is GitHubPublishedAssets)
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
            var appDefs = appKeys
                .Select(a => new AppDefinitionModel(a))
                .ToArray();
            Console.WriteLine("Adding or updating apps");
            await hubAdministration.AddOrUpdateApps(versionName, appDefs);
            Console.WriteLine("Adding or updating versions");
            await hubAdministration.AddOrUpdateVersions(appKeys, versions);
            var versionKey = string.IsNullOrWhiteSpace(options.VersionKey) ? AppVersionKey.Current : AppVersionKey.Parse(options.VersionKey);
            if (xtiEnv.IsProduction() && versionKey.IsCurrent())
            {
                versionKey = versions.First(v => v.IsCurrent()).VersionKey;
            }
            foreach (var appKey in appKeys)
            {
                var installations = installOptionsAccessor.Installations(appKey);
                foreach (var installationOptions in installations)
                {
                    var installMachineName = string.IsNullOrWhiteSpace(installationOptions.MachineName)
                        ? GetLocalMachineName()
                        : installationOptions.MachineName;
                    var instResult = await NewInstallation
                    (
                        appKey,
                        installMachineName,
                        versionName,
                        installationOptions.Domain,
                        installationOptions.SiteName
                    );
                    var installerCreds = await GetInstallerCredentials(hubAdministration, installMachineName);
                    if (string.IsNullOrWhiteSpace(installationOptions.MachineName))
                    {
                        var adminInstallOptions = new AdminInstallOptions
                        (
                            AppKey: appKey,
                            VersionKey: versionKey,
                            RepoOwner: gitRepoInfo.RepoOwner,
                            RepoName: gitRepoInfo.RepoName,
                            Release: release,
                            CurrentInstallationID: instResult.CurrentInstallationID,
                            VersionInstallationID: instResult.VersionInstallationID,
                            InstallerUserName: installerCreds.UserName,
                            InstallerPassword: installerCreds.Password,
                            Options: installationOptions
                        );
                        await localInstallProcess.Run(adminInstallOptions, publishedAssets);
                    }
                    else
                    {
                        var remoteOptions = options.Copy();
                        remoteOptions.DestinationMachine = "";
                        remoteOptions.Domain = installationOptions.Domain;
                        remoteOptions.SiteName = installationOptions.SiteName;
                        await remoteCommandService.Run
                        (
                            installationOptions.MachineName,
                            CommandNames.FromRemote.ToString(),
                            remoteOptions
                        );
                    }
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

    private readonly Dictionary<string, CredentialValue> machineCredentials = new ();

    private async Task<CredentialValue> GetInstallerCredentials(IHubAdministration hubAdministration, string installMachineName)
    {
        var dotIndex = installMachineName.IndexOf('.');
        if(dotIndex > -1)
        {
            installMachineName = installMachineName.Substring(0, dotIndex);
        }
        var key = installMachineName.ToLower();
        if(!machineCredentials.TryGetValue(key, out var installerCreds))
        {
            var password = Guid.NewGuid().ToString();
            var installationUser = await hubAdministration.AddOrUpdateInstallationUser(installMachineName, password);
            installerCreds = new CredentialValue
            (
                installationUser.UserName.Value,
                password
            );
            machineCredentials.Add(key, installerCreds);
        }
        return installerCreds;
    }

    private Task<NewInstallationResult> NewInstallation(AppKey appKey, string machineName, AppVersionName versionName, string domain, string siteName)
    {
        Console.WriteLine($"New installation {appKey.Name.DisplayText} {appKey.Type.DisplayText} {machineName} {versionName.DisplayText}");
        return hubAdministration.NewInstallation(versionName, appKey, machineName, domain, siteName);
    }
}