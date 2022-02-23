using XTI_App.Abstractions;
using XTI_Core;
using XTI_Hub.Abstractions;

namespace XTI_Hub;

public sealed class DbHubAdministration : IHubAdministration
{
    private readonly XtiEnvironment xtiEnv;
    private readonly AppFactory appFactory;
    private readonly IHashedPasswordFactory hashedPasswordFactory;
    private readonly IClock clock;

    public DbHubAdministration(XtiEnvironment xtiEnv, AppFactory appFactory, IHashedPasswordFactory hashedPasswordFactory, IClock clock)
    {
        this.xtiEnv = xtiEnv;
        this.appFactory = appFactory;
        this.hashedPasswordFactory = hashedPasswordFactory;
        this.clock = clock;
    }

    public async Task<AppVersionModel> Version(AppKey appKey, AppVersionKey versionKey)
    {
        var app = await appFactory.Apps.App(appKey);
        var version = await app.Version(versionKey);
        return version.ToModel();
    }

    public async Task<AppVersionModel> BeginPublish(AppKey appKey, AppVersionKey versionKey)
    {
        var app = await appFactory.Apps.App(appKey);
        var version = await app.Version(versionKey);
        await version.Publishing();
        return version.ToModel();
    }

    public async Task<AppVersionModel> EndPublish(AppKey appKey, AppVersionKey versionKey)
    {
        var app = await appFactory.Apps.App(appKey);
        var version = await app.Version(versionKey);
        await version.Published();
        return version.ToModel();
    }

    public async Task<NewInstallationResult> NewInstallation(AppKey appKey, string machineName)
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
            currentInstallation = await installLocation.NewCurrentInstallation(version, clock.Now());
        }
        Installation? versionInstallation = null;
        if (xtiEnv.IsProduction())
        {
            var hasVersionInstallation = await installLocation.HasVersionInstallation(version);
            if (hasVersionInstallation)
            {
                versionInstallation = await installLocation.VersionInstallation(version);
                await versionInstallation.InstallPending();
            }
            else
            {
                versionInstallation = await installLocation.NewVersionInstallation(version, clock.Now());
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

    public async Task Installed(int installationID)
    {
        var installation = await appFactory.Installations.Installation(installationID);
        await installation.Installed();
    }

    public async Task<AppUserModel> AddOrUpdateInstallationUser(string machineName, string password)
    {
        var dashIndex = machineName.IndexOf(".");
        if (dashIndex > -1)
        {
            machineName = machineName.Substring(0, dashIndex);
        }
        var hashedPassword = hashedPasswordFactory.Create(password);
        var installationUser = await appFactory.InstallationUsers.AddOrUpdateInstallationUser(machineName, hashedPassword, clock.Now());
        return installationUser.ToModel();
    }

    public Task<AppVersionKey> NextVersionKey() => appFactory.Versions.NextKey();

    public async Task<AppVersionModel> StartNewVersion(AppKey appKey, string domain, AppVersionKey versionKey, AppVersionType versionType)
    {
        var version = await appFactory.Apps.StartNewVersion(appKey, domain, versionKey, versionType, clock.Now());
        return version.ToModel();
    }
}