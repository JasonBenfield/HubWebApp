using XTI_App.Abstractions;
using XTI_Core;
using XTI_Hub.Abstractions;
using XTI_Internal.Abstractions;

namespace XTI_Hub;

public sealed class EfHubService : IHubService
{
    private readonly EfHubDB db;
    private readonly IHashedPasswordFactory hashedPasswordFactory;
    private readonly IClock clock;

    public EfHubService(EfHubDB db, IHashedPasswordFactory hashedPasswordFactory, IClock clock)
    {
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

    public async Task<AppModel[]> AddOrUpdateApps(AppVersionName versionName, string repoOwner, string repoName, AppKey[] appKeys, CancellationToken ct)
    {
        var efApps = new List<AppModel>();
        foreach (var appKey in appKeys)
        {
            var efApp = await db.Transaction(() => db.Apps.AddOrUpdate(versionName, repoOwner, repoName, appKey, clock.Now(), ct));
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

    public async Task<AppUserModel> AddOrUpdateInstallationUser(string machineName, string password, CancellationToken ct)
    {
        machineName = GetMachineName(machineName);
        var hashedPassword = hashedPasswordFactory.Create(password);
        var efInstallationUser = await db.Installers.AddOrUpdateInstaller(machineName, hashedPassword, clock.Now(), ct);
        return efInstallationUser.ToModel();
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
        var efInstallationUser = await db.SystemUsers.AddOrUpdateSystemUser(new SystemUserName(appKey, machineName), hashedPassword, clock.Now(), ct);
        return efInstallationUser.ToModel();
    }

    public async Task<AppUserModel> AddOrUpdateAdminUser(AppKey appKey, AppUserName userName, string password, CancellationToken ct)
    {
        var hashedPassword = hashedPasswordFactory.Create(password);
        var efDefaultUserGroup = await db.UserGroups.GetGeneral(ct);
        var efUser = await efDefaultUserGroup.AddOrUpdate
        (
            userName,
            hashedPassword,
            new PersonName(userName.DisplayText),
            new EmailAddress(""),
            clock.Now(),
            ct
        );
        var efApp = await db.Apps.App(appKey, ct);
        var efAdminRole = await efApp.AddOrUpdateRole(AppRoleName.Admin, ct);
        await efUser.AssignRole(efAdminRole, ct);
        return efUser.ToModel();
    }

    public async Task<XtiVersionModel> StartNewVersion(AppVersionName versionName, AppVersionType versionType, CancellationToken ct)
    {
        var efVersion = await db.Versions.StartNewVersion(versionName, clock.Now(), versionType, ct);
        return efVersion.ToModel();
    }

    public async Task<InstallConfigurationModel[]> InstallConfigurations(GetInstallConfigurationsRequest getRequest, CancellationToken ct)
    {
        var efInstallConfigs = await db.InstallConfigurations.Configurations
        (
            getRequest.RepoOwner,
            getRequest.RepoName,
            getRequest.ConfigurationName,
            ct
        );
        var installConfigs = new List<InstallConfigurationModel>();
        foreach (var efInstallConfig in efInstallConfigs)
        {
            var installConfig = await efInstallConfig.ToModel(ct);
            installConfigs.Add(installConfig);
        }
        return installConfigs.ToArray();
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
        var efInstallConfig = await db.Transaction(() => _ConfigureInstall(configRequest, ct));
        var installConfig = await efInstallConfig.ToModel(ct);
        return installConfig;
    }

    private async Task<EfInstallConfiguration> _ConfigureInstall(ConfigureInstallRequest configRequest, CancellationToken ct)
    {
        var efTemplate = await db.InstallConfigurationTemplates.Template(configRequest.TemplateName, ct);
        var efInstallConfig = await db.InstallConfigurations.AddOrUpdateConfiguration
        (
            configRequest.RepoOwner,
            configRequest.RepoName,
            configRequest.ConfigurationName,
            configRequest.AppKey.ToAppKey(),
            efTemplate,
            configRequest.InstallSequence,
            ct
        );
        return efInstallConfig;
    }

    public async Task<InstallConfigurationTemplateModel> ConfigureInstallTemplate(ConfigureInstallTemplateRequest configRequest, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(configRequest.TemplateName))
        {
            throw new Exception("Template Name is required.");
        }
        var efInstallLocation = await db.InstallLocations.AddIfNotFound(configRequest.DestinationMachineName, ct);
        var efTemplate = await db.InstallConfigurationTemplates.AddOrUpdateTemplate
        (
            configRequest.TemplateName,
            configRequest.DestinationMachineName,
            configRequest.Domain,
            configRequest.SiteName,
            ct
        );
        return efTemplate.ToModel();
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
        var efInstallConfig = await db.InstallConfigurations.ConfigurationOrDefault
        (
            deleteRequest.RepoOwner,
            deleteRequest.RepoName,
            deleteRequest.ConfigurationName,
            deleteRequest.AppKey.ToAppKey(),
            ct
        );
        if (efInstallConfig.IsFound())
        {
            await efInstallConfig.Delete(ct);
        }
    }

    public async Task<AppInstallCommandDetailModel> AddInstallCommand(AddInstallCommandRequest installRequest, CancellationToken ct)
    {
        var efApp = await db.Apps.App(installRequest.AppKey.ToAppKey(), ct);
        var efInstallConfiguration = await db.InstallConfigurations.Configuration(installRequest.InstallConfigurationID, ct);
        var installConfiguration = await efInstallConfiguration.ToModel(ct);
        var versionKey = installRequest.ToAppVersionKey();
        var efAppVersion = await efApp.AddVersionIfNotFound(versionKey, ct);
        var efLocation = await db.InstallLocations.AddIfNotFound(installConfiguration.Template.DestinationMachineName, ct);
        var efInstallCommand = await efApp.AddCommand
        (
            efLocation: efLocation,
            commandName: AppCommandName.Install,
            serializedRequest: installRequest.Serialize(),
            timeAdded: clock.Now(),
            timeStarted: installRequest.IsAutoStartEnabled ? clock.Now() : DateTimeOffset.MaxValue,
            ct: ct
        );
        var installCommandDetail = await efInstallCommand.ToInstallCommandDetailModel(ct);
        return installCommandDetail;
    }

    public async Task<AppCommandModel> BeginCommand(AppKey appKey, int commandID, CancellationToken ct)
    {
        var efApp = await db.Apps.App(appKey, ct);
        var efCommand = await efApp.Command(commandID, ct);
        await efCommand.Begin(clock.Now(), ct);
        return efCommand.ToModel();
    }

    public async Task<AppInstallCommandDetailModel> GetInstallCommandDetail(AppKey appKey, int commandID, CancellationToken ct)
    {
        var efApp = await db.Apps.App(appKey, ct);
        var efCommand = await efApp.Command(commandID, ct);
        var installCommandDetail = await efCommand.ToInstallCommandDetailModel(ct);
        return installCommandDetail;
    }

    public async Task<InstallationModel> BeginInstallation(AppKey appKey, int commandID, bool isCurrent, CancellationToken ct)
    {
        var efApp = await db.Apps.App(appKey, ct);
        var efCommand = await efApp.Command(commandID, ct);
        var efInstallation = await db.Transaction
        (
            () => efCommand.BeginInstallation
            (
                timeAdded: clock.Now(),
                isCurrent: isCurrent,
                ct: ct
            )
        );
        return efInstallation.ToModel();
    }

    public async Task Installed(AppKey appKey, int installationID, CancellationToken ct)
    {
        var efApp = await db.Apps.App(appKey, ct);
        var efInstallation = await efApp.Installation(installationID, ct);
        await efInstallation.Installed(ct);
    }

    public async Task CommandEnded(AppKey appKey, int commandID, CancellationToken ct)
    {
        var efApp = await db.Apps.App(appKey, ct);
        var efCommand = await efApp.Command(commandID, ct);
        await efCommand.End(clock.Now(), ct);
    }

    public async Task<AppCommandStepModel> BeginCommandStep(AppKey appKey, int commandID, string activity, CancellationToken ct)
    {
        var efApp = await db.Apps.App(appKey, ct);
        var efCommand = await efApp.Command(commandID, ct);
        var efStep = await efCommand.BeginStep(activity, clock.Now(), ct);
        return efStep.ToModel();
    }

    public async Task CommandStepEnded(AppKey appKey, int stepID, string errorMessage, CancellationToken ct)
    {
        var efApp = await db.Apps.App(appKey, ct);
        var efStep = await efApp.CommandStep(stepID, ct);
        await efStep.End(clock.Now(), errorMessage, ct);
        if (!string.IsNullOrWhiteSpace(errorMessage))
        {
            var efCommand = await efStep.Command(ct);
            await efCommand.Failed(clock.Now(), ct);
        }
    }

    public async Task<AppDeleteCommandDetailModel> GetDeleteCommandDetail(AppKey appKey, int commandID, CancellationToken ct)
    {
        var efApp = await db.Apps.App(appKey, ct);
        var efCommand = await efApp.Command(commandID, ct);
        var deleteCommandDetail = await efCommand.ToDeleteCommandDetailModel(ct);
        return deleteCommandDetail;
    }

    public async Task<AppDeleteCommandDetailModel> AddDeleteCommand(AppKey appKey, int installationID, CancellationToken ct)
    {
        var efApp = await db.Apps.App(appKey, ct);
        var efInstallation = await efApp.Installation(installationID, ct);
        var efCommand = await efInstallation.AddDeleteCommand(timeAdded: clock.Now(), ct: ct);
        var commandDetail = await efCommand.ToDeleteCommandDetailModel(ct);
        return commandDetail;
    }

    public async Task BeginDelete(AppKey appKey, int installationID, CancellationToken ct)
    {
        var efApp = await db.Apps.App(appKey, ct);
        var efInstallation = await efApp.Installation(installationID, ct);
        await efInstallation.BeginDelete(ct);
    }

    public async Task Deleted(AppKey appKey, int installationID, CancellationToken ct)
    {
        var efApp = await db.Apps.App(appKey, ct);
        var efInstallation = await efApp.Installation(installationID, ct);
        await efInstallation.Deleted(ct);
    }

    public async Task<AppCommandSummaryModel[]> GetPendingCommands(AppCommandName[] commandNames, string[] machineNames, CancellationToken ct)
    {
        var commandSummaries = new List<AppCommandSummaryModel>();
        foreach (var machineName in machineNames)
        {
            var efCommands = await db.AppCommands.GetPendingCommands
            (
                commandNames,
                machineName,
                ct
            );
            foreach(var efCommand in efCommands)
            {
                var commandSummary = await efCommand.ToSummaryModel(ct);
                commandSummaries.Add(commandSummary);
            }
        }
        return commandSummaries.ToArray();
    }

}