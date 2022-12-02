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
        return hubClient.Install.AddInstallationUser(request);
    }

    public Task<AppUserModel> AddOrUpdateSystemUser(AppKey appKey, string machineName, string password)
    {
        var request = new AddSystemUserRequest
        {
            AppKey = appKey,
            MachineName = machineName,
            Password = password
        };
        return hubClient.Install.AddSystemUser(request);
    }

    public Task<AppUserModel> AddOrUpdateAdminUser(AppKey appKey, AppUserName userName, string password) =>
        hubClient.Install.AddAdminUser
        (
            new AddAdminUserRequest
            {
                AppKey = appKey,
                UserName = userName.Value,
                Password = password
            }
        );

    public Task BeginInstall(int installationID) =>
        hubClient.Install.BeginInstallation(new GetInstallationRequest(installationID));

    public Task<XtiVersionModel> BeginPublish(AppVersionName versionName, AppVersionKey versionKey)
    {
        var request = new PublishVersionRequest
        {
            VersionName = versionName,
            VersionKey = versionKey
        };
        return hubClient.Publish.BeginPublish(request);
    }

    public Task<XtiVersionModel> EndPublish(AppVersionName versionName, AppVersionKey versionKey)
    {
        var request = new PublishVersionRequest
        {
            VersionName = versionName,
            VersionKey = versionKey
        };
        return hubClient.Publish.EndPublish(request);
    }

    public Task Installed(int installationID) =>
        hubClient.Install.Installed(new GetInstallationRequest(installationID));

    public Task<NewInstallationResult> NewInstallation(AppVersionName versionName, AppKey appKey, string machineName, string domain, string siteName)
    {
        var request = new NewInstallationRequest
        {
            VersionName = versionName,
            AppKey = appKey,
            QualifiedMachineName = machineName,
            Domain = domain,
            SiteName = siteName
        };
        return hubClient.Install.NewInstallation(request);
    }

    public Task<XtiVersionModel> StartNewVersion(AppVersionName versionName, AppVersionType versionType)
    {
        var request = new NewVersionRequest
        {
            VersionName = versionName,
            VersionType = versionType
        };
        return hubClient.Publish.NewVersion(request);
    }

    public Task<XtiVersionModel> Version(AppVersionName versionName, AppVersionKey versionKey)
    {
        var request = new GetVersionRequest
        {
            VersionName = versionName,
            VersionKey = versionKey
        };
        return hubClient.Install.GetVersion(request);
    }

    public Task<XtiVersionModel[]> Versions(AppVersionName versionName) =>
        hubClient.Install.GetVersions(new GetVersionsRequest { VersionName = versionName });

    public Task AddOrUpdateVersions(AppKey[] appKeys, XtiVersionModel[] publishedVersions) =>
        hubClient.Install.AddOrUpdateVersions
        (
            new AddOrUpdateVersionsRequest
            {
                Apps = appKeys,
                Versions = publishedVersions
            }
        );

    public Task<AppModel[]> AddOrUpdateApps(AppVersionName versionName, AppDefinitionModel[] appDefs) =>
        hubClient.Install.AddOrUpdateApps
        (
            new AddOrUpdateAppsRequest
            {
                VersionName = versionName,
                Apps = appDefs
            }
        );

}