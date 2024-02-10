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
        (
            machineName: machineName,
            password: password
        );
        return hubClient.Install.AddInstallationUser(request);
    }

    public Task<AppUserModel> AddOrUpdateSystemUser(AppKey appKey, string machineName, string password)
    {
        var request = new AddSystemUserRequest
        (
            appKey: appKey,
            machineName: machineName,
            password: password
        );
        return hubClient.Install.AddSystemUser(request);
    }

    public Task<AppUserModel> AddOrUpdateAdminUser(AppKey appKey, AppUserName userName, string password) =>
        hubClient.Install.AddAdminUser
        (
            new AddAdminUserRequest
            (
                appKey: appKey,
                userName: userName,
                password: password
            )
        );

    public Task BeginInstall(int installationID) =>
        hubClient.Install.BeginInstallation(new GetInstallationRequest(installationID));

    public Task<XtiVersionModel> BeginPublish(AppVersionName versionName, AppVersionKey versionKey)
    {
        var request = new PublishVersionRequest
        (
            versionName: versionName,
            versionKey: versionKey
        );
        return hubClient.Publish.BeginPublish(request);
    }

    public Task<XtiVersionModel> EndPublish(AppVersionName versionName, AppVersionKey versionKey)
    {
        var request = new PublishVersionRequest
        (
            versionName: versionName,
            versionKey: versionKey
        );
        return hubClient.Publish.EndPublish(request);
    }

    public Task Installed(int installationID) =>
        hubClient.Install.Installed(new GetInstallationRequest(installationID));

    public Task<NewInstallationResult> NewInstallation(AppVersionName versionName, AppKey appKey, string machineName, string domain, string siteName)
    {
        var request = new NewInstallationRequest
        (
            versionName: versionName,
            appKey: appKey,
            qualifiedMachineName: machineName,
            domain: domain,
            siteName: siteName
        );
        return hubClient.Install.NewInstallation(request);
    }

    public Task<XtiVersionModel> StartNewVersion(AppVersionName versionName, AppVersionType versionType)
    {
        var request = new NewVersionRequest
        (
            versionName: versionName,
            versionType: versionType
        );
        return hubClient.Publish.NewVersion(request);
    }

    public Task<XtiVersionModel> Version(AppVersionName versionName, AppVersionKey versionKey)
    {
        var request = new GetVersionRequest
        (
            versionName: versionName,
            versionKey: versionKey
        );
        return hubClient.Install.GetVersion(request);
    }

    public Task<XtiVersionModel[]> Versions(AppVersionName versionName) =>
        hubClient.Install.GetVersions(new GetVersionsRequest(versionName));

    public Task AddOrUpdateVersions(AppKey[] appKeys, AddVersionRequest[] publishedVersions) =>
        hubClient.Install.AddOrUpdateVersions
        (
            new AddOrUpdateVersionsRequest
            (
                apps: appKeys,
                versions: publishedVersions
            )
        );

    public Task<AppModel[]> AddOrUpdateApps(AppVersionName versionName, AppKey[] appKeys) =>
        hubClient.Install.AddOrUpdateApps
        (
            new AddOrUpdateAppsRequest
            (
                versionName: versionName,
                appKeys: appKeys
            )
        );

}