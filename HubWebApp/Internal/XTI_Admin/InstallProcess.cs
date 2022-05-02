using XTI_App.Abstractions;
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
            var versionKey = string.IsNullOrWhiteSpace(options.VersionKey) ? AppVersionKey.Current : AppVersionKey.Parse(options.VersionKey);
            var versionName = scopes.GetRequiredService<AppVersionNameAccessor>().Value;
            using var publishedAssets = scopes.GetRequiredService<IPublishedAssets>();
            var appVersion = await new CurrentVersion(scopes, versionName).Value();
            var release = $"v{appVersion.VersionNumber.Format()}";
            await publishedAssets.LoadVersions(release);
            var versionReader = new VersionReader(publishedAssets.VersionsPath);
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
            await hubAdministration.AddOrUpdateInstallationUser(Environment.MachineName, password);
            options.InstallationUserName = AppUserName.InstallationUser(Environment.MachineName).Value;
            options.InstallationPassword = password;
            foreach (var appKey in appKeys)
            {
                var installations = scopes.GetRequiredService<InstallOptionsAccessor>().Installations(appKey);
                foreach (var installation in installations)
                {
                    var installMachineName = string.IsNullOrWhiteSpace(installation.MachineName)
                        ? Environment.MachineName
                        : installation.MachineName;
                    await newInstallation
                    (
                        appKey,
                        installMachineName,
                        versionName
                    );
                    if (string.IsNullOrWhiteSpace(installation.MachineName))
                    {
                        await new LocalInstallProcess(scopes, appKey, publishedAssets).Run(installation);
                    }
                    else
                    {
                        await new LocalInstallServiceProcess(scopes, appKey, versionName).Run(installation);
                    }
                }
            }
        }
    }

    private Task newInstallation(AppKey appKey, string machineName, AppVersionName versionName)
    {
        Console.WriteLine($"New installation {appKey.Name.DisplayText} {appKey.Type.DisplayText} {machineName} {versionName.DisplayText}");
        var hubAdministration = scopes.GetRequiredService<IHubAdministration>();
        return hubAdministration.NewInstallation(versionName, appKey, machineName);
    }
}