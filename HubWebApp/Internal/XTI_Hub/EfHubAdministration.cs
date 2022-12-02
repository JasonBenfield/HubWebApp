using XTI_App.Abstractions;
using XTI_Core;
using XTI_Hub.Abstractions;

namespace XTI_Hub;

public sealed class EfHubAdministration : IHubAdministration
{
    private readonly XtiEnvironment xtiEnv;
    private readonly HubFactory hubFactory;
    private readonly IHashedPasswordFactory hashedPasswordFactory;
    private readonly IClock clock;

    public EfHubAdministration(XtiEnvironment xtiEnv, HubFactory hubFactory, IHashedPasswordFactory hashedPasswordFactory, IClock clock)
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
            apps.Add(app.ToModel());
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

    public async Task<NewInstallationResult> NewInstallation(AppVersionName versionName, AppKey appKey, string machineName, string domain, string siteName)
    {
        var version = await hubFactory.Versions.VersionByName(versionName, AppVersionKey.Current);
        var app = await hubFactory.Apps.App(appKey);
        await app.AddVersionIfNotFound(version);
        var appVersion = version.App(app);
        var installLocation = await hubFactory.InstallLocations.AddIfNotFound(machineName);
        var currentInstallation = await installLocation.NewCurrentInstallation(appVersion, domain, siteName, clock.Now());
        Installation? versionInstallation = null;
        if (xtiEnv.IsProduction())
        {
            versionInstallation = await installLocation.NewVersionInstallation(appVersion, domain, siteName, clock.Now());
        }
        return new NewInstallationResult(currentInstallation.ID, versionInstallation?.ID ?? 0);
    }

    public async Task BeginInstall(int installationID)
    {
        var installation = await hubFactory.Installations.InstallationOrDefault(installationID);
        await installation.BeginInstallation();
    }

    public async Task Installed(int installationID)
    {
        var installation = await hubFactory.Installations.InstallationOrDefault(installationID);
        await installation.Installed();
    }

    public async Task<AppUserModel> AddOrUpdateInstallationUser(string machineName, string password)
    {
        machineName = getMachineName(machineName);
        var hashedPassword = hashedPasswordFactory.Create(password);
        var installationUser = await hubFactory.Installers.AddOrUpdateInstaller(machineName, hashedPassword, clock.Now());
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
        var installationUser = await hubFactory.SystemUsers.AddOrUpdateSystemUser(new SystemUserName(appKey, machineName), hashedPassword, clock.Now());
        return installationUser.ToModel();
    }

    public async Task<AppUserModel> AddOrUpdateAdminUser(AppKey appKey, AppUserName userName, string password)
    {
        var hashedPassword = hashedPasswordFactory.Create(password);
        var defaultUserGroup = await hubFactory.UserGroups.GetGeneral();
        var user = await defaultUserGroup.AddOrUpdate
        (
            userName, 
            hashedPassword, 
            new PersonName(userName.DisplayText), 
            new EmailAddress(""), 
            clock.Now()
        );
        var app = await hubFactory.Apps.App(appKey);
        var adminRole = await app.AddRoleIfNotFound(AppRoleName.Admin);
        await user.AssignRole(adminRole);
        return user.ToModel();
    }

    public async Task<XtiVersionModel> StartNewVersion(AppVersionName versionName, AppVersionType versionType)
    {
        var version = await hubFactory.Versions.StartNewVersion(versionName, clock.Now(), versionType);
        return version.ToModel();
    }
}