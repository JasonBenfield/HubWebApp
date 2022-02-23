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

    public Task<int> BeginCurrentInstall(AppKey appKey, AppVersionKey installVersionKey, string machineName)
    {
        var request = new BeginInstallationRequest
        {
            AppKey = appKey,
            VersionKey = installVersionKey.DisplayText,
            QualifiedMachineName = machineName
        };
        return hubClient.Install.BeginCurrentInstallation("", request);
    }

    public Task<int> BeginVersionInstall(AppKey appKey, AppVersionKey versionKey, string machineName)
    {
        var request = new BeginInstallationRequest
        {
            AppKey = appKey,
            VersionKey = versionKey.DisplayText,
            QualifiedMachineName = machineName
        };
        return hubClient.Install.BeginVersionInstallation("", request);
    }

    public Task<AppVersionModel> BeginPublish(AppKey appKey, AppVersionKey versionKey)
    {
        var request = new PublishVersionRequest
        {
            AppKey = appKey,
            VersionKey = versionKey
        };
        return hubClient.Publish.BeginPublish("", request);
    }

    public Task<AppVersionModel> EndPublish(AppKey appKey, AppVersionKey versionKey)
    {
        var request = new PublishVersionRequest
        {
            AppKey = appKey,
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

    public Task<NewInstallationResult> NewInstallation(AppKey appKey, string machineName)
    {
        var request = new NewInstallationRequest
        {
            AppKey = appKey,
            QualifiedMachineName = machineName
        };
        return hubClient.Install.NewInstallation("", request);
    }

    public Task<AppVersionModel> StartNewVersion(AppKey appKey, string domain, AppVersionKey versionKey, AppVersionType versionType)
    {
        var request = new NewVersionRequest
        {
            AppKey = appKey,
            VersionKey = versionKey,
            Domain = domain,
            VersionType = versionType
        };
        return hubClient.Publish.NewVersion("", request);
    }

    public Task<AppVersionModel> Version(AppKey appKey, AppVersionKey versionKey)
    {
        var request = new GetVersionRequest
        {
            AppKey = appKey,
            VersionKey = versionKey
        };
        return hubClient.Install.GetVersion("", request);
    }

    public Task<AppVersionKey> NextVersionKey() => hubClient.Publish.NextVersionKey("");
}