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

    public async Task<AppModel[]> AddOrUpdateApps(AppVersionName versionName, AppDefinitionModel[] appDefs)
    {
        var apps = new List<AppModel>();
        foreach (var appDef in appDefs)
        {
            var app =  await appFactory.Apps.AddOrUpdate(versionName, appDef.AppKey, appDef.Domain, clock.Now());
            apps.Add(app.ToAppModel());
        }
        return apps.ToArray();
    }

    public async Task<XtiVersionModel> Version(AppVersionName versionName, AppVersionKey versionKey)
    {
        var version = await appFactory.Versions.VersionByName(versionName, versionKey);
        return version.ToModel();
    }

    public async Task<XtiVersionModel[]> Versions(AppVersionName versionName)
    {
        var versions = await appFactory.Versions.VersionsByName(versionName);
        return versions.Select(v => v.ToModel()).ToArray();
    }

    public async Task AddOrUpdateVersions(AppKey[] appKeys, XtiVersionModel[] publishedVersions)
    {
        if (publishedVersions.Any())
        {
            var versionName = publishedVersions[0].VersionName;
            var exceptions = publishedVersions.Where(v => !v.VersionName.Equals(versionName));
            if (exceptions.Any())
            {
                var joinedExceptions = string.Join(",", exceptions.Select(v => v.VersionName.DisplayText).Distinct());
                throw new ArgumentException($"Expected version '{versionName.DisplayText}' but included versions {joinedExceptions}");
            }
            var versions = new List<XtiVersion>();
            foreach (var publishedVersion in publishedVersions)
            {
                foreach (var versionModel in publishedVersions)
                {
                    var version = await appFactory.Versions.AddIfNotFound
                    (
                        versionModel.VersionName,
                        versionModel.VersionKey,
                        clock.Now(),
                        versionModel.Status,
                        versionModel.VersionType,
                        versionModel.VersionNumber
                    );
                    versions.Add(version);
                }
            }
            foreach (var appKey in appKeys)
            {
                var app = await appFactory.Apps.App(appKey);
                foreach (var version in versions)
                {
                    await app.AddVersionIfNotFound(version);
                }
            }
        }
    }

    public async Task<XtiVersionModel> BeginPublish(AppVersionName versionName, AppVersionKey versionKey)
    {
        var version = await appFactory.Versions.VersionByName(versionName, versionKey);
        await version.Publishing();
        return version.ToModel();
    }

    public async Task<XtiVersionModel> EndPublish(AppVersionName versionName, AppVersionKey versionKey)
    {
        var version = await appFactory.Versions.VersionByName(versionName, versionKey);
        await version.Published();
        return version.ToModel();
    }

    public async Task<NewInstallationResult> NewInstallation(AppVersionName versionName, AppKey appKey, string machineName)
    {
        var version = await appFactory.Versions.VersionByName(versionName, AppVersionKey.Current);
        var app = await appFactory.Apps.App(appKey);
        await app.AddVersionIfNotFound(version);
        var appVersion = version.App(app);
        var installLocation = await appFactory.InstallLocations.TryAdd(machineName);
        Installation currentInstallation;
        var hasCurrentInstallation = await installLocation.HasCurrentInstallation(app);
        if (hasCurrentInstallation)
        {
            currentInstallation = await installLocation.CurrentInstallation(app);
        }
        else
        {
            currentInstallation = await installLocation.NewCurrentInstallation(appVersion, clock.Now());
        }
        Installation? versionInstallation = null;
        if (xtiEnv.IsProduction())
        {
            var hasVersionInstallation = await installLocation.HasVersionInstallation(appVersion);
            if (hasVersionInstallation)
            {
                versionInstallation = await installLocation.VersionInstallation(appVersion);
                await versionInstallation.InstallPending();
            }
            else
            {
                versionInstallation = await installLocation.NewVersionInstallation(appVersion, clock.Now());
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
        machineName = getMachineName(machineName);
        var hashedPassword = hashedPasswordFactory.Create(password);
        var installationUser = await appFactory.InstallationUsers.AddOrUpdateInstallationUser(machineName, hashedPassword, clock.Now());
        return installationUser.ToModel();
    }

    private static string getMachineName(string machineName)
    {
        var dashIndex = machineName.IndexOf(".");
        if (dashIndex > -1)
        {
            machineName = machineName.Substring(0, dashIndex);
        }
        return machineName;
    }

    public async Task<AppUserModel> AddOrUpdateSystemUser(AppKey appKey, string machineName, string password)
    {
        machineName = getMachineName(machineName);
        var hashedPassword = hashedPasswordFactory.Create(password);
        var installationUser = await appFactory.SystemUsers.AddOrUpdateSystemUser(appKey, machineName, hashedPassword, clock.Now());
        return installationUser.ToModel();
    }

    public async Task<XtiVersionModel> StartNewVersion(AppVersionName versionName, AppVersionType versionType, AppKey[] appKeys)
    {
        var version = await appFactory.Versions.StartNewVersion(versionName, clock.Now(), versionType, appKeys);
        return version.ToModel();
    }
}