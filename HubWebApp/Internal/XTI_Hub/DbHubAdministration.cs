using XTI_App.Abstractions;
using XTI_Core;
using XTI_Hub.Abstractions;

namespace XTI_Hub;

public sealed class DbHubAdministration : IHubAdministration
{
    private readonly XtiEnvironment xtiEnv;
    private readonly HubFactory hubFactory;
    private readonly IHashedPasswordFactory hashedPasswordFactory;
    private readonly IClock clock;

    public DbHubAdministration(XtiEnvironment xtiEnv, HubFactory hubFactory, IHashedPasswordFactory hashedPasswordFactory, IClock clock)
    {
        this.xtiEnv = xtiEnv;
        this.hubFactory = hubFactory;
        this.hashedPasswordFactory = hashedPasswordFactory;
        this.clock = clock;
    }

    public async Task<AppModel[]> AddOrUpdateApps(AppVersionName versionName, AppDefinitionModel[] appDefs)
    {
        var apps = new List<AppModel>();
        foreach (var appDef in appDefs)
        {
            var app = await hubFactory.Apps.AddOrUpdate(versionName, appDef.AppKey, clock.Now());
            apps.Add(app.ToAppModel());
        }
        return apps.ToArray();
    }

    public async Task<XtiVersionModel> Version(AppVersionName versionName, AppVersionKey versionKey)
    {
        var version = await hubFactory.Versions.VersionByName(versionName, versionKey);
        return version.ToModel();
    }

    public async Task<XtiVersionModel[]> Versions(AppVersionName versionName)
    {
        var versions = await hubFactory.Versions.VersionsByName(versionName);
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
                var version = await hubFactory.Versions.AddIfNotFound
                (
                    publishedVersion.VersionName,
                    publishedVersion.VersionKey,
                    clock.Now(),
                    publishedVersion.Status,
                    publishedVersion.VersionType,
                    publishedVersion.VersionNumber
                );
                versions.Add(version);
            }
            foreach (var appKey in appKeys)
            {
                var app = await hubFactory.Apps.App(appKey);
                foreach (var version in versions)
                {
                    await app.AddVersionIfNotFound(version);
                }
            }
        }
    }

    public async Task<XtiVersionModel> BeginPublish(AppVersionName versionName, AppVersionKey versionKey)
    {
        var version = await hubFactory.Versions.VersionByName(versionName, versionKey);
        await version.Publishing();
        return version.ToModel();
    }

    public async Task<XtiVersionModel> EndPublish(AppVersionName versionName, AppVersionKey versionKey)
    {
        var version = await hubFactory.Versions.VersionByName(versionName, versionKey);
        await version.Published();
        return version.ToModel();
    }

    public async Task<NewInstallationResult> NewInstallation(AppVersionName versionName, AppKey appKey, string machineName)
    {
        var version = await hubFactory.Versions.VersionByName(versionName, AppVersionKey.Current);
        var app = await hubFactory.Apps.App(appKey);
        await app.AddVersionIfNotFound(version);
        var appVersion = version.App(app);
        var installLocation = await hubFactory.InstallLocations.AddIfNotFound(machineName);
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
        return new NewInstallationResult(currentInstallation.ID, versionInstallation?.ID ?? 0);
    }

    public async Task<int> BeginCurrentInstall(AppKey appKey, AppVersionKey installVersionKey, string machineName, string domain)
    {
        var installLocation = await hubFactory.InstallLocations.Location(machineName);
        var app = await hubFactory.Apps.App(appKey);
        var versionToInstall = await app.Version(installVersionKey);
        var installation = await installLocation.CurrentInstallation(app);
        await installation.Start(versionToInstall, domain);
        return installation.ID;
    }

    public async Task<int> BeginVersionInstall(AppKey appKey, AppVersionKey versionKey, string machineName, string domain)
    {
        var installLocation = await hubFactory.InstallLocations.Location(machineName);
        var app = await hubFactory.Apps.App(appKey);
        var appVersion = await app.Version(versionKey);
        var installation = await installLocation.VersionInstallation(appVersion);
        await installation.Start(domain);
        return installation.ID;
    }

    public async Task Installed(int installationID)
    {
        var installation = await hubFactory.Installations.Installation(installationID);
        await installation.Installed();
    }

    public async Task<AppUserModel> AddOrUpdateInstallationUser(string machineName, string password)
    {
        machineName = getMachineName(machineName);
        var hashedPassword = hashedPasswordFactory.Create(password);
        var installationUser = await hubFactory.InstallationUsers.AddOrUpdateInstallationUser(machineName, hashedPassword, clock.Now());
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
        var installationUser = await hubFactory.SystemUsers.AddOrUpdateSystemUser(appKey, machineName, hashedPassword, clock.Now());
        return installationUser.ToModel();
    }

    public async Task<XtiVersionModel> StartNewVersion(AppVersionName versionName, AppVersionType versionType, AppKey[] appKeys)
    {
        var version = await hubFactory.Versions.StartNewVersion(versionName, clock.Now(), versionType, appKeys);
        return version.ToModel();
    }
}