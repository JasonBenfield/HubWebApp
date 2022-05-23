using XTI_App.Abstractions;
using XTI_App.Secrets;
using XTI_Credentials;
using XTI_Hub.Abstractions;

namespace XTI_Admin;

internal sealed class InstallProcess
{
    private readonly Scopes scopes;

    public InstallProcess(Scopes scopes)
    {
        this.scopes = scopes;
    }

    public async Task Run()
    {
        var options = scopes.GetRequiredService<AdminOptions>();
        var selectedAppKeys = scopes.GetRequiredService<SelectedAppKeys>();
        var appKeys = selectedAppKeys.Values.Where(a => !a.Type.Equals(AppType.Values.Package)).ToArray();
        if (appKeys.Any())
        {
            Console.WriteLine("Beginning Install");
            var versionKey = string.IsNullOrWhiteSpace(options.VersionKey) ? AppVersionKey.Current : AppVersionKey.Parse(options.VersionKey);
            var versionName = scopes.GetRequiredService<AppVersionNameAccessor>().Value;
            using var publishedAssets = scopes.GetRequiredService<IPublishedAssets>();
            var appVersion = await new CurrentVersion(scopes, versionName).Value();
            var release = $"v{appVersion.VersionNumber.Format()}";
            var versionsPath = await publishedAssets.LoadVersions(release);
            var versionReader = new VersionReader(versionsPath);
            var versions = await versionReader.Versions();
            var hubAdministration = scopes.GetRequiredService<IHubAdministration>();
            var appDefs = appKeys
                .Select(a => new AppDefinitionModel(a))
                .ToArray();
            Console.WriteLine("Adding or updating apps");
            await hubAdministration.AddOrUpdateApps(versionName, appDefs);
            Console.WriteLine("Adding or updating versions");
            await hubAdministration.AddOrUpdateVersions(appKeys, versions);
            var password = Guid.NewGuid().ToString();
            var installationUser = await hubAdministration.AddOrUpdateInstallationUser(Environment.MachineName, password);
            var credentials = scopes.GetRequiredService<InstallationUserCredentials>();
            var installerCreds = new CredentialValue
            (
                AppUserName.InstallationUser(Environment.MachineName).Value,
                password
            );
            await credentials.Update(installerCreds);
            foreach (var appKey in appKeys)
            {
                var installations = scopes.GetRequiredService<InstallOptionsAccessor>().Installations(appKey);
                foreach (var installationOptions in installations)
                {
                    var installMachineName = string.IsNullOrWhiteSpace(installationOptions.MachineName)
                        ? Environment.MachineName
                        : installationOptions.MachineName;
                    var instResult = await newInstallation
                    (
                        appKey,
                        installMachineName,
                        versionName
                    );
                    var gitRepoInfo = scopes.GetRequiredService<GitRepoInfo>();
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
                    if (string.IsNullOrWhiteSpace(installationOptions.MachineName))
                    {
                        await new LocalInstallProcess(scopes, publishedAssets)
                            .Run(adminInstallOptions);
                    }
                    else
                    {
                        var storedObjFactory = scopes.GetRequiredService<StoredObjectFactory>();
                        var storageName = new StorageName("XTI Remote Install");
                        var remoteInstallKey = await storedObjFactory.CreateStoredObject(storageName)
                            .Store
                            (
                                GeneratedStorageKeyType.Values.SixDigit, 
                                adminInstallOptions, 
                                TimeSpan.FromMinutes(30)
                            );
                        await new LocalInstallServiceProcess(scopes, appKey, versionName)
                            .Run(installationOptions.MachineName, remoteInstallKey);
                    }
                }
            }
        }
    }

    private Task<NewInstallationResult> newInstallation(AppKey appKey, string machineName, AppVersionName versionName)
    {
        Console.WriteLine($"New installation {appKey.Name.DisplayText} {appKey.Type.DisplayText} {machineName} {versionName.DisplayText}");
        var hubAdministration = scopes.GetRequiredService<IHubAdministration>();
        return hubAdministration.NewInstallation(versionName, appKey, machineName);
    }
}