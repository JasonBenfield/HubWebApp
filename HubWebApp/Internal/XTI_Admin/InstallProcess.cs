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
            await publishedAssets.LoadVersions();
            var versionReader = new VersionReader(publishedAssets.VersionsPath);
            var versions = await versionReader.Versions();
            var hubAdministration = scopes.GetRequiredService<IHubAdministration>();
            var appDefs = appKeys
                .Select(a => new AppDefinitionModel(a, a.Type.Equals(AppType.Values.WebApp) ? options.Domain : ""))
                .ToArray();
            await hubAdministration.AddOrUpdateApps(versionName, appDefs);
            await hubAdministration.AddOrUpdateVersions(appKeys, versions);
            foreach (var appKey in appKeys)
            {
                var installMachineName = getMachineName();
                await newInstallation
                (
                    appKey,
                    installMachineName,
                    versionName
                );
                if (string.IsNullOrWhiteSpace(options.DestinationMachine))
                {
                    await new LocalInstallProcess(scopes, appKey, publishedAssets).Run();
                }
                else
                {
                    await new LocalInstallServiceProcess(scopes, appKey, versionName).Run();
                }
            }
        }
    }

    private Task newInstallation(AppKey appKey, string machineName, AppVersionName versionName)
    {
        Console.WriteLine("New installation");
        var hubAdministration = scopes.GetRequiredService<IHubAdministration>();
        return hubAdministration.NewInstallation(versionName, appKey, machineName);
    }

    private string getMachineName()
    {
        var options = scopes.GetRequiredService<AdminOptions>();
        string machineName;
        if (string.IsNullOrWhiteSpace(options.DestinationMachine))
        {
            machineName = Environment.MachineName;
        }
        else
        {
            machineName = options.DestinationMachine;
        }
        return machineName;
    }
}