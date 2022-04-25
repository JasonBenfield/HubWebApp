namespace XTI_HubAppClient;

public sealed class HcHubAdministration : IHubAdministration
{
    private readonly HubAppClient hubClient;

    public HcHubAdministration(HubAppClient hubClient)
    {
        this.hubClient = hubClient;
    }

    public Task<AppUserModel> AddOrUpdateInstallationUser(string machineName, string password)
    {
        var request = new AddInstallationUserRequest
        {
            MachineName = machineName,
            Password = password
        };
        return hubClient.Install.AddInstallationUser("", request);
    }

    public Task<AppUserModel> AddOrUpdateSystemUser(AppKey appKey, string machineName, string password)
    {
        var request = new AddSystemUserRequest
        {
            AppKey = appKey,
            MachineName = machineName,
            Password = password
        };
        return hubClient.Install.AddSystemUser("", request);
    }

    public Task<int> BeginCurrentInstall(AppKey appKey, AppVersionKey installVersionKey, string machineName)
    {
        var request = new BeginInstallationRequest
        {
            AppKey = appKey,
            VersionKey = installVersionKey,
            QualifiedMachineName = machineName
        };
        return hubClient.Install.BeginCurrentInstallation("", request);
    }

    public Task<int> BeginVersionInstall(AppKey appKey, AppVersionKey versionKey, string machineName)
    {
        var request = new BeginInstallationRequest
        {
            AppKey = appKey,
            VersionKey = versionKey,
            QualifiedMachineName = machineName
        };
        return hubClient.Install.BeginVersionInstallation("", request);
    }

    public Task<XtiVersionModel> BeginPublish(AppVersionName versionName, AppVersionKey versionKey)
    {
        var request = new PublishVersionRequest
        {
            VersionName = versionName,
            VersionKey = versionKey
        };
        return hubClient.Publish.BeginPublish("", request);
    }

    public Task<XtiVersionModel> EndPublish(AppVersionName versionName, AppVersionKey versionKey)
    {
        var request = new PublishVersionRequest
        {
            VersionName = versionName,
            VersionKey = versionKey
        };
        return hubClient.Publish.EndPublish("", request);
    }

    public Task Installed(int installationID)
    {
        var request = new InstalledRequest
        {
            InstallationID = installationID
        };
        return hubClient.Install.Installed("", request);
    }

    public Task<NewInstallationResult> NewInstallation(AppVersionName versionName, AppKey appKey, string machineName)
    {
        var request = new NewInstallationRequest
        {
            VersionName = versionName,
            AppKey = appKey,
            QualifiedMachineName = machineName
        };
        return hubClient.Install.NewInstallation("", request);
    }

    public Task<XtiVersionModel> StartNewVersion(AppVersionName versionName, AppVersionType versionType, AppKey[] appKeys)
    {
        var request = new NewVersionRequest
        {
            VersionName = versionName,
            VersionType = versionType,
            AppKeys = appKeys
        };
        return hubClient.Publish.NewVersion("", request);
    }

    public Task<XtiVersionModel> Version(AppVersionName versionName, AppVersionKey versionKey)
    {
        var request = new GetVersionRequest
        {
            VersionName = versionName,
            VersionKey = versionKey
        };
        return hubClient.Install.GetVersion("", request);
    }

    public Task<XtiVersionModel[]> Versions(AppVersionName versionName) => 
        hubClient.Install.GetVersions("", new GetVersionsRequest { VersionName = versionName });

    public Task AddOrUpdateVersions(AppKey[] appKeys, XtiVersionModel[] publishedVersions) =>
        hubClient.Install.AddOrUpdateVersions
        (
            "", 
            new AddOrUpdateVersionsRequest
            {
                Apps = appKeys,
                Versions = publishedVersions
            }
        );

    public Task<AppModel[]> AddOrUpdateApps(AppVersionName versionName, AppDefinitionModel[] appDefs) =>
        hubClient.Install.AddOrUpdateApps
        (
            "",
            new AddOrUpdateAppsRequest
            {
                VersionName = versionName,
                Apps = appDefs
            }
        );
}