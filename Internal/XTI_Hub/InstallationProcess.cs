using XTI_App.Abstractions;

namespace XTI_Hub;

public sealed class InstallationProcess
{
    private readonly AppFactory appFactory;

    public InstallationProcess(AppFactory appFactory)
    {
        this.appFactory = appFactory;
    }

    public async Task<NewInstallationResult> NewInstallation(AppKey appKey, string machineName, bool includeVersionInstall, DateTimeOffset now)
    {
        var app = await appFactory.Apps.App(appKey);
        var version = await app.CurrentVersion();
        var installLocation = await appFactory.InstallLocations.TryAdd(machineName);
        Installation currentInstallation;
        var hasCurrentInstallation = await installLocation.HasCurrentInstallation(app);
        if (hasCurrentInstallation)
        {
            currentInstallation = await installLocation.CurrentInstallation(app);
        }
        else
        {
            currentInstallation = await installLocation.NewCurrentInstallation(version, now);
        }
        Installation? versionInstallation = null;
        if (includeVersionInstall)
        {
            var hasVersionInstallation = await installLocation.HasVersionInstallation(version);
            if (hasVersionInstallation)
            {
                versionInstallation = await installLocation.VersionInstallation(version);
                await versionInstallation.InstallPending();
            }
            else
            {
                versionInstallation = await installLocation.NewVersionInstallation(version, now);
            }
        }
        return new NewInstallationResult(currentInstallation.ID.Value, versionInstallation?.ID.Value ?? 0);
    }

    public async Task<int> BeginCurrentInstall(AppKey appKey, AppVersionKey installVersionKey, string machineName)
    {
        var installLocation = await appFactory.InstallLocations.Location(machineName);
        var app = await appFactory.Apps.App(appKey);
        var versionToInstall = await app.Version(installVersionKey);
        var installation = await installLocation.CurrentInstallation(app);
        await installation.Start(versionToInstall);
        return installation.ID.Value;
    }

    public async Task<int> BeginVersionInstall(AppKey appKey, AppVersionKey versionKey, string machineName)
    {
        var installLocation = await appFactory.InstallLocations.Location(machineName);
        var app = await appFactory.Apps.App(appKey);
        var appVersion = await app.Version(versionKey);
        var installation = await installLocation.VersionInstallation(appVersion);
        await installation.Start();
        return installation.ID.Value;
    }
}