using XTI_App.Abstractions;
using XTI_Core;
using XTI_Hub.Abstractions;

namespace XTI_Admin;

internal sealed class SetupCommand : ICommand
{
    private readonly AdminOptions options;
    private readonly XtiEnvironment xtiEnv;
    private readonly PublishedAssetsFactory publishedAssetsFactory;
    private readonly IHubAdministration hubAdministration;
    private readonly SelectedAppKeys selectedAppKeys;
    private readonly AppVersionNameAccessor versionNameAccessor;
    private readonly CurrentVersion currentVersionAccessor;
    private readonly PublishSetupProcess publishSetupProcess;

    public SetupCommand(AdminOptions options, XtiEnvironment xtiEnv, PublishedAssetsFactory publishedAssetsFactory, IHubAdministration hubAdministration, SelectedAppKeys selectedAppKeys, AppVersionNameAccessor versionNameAccessor, CurrentVersion currentVersionAccessor, PublishSetupProcess publishSetupProcess)
    {
        this.options = options;
        this.xtiEnv = xtiEnv;
        this.publishedAssetsFactory = publishedAssetsFactory;
        this.hubAdministration = hubAdministration;
        this.selectedAppKeys = selectedAppKeys;
        this.versionNameAccessor = versionNameAccessor;
        this.currentVersionAccessor = currentVersionAccessor;
        this.publishSetupProcess = publishSetupProcess;
    }

    public async Task Execute()
    {
        var slnDir = Environment.CurrentDirectory;
        using var publishedAssets = publishedAssetsFactory.Create(options.GetInstallationSource(xtiEnv));
        var appKeys = selectedAppKeys.Values
            .Where(ak => !ak.Type.Equals(AppType.Values.Package))
            .ToArray();
        var appDefs = appKeys
            .Select(a => new AppDefinitionModel(a))
            .ToArray();
        var versionName = versionNameAccessor.Value;
        await hubAdministration.AddOrUpdateApps(versionName, appDefs);
        var versionKey = AppVersionKey.Current;
        if (xtiEnv.IsProduction() && !string.IsNullOrWhiteSpace(options.VersionKey))
        {
            versionKey = AppVersionKey.Parse(options.VersionKey);
        }
        foreach (var appKey in appKeys)
        {
            SetCurrentDirectory(slnDir, appKey);
            await publishSetupProcess.Run(appKey, versionKey);
            var appVersion = await currentVersionAccessor.Value();
            var release = $"v{appVersion.VersionNumber.Format()}";
            var setupAppPath = await publishedAssets.LoadSetup(release, appKey, versionKey);
            await new RunSetupProcess(xtiEnv).Run(versionName, appKey, versionKey, setupAppPath);
        }
        Environment.CurrentDirectory = slnDir;
    }

    private static void SetCurrentDirectory(string slnDir, AppKey appKey)
    {
        var projectDir = Path.Combine(slnDir, new AppDirectoryName(appKey).Value);
        if (Directory.Exists(projectDir))
        {
            Environment.CurrentDirectory = projectDir;
        }
    }

}
