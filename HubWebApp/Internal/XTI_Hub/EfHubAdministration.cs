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

    public Task<string> StoreSingleUse(StorageName storageName, GenerateKeyModel generateKey, object data, TimeSpan expireAfter, CancellationToken ct) =>
        hubFactory.StoredObjects.StoreSingleUse
        (
            storageName,
            generateKey,
            data,
            clock,
            expireAfter
        );

    public Task<string> StoredObject(StorageName storageName, string storageKey, CancellationToken ct) =>
        hubFactory.StoredObjects.SerializedStoredObject(storageName, storageKey, clock.Now());
    public async Task<AppModel[]> AddOrUpdateApps(AppVersionName versionName, AppKey[] appKeys, CancellationToken ct)
    {
        var apps = new List<AppModel>();
        foreach (var appKey in appKeys)
        {
            var app = await hubFactory.Apps.AddOrUpdate(versionName, appKey, clock.Now());
            apps.Add(app.ToModel());
        }
        return apps.ToArray();
    }

    public async Task<XtiVersionModel> Version(AppVersionName versionName, AppVersionKey versionKey, CancellationToken ct)
    {
        var version = await hubFactory.Versions.VersionByName(versionName, versionKey);
        return version.ToModel();
    }

    public async Task<XtiVersionModel[]> Versions(AppVersionName versionName, CancellationToken ct)
    {
        var versions = await hubFactory.Versions.VersionsByName(versionName);
        return versions.Select(v => v.ToModel()).ToArray();
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
            var versions = new List<XtiVersion>();
            foreach (var publishedVersion in publishedVersions)
            {
                var version = await hubFactory.Versions.AddIfNotFound
                (
                    publishedVersion.ToAppVersionName(),
                    publishedVersion.ToAppVersionKey(),
                    clock.Now(),
                    publishedVersion.ToAppVersionStatus(),
                    publishedVersion.ToAppVersionType(),
                    publishedVersion.VersionNumber.ToAppVersionNumber()
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

    public async Task<XtiVersionModel> BeginPublish(AppVersionName versionName, AppVersionKey versionKey, CancellationToken ct)
    {
        var version = await hubFactory.Versions.VersionByName(versionName, versionKey);
        await version.Publishing();
        return version.ToModel();
    }

    public async Task<XtiVersionModel> EndPublish(AppVersionName versionName, AppVersionKey versionKey, CancellationToken ct)
    {
        var version = await hubFactory.Versions.VersionByName(versionName, versionKey);
        await version.Published();
        return version.ToModel();
    }

    public async Task<NewInstallationResult> NewInstallation(AppVersionName versionName, AppKey appKey, string machineName, string domain, string siteName, CancellationToken ct)
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

    public async Task BeginInstall(int installationID, CancellationToken ct)
    {
        var installation = await hubFactory.Installations.InstallationOrDefault(installationID);
        await installation.BeginInstallation();
    }

    public async Task Installed(int installationID, CancellationToken ct)
    {
        var installation = await hubFactory.Installations.InstallationOrDefault(installationID);
        await installation.Installed();
    }

    public async Task<AppUserModel> AddOrUpdateInstallationUser(string machineName, string password, CancellationToken ct)
    {
        machineName = GetMachineName(machineName);
        var hashedPassword = hashedPasswordFactory.Create(password);
        var installationUser = await hubFactory.Installers.AddOrUpdateInstaller(machineName, hashedPassword, clock.Now());
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
        var installationUser = await hubFactory.SystemUsers.AddOrUpdateSystemUser(new SystemUserName(appKey, machineName), hashedPassword, clock.Now());
        return installationUser.ToModel();
    }

    public async Task<AppUserModel> AddOrUpdateAdminUser(AppKey appKey, AppUserName userName, string password, CancellationToken ct)
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
        var adminRole = await app.AddOrUpdateRole(AppRoleName.Admin);
        await user.AssignRole(adminRole);
        return user.ToModel();
    }

    public async Task<XtiVersionModel> StartNewVersion(AppVersionName versionName, AppVersionType versionType, CancellationToken ct)
    {
        var version = await hubFactory.Versions.StartNewVersion(versionName, clock.Now(), versionType);
        return version.ToModel();
    }

    public async Task<InstallConfigurationModel[]> InstallConfigurations(GetInstallConfigurationsRequest getRequest, CancellationToken ct)
    {
        var installConfigs = await hubFactory.InstallConfigurations.Configurations
        (
            getRequest.RepoOwner,
            getRequest.RepoName,
            getRequest.ConfigurationName
        );
        var installConfigModels = await installConfigs
            .ToAsyncEnumerable()
            .SelectAwait(async c => await c.ToModel())
            .ToArrayAsync();
        return installConfigModels;
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
        var template = await hubFactory.InstallConfigurationTemplates.Template(configRequest.TemplateName);
        var installConfig = await hubFactory.InstallConfigurations.AddOrUpdateConfiguration
        (
            configRequest.RepoOwner,
            configRequest.RepoName,
            configRequest.ConfigurationName,
            configRequest.AppKey.ToAppKey(),
            template,
            configRequest.InstallSequence
        );
        var installConfigModel = await installConfig.ToModel();
        return installConfigModel;
    }

    public async Task<InstallConfigurationTemplateModel> ConfigureInstallTemplate(ConfigureInstallTemplateRequest configRequest, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(configRequest.TemplateName))
        {
            throw new Exception("Template Name is required.");
        }
        var template = await hubFactory.InstallConfigurationTemplates.AddOrUpdateTemplate
        (
            configRequest.TemplateName,
            configRequest.DestinationMachineName,
            configRequest.Domain,
            configRequest.SiteName
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
        var installConfig = await hubFactory.InstallConfigurations.ConfigurationOrDefault
        (
            deleteRequest.RepoOwner,
            deleteRequest.RepoName,
            deleteRequest.ConfigurationName,
            deleteRequest.AppKey.ToAppKey()
        );
        if (installConfig.IsFound())
        {
            await installConfig.Delete();
        }
    }

}