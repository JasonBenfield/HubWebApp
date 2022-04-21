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
        var versionKey = scopes.GetRequiredService<AppVersionKey>();
        var hubAdministration = scopes.GetRequiredService<IHubAdministration>();
        var appKeys = selectedAppKeys.Values.Where(a => !a.Type.Equals(AppType.Values.Package));
        foreach (var appKey in appKeys)
        {
            using var publishedAssets = scopes.GetRequiredService<IPublishedAssets>();
            await publishedAssets.Load(appKey, versionKey);
            var versionReader = new VersionReader(publishedAssets.VersionsPath);
            var versions = await versionReader.Versions();
            await hubAdministration.AddOrUpdateVersions
            (
                new AppDefinitionModel(appKey, appKey.Type.Equals(AppType.Values.WebApp) ? options.Domain : ""),
                versions
            );
            var installMachineName = getMachineName();
            await newInstallation
            (
                appKey,
                installMachineName
            );
            if (string.IsNullOrWhiteSpace(options.DestinationMachine))
            {
                await new LocalInstallProcess(scopes, appKey, publishedAssets).Run();
            }
            else
            {
                await new LocalInstallServiceProcess(scopes, appKey).Run();
            }
        }
    }

    private Task newInstallation(AppKey appKey, string machineName)
    {
        Console.WriteLine("New installation");
        var versionName = new AppVersionNameAccessor().Value;
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