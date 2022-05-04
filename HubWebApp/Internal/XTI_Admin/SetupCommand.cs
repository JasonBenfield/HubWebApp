using XTI_App.Abstractions;
using XTI_Core;
using XTI_Hub.Abstractions;

namespace XTI_Admin;

internal sealed class SetupCommand : ICommand
{
    private readonly Scopes scopes;

    public SetupCommand(Scopes scopes)
    {
        this.scopes = scopes;
    }

    public async Task Execute()
    {
        var slnDir = Environment.CurrentDirectory;
        var options = scopes.GetRequiredService<AdminOptions>();
        var xtiEnv = scopes.GetRequiredService<XtiEnvironment>();
        using var publishedAssets = scopes.GetRequiredService<IPublishedAssets>();
        var hubAdministration = scopes.GetRequiredService<IHubAdministration>();
        var appKeys = scopes.GetRequiredService<SelectedAppKeys>().Values
            .Where(ak => !ak.Type.Equals(AppType.Values.Package))
            .ToArray();
        var appDefs = appKeys
            .Select(a => new AppDefinitionModel(a))
            .ToArray();
        var versionName = scopes.GetRequiredService<AppVersionNameAccessor>().Value;
        await hubAdministration.AddOrUpdateApps(versionName, appDefs);
        var versionKey = AppVersionKey.Current;
        if (xtiEnv.IsProduction() && !string.IsNullOrWhiteSpace(options.VersionKey))
        {
            versionKey = AppVersionKey.Parse(options.VersionKey);
        }
        foreach (var appKey in appKeys)
        {
            setCurrentDirectory(slnDir, appKey);
            await new PublishSetupProcess(scopes).Run(appKey, versionKey);
            var setupAppPath = await publishedAssets.LoadSetup(appKey, versionKey);
            await new RunSetupProcess(xtiEnv).Run(versionName, appKey, options.VersionKey, setupAppPath);
        }
        Environment.CurrentDirectory = slnDir;
    }

    private static void setCurrentDirectory(string slnDir, AppKey appKey)
    {
        var projectDir = Path.Combine(slnDir, new AppDirectoryName(appKey).Value);
        if (Directory.Exists(projectDir))
        {
            Environment.CurrentDirectory = projectDir;
        }
    }

}
