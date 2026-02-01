using XTI_App.Abstractions;
using XTI_Core;
using XTI_Hub.Abstractions;

namespace XTI_Hub;

public sealed class EfHubAdministration : IHubAdministration
{
    private readonly XtiEnvironment xtiEnv;
    private readonly EfHubDB db;
    private readonly IHashedPasswordFactory hashedPasswordFactory;
    private readonly IClock clock;

    public EfHubAdministration(XtiEnvironment xtiEnv, EfHubDB db, IHashedPasswordFactory hashedPasswordFactory, IClock clock)
    {
        this.xtiEnv = xtiEnv;
        this.db = db;
        this.hashedPasswordFactory = hashedPasswordFactory;
        this.clock = clock;
    }

    public Task<string> StoreSingleUse(StorageName storageName, GenerateKeyModel generateKey, object data, TimeSpan expireAfter, CancellationToken ct) =>
        db.StoredObjects.StoreSingleUse
        (
            storageName,
            generateKey,
            data,
            clock,
            expireAfter,
            ct
        );

    public Task<string> StoredObject(StorageName storageName, string storageKey, CancellationToken ct) =>
        db.StoredObjects.SerializedStoredObject(storageName, storageKey, clock.Now(), 0, ct);

    public async Task<AppModel[]> AddOrUpdateApps(AppVersionName versionName, AppKey[] appKeys, CancellationToken ct)
    {
        var efApps = new List<AppModel>();
        foreach (var appKey in appKeys)
        {
            var efApp = await db.Transaction(() => db.Apps.AddOrUpdate(versionName, appKey, clock.Now(), ct));
            efApps.Add(efApp.ToModel());
        }
        return efApps.ToArray();
    }

    public async Task<XtiVersionModel> Version(AppVersionName versionName, AppVersionKey versionKey, CancellationToken ct)
    {
        var efVersion = await db.Versions.VersionByName(versionName, versionKey, ct);
        return efVersion.ToModel();
    }

    public async Task<XtiVersionModel[]> Versions(AppVersionName versionName, CancellationToken ct)
    {
        var efVersions = await db.Versions.VersionsByName(versionName, ct);
        return efVersions.Select(v => v.ToModel()).ToArray();
    }

    public async Task AddOrUpdateVersions(AppKey[] appKeys, AddVersionRequest[] publishedVersions, CancellationToken ct)
    {
        if (publishedVersions.Any())
        {
            var versionName = publishedVersions[0].VersionName;
            var exceptions = publishedVersions.Where(v => !v.VersionName.Equals(versionName));
            if (exceptions.Any())
            {
                var joinedExceptions = string.Join(",", exceptions.Select(v => v.VersionName).Distinct());
                throw new ArgumentException($"Expected version '{versionName}' but included versions {joinedExceptions}");
            }
            var efVersions = new List<EfVersion>();
            foreach (var publishedVersion in publishedVersions)
            {
                var version = await db.Versions.AddIfNotFound
                (
                    publishedVersion.ToAppVersionName(),
                    publishedVersion.ToAppVersionKey(),
                    clock.Now(),
                    publishedVersion.ToAppVersionStatus(),
                    publishedVersion.ToAppVersionType(),
                    publishedVersion.VersionNumber.ToAppVersionNumber(),
                    ct
                );
                efVersions.Add(version);
            }
            foreach (var appKey in appKeys)
            {
                var efApp = await db.Apps.App(appKey, ct);
                foreach (var efVersion in efVersions)
                {
                    await efApp.AddVersionIfNotFound(efVersion, ct);
                }
            }
        }
    }

    public async Task<XtiVersionModel> BeginPublish(AppVersionName versionName, AppVersionKey versionKey, CancellationToken ct)
    {
        var efVersion = await db.Versions.VersionByName(versionName, versionKey, ct);
        await efVersion.Publishing(ct);
        return efVersion.ToModel();
    }

    public async Task<XtiVersionModel> EndPublish(AppVersionName versionName, AppVersionKey versionKey, CancellationToken ct)
    {
        var efVersion = await db.Versions.VersionByName(versionName, versionKey, ct);
        await db.Transaction(() => efVersion.Published(ct));
        return efVersion.ToModel();
    }

    public async Task<NewInstallationResult> NewInstallation(AppVersionName versionName, AppKey appKey, string machineName, string domain, string siteName, CancellationToken ct)
    {
        var version = await db.Versions.VersionByName(versionName, AppVersionKey.Current, ct);
        var app = await db.Apps.App(appKey, ct);
        await app.AddVersionIfNotFound(version, ct);
        var appVersion = version.App(app);
        var installLocation = await db.InstallLocations.AddIfNotFound(machineName, ct);
        var currentInstallation = await installLocation.NewCurrentInstallation(appVersion, domain, siteName, clock.Now(), ct);
        EfInstallation? versionInstallation = null;
        if (xtiEnv.IsProduction())
        {
            versionInstallation = await installLocation.NewVersionInstallation(appVersion, domain, siteName, clock.Now(), ct);
        }
        return new NewInstallationResult(currentInstallation.ID, versionInstallation?.ID ?? 0);
    }

    public async Task BeginInstall(int installationID, CancellationToken ct)
    {
        var installation = await db.Installations.InstallationOrDefault(installationID, ct);
        await installation.BeginInstallation(ct);
    }

    public async Task Installed(int installationID, CancellationToken ct)
    {
        var installation = await db.Installations.InstallationOrDefault(installationID, ct);
        await db.Transaction(() => installation.Installed(ct));
    }

    public async Task<AppUserModel> AddOrUpdateInstallationUser(string machineName, string password, CancellationToken ct)
    {
        machineName = GetMachineName(machineName);
        var hashedPassword = hashedPasswordFactory.Create(password);
        var installationUser = await db.Installers.AddOrUpdateInstaller(machineName, hashedPassword, clock.Now(), ct);
        return installationUser.ToModel();
    }

    private static string GetMachineName(string machineName)
    {
        var dashIndex = machineName.IndexOf(".");
        if (dashIndex > -1)
        {
            machineName = machineName.Substring(0, dashIndex);
        }
        return machineName;
    }

    public async Task<AppUserModel> AddOrUpdateSystemUser(AppKey appKey, string machineName, string password, CancellationToken ct)
    {
        machineName = GetMachineName(machineName);
        var hashedPassword = hashedPasswordFactory.Create(password);
        var installationUser = await db.SystemUsers.AddOrUpdateSystemUser(new SystemUserName(appKey, machineName), hashedPassword, clock.Now(), ct);
        return installationUser.ToModel();
    }

    public async Task<AppUserModel> AddOrUpdateAdminUser(AppKey appKey, AppUserName userName, string password, CancellationToken ct)
    {
        var hashedPassword = hashedPasswordFactory.Create(password);
        var defaultUserGroup = await db.UserGroups.GetGeneral(ct);
        var user = await defaultUserGroup.AddOrUpdate
        (
            userName,
            hashedPassword,
            new PersonName(userName.DisplayText),
            new EmailAddress(""),
            clock.Now(),
            ct
        );
        var app = await db.Apps.App(appKey, ct);
        var adminRole = await app.AddOrUpdateRole(AppRoleName.Admin, ct);
        await user.AssignRole(adminRole, ct);
        return user.ToModel();
    }

    public async Task<XtiVersionModel> StartNewVersion(AppVersionName versionName, AppVersionType versionType, CancellationToken ct)
    {
        var version = await db.Versions.StartNewVersion(versionName, clock.Now(), versionType, ct);
        return version.ToModel();
    }

    public async Task<InstallConfigurationModel[]> InstallConfigurations(GetInstallConfigurationsRequest getRequest, CancellationToken ct)
    {
        var installConfigs = await db.InstallConfigurations.Configurations
        (
            getRequest.RepoOwner,
            getRequest.RepoName,
            getRequest.ConfigurationName,
            ct
        );
        var installConfigModels = new List<InstallConfigurationModel>();
        foreach (var installConfig in installConfigs)
        {
            var installConfigModel = await installConfig.ToModel(ct);
            installConfigModels.Add(installConfigModel);
        }
        return installConfigModels.ToArray();
    }

    public async Task<InstallConfigurationModel> InstallConfiguration(int configurationID, CancellationToken ct)
    {
        var efInstallConfig = await db.InstallConfigurations.Configuration(configurationID, ct);
        var installConfig = await efInstallConfig.ToModel(ct);
        return installConfig;
    }

    public async Task<InstallConfigurationModel> ConfigureInstall(ConfigureInstallRequest configRequest, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(configRequest.RepoOwner))
        {
            throw new Exception("Repo Owner is required.");
        }
        if (string.IsNullOrWhiteSpace(configRequest.RepoName))
        {
            throw new Exception("Repo Name is required.");
        }
        if (string.IsNullOrWhiteSpace(configRequest.ConfigurationName))
        {
            throw new Exception("Configuration Name is required.");
        }
        if (string.IsNullOrWhiteSpace(configRequest.AppKey.AppName))
        {
            throw new Exception("App Name is required.");
        }
        if (configRequest.AppKey.AppType <= 0)
        {
            throw new Exception("App Type is required.");
        }
        if (string.IsNullOrWhiteSpace(configRequest.TemplateName))
        {
            throw new Exception("Template Name is required.");
        }
        var template = await db.InstallConfigurationTemplates.Template(configRequest.TemplateName, ct);
        var installConfig = await db.InstallConfigurations.AddOrUpdateConfiguration
        (
            configRequest.RepoOwner,
            configRequest.RepoName,
            configRequest.ConfigurationName,
            configRequest.AppKey.ToAppKey(),
            template,
            configRequest.InstallSequence,
            ct
        );
        var installConfigModel = await installConfig.ToModel(ct);
        return installConfigModel;
    }

    public async Task<InstallConfigurationTemplateModel> ConfigureInstallTemplate(ConfigureInstallTemplateRequest configRequest, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(configRequest.TemplateName))
        {
            throw new Exception("Template Name is required.");
        }
        var template = await db.InstallConfigurationTemplates.AddOrUpdateTemplate
        (
            configRequest.TemplateName,
            configRequest.DestinationMachineName,
            configRequest.Domain,
            configRequest.SiteName,
            ct
        );
        return template.ToModel();
    }

    public async Task DeleteInstallConfiguration(DeleteInstallConfigurationRequest deleteRequest, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(deleteRequest.RepoOwner))
        {
            throw new Exception("Repo Owner is required.");
        }
        if (string.IsNullOrWhiteSpace(deleteRequest.RepoName))
        {
            throw new Exception("Repo Name is required.");
        }
        if (string.IsNullOrWhiteSpace(deleteRequest.ConfigurationName))
        {
            throw new Exception("Configuration Name is required.");
        }
        if (string.IsNullOrWhiteSpace(deleteRequest.AppKey.AppName))
        {
            throw new Exception("App Name is required.");
        }
        if (deleteRequest.AppKey.AppType <= 0)
        {
            throw new Exception("App Type is required.");
        }
        var installConfig = await db.InstallConfigurations.ConfigurationOrDefault
        (
            deleteRequest.RepoOwner,
            deleteRequest.RepoName,
            deleteRequest.ConfigurationName,
            deleteRequest.AppKey.ToAppKey(),
            ct
        );
        if (installConfig.IsFound())
        {
            await installConfig.Delete(ct);
        }
    }

}