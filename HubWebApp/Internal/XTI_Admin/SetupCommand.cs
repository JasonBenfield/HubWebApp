using XTI_App.Abstractions;
using XTI_Core;
using XTI_Hub.Abstractions;
using XTI_Installation;

namespace XTI_Admin;

internal sealed class SetupCommand : ICommand
{
    private readonly AdminOptions options;
    private readonly XtiEnvironment xtiEnv;
    private readonly PublishedAssetsFactory publishedAssetsFactory;
    private readonly IHubService hubService;
    private readonly SelectedAppKeys selectedAppKeys;
    private readonly AppVersionNameAccessor versionNameAccessor;
    private readonly CurrentVersion currentVersionAccessor;
    private readonly PublishSetupProcess publishSetupProcess;

    public SetupCommand(AdminOptions options, XtiEnvironment xtiEnv, PublishedAssetsFactory publishedAssetsFactory, IHubService hubService, SelectedAppKeys selectedAppKeys, AppVersionNameAccessor versionNameAccessor, CurrentVersion currentVersionAccessor, PublishSetupProcess publishSetupProcess)
    {
        this.options = options;
        this.xtiEnv = xtiEnv;
        this.publishedAssetsFactory = publishedAssetsFactory;
        this.hubService = hubService;
        this.selectedAppKeys = selectedAppKeys;
        this.versionNameAccessor = versionNameAccessor;
        this.currentVersionAccessor = currentVersionAccessor;
        this.publishSetupProcess = publishSetupProcess;
    }

    public async Task Execute(CancellationToken ct)
    {
        var slnDir = Environment.CurrentDirectory;
        using var publishedAssets = publishedAssetsFactory.Create(options.GetInstallationSource(xtiEnv));
        var appKeys = selectedAppKeys.Values()
            .Where(ak => !ak.Type.Equals(AppType.Values.Package))
            .ToArray();
        var versionName = versionNameAccessor.Value;
        await hubService.AddOrUpdateApps(versionName, appKeys, ct);
        var versionKey = AppVersionKey.Current;
        if (xtiEnv.IsProduction() && !string.IsNullOrWhiteSpace(options.VersionKey))
        {
            versionKey = AppVersionKey.Parse(options.VersionKey);
        }
        foreach (var appKey in appKeys)
        {
            SetCurrentDirectory(slnDir, appKey);
            await publishSetupProcess.Run(appKey, versionKey);
            var appVersion = await currentVersionAccessor.Value(ct);
            var release = $"v{appVersion.VersionNumber.Format()}";
            var setupAppPath = await publishedAssets.LoadSetup(release, appKey, versionKey, ct);
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
