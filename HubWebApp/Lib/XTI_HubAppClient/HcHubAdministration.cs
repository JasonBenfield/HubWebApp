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

    public Task<AppUserModel> AddOrUpdateSystemUser(AppKey appKey, string machineName, string domain, string password)
    {
        var request = new AddSystemUserRequest
        {
            AppKey = appKey,
            MachineName = machineName,
            Domain = domain,
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

    public Task<XtiVersionModel> BeginPublish(string groupName, AppVersionKey versionKey)
    {
        var request = new PublishVersionRequest
        {
            GroupName = groupName,
            VersionKey = versionKey
        };
        return hubClient.Publish.BeginPublish("", request);
    }

    public Task<XtiVersionModel> EndPublish(string groupName, AppVersionKey versionKey)
    {
        var request = new PublishVersionRequest
        {
            GroupName = groupName,
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

    public Task<NewInstallationResult> NewInstallation(string versionName, AppKey appKey, string machineName)
    {
        var request = new NewInstallationRequest
        {
            GroupName = versionName,
            AppKey = appKey,
            QualifiedMachineName = machineName
        };
        return hubClient.Install.NewInstallation("", request);
    }

    public Task<XtiVersionModel> StartNewVersion(string groupName, AppVersionType versionType, AppDefinitionModel[] appDefs)
    {
        var request = new NewVersionRequest
        {
            GroupName = groupName,
            VersionType = versionType,
            AppDefinitions = appDefs
        };
        return hubClient.Publish.NewVersion("", request);
    }

    public Task<XtiVersionModel> Version(string groupName, AppVersionKey versionKey)
    {
        var request = new GetVersionRequest
        {
            GroupName = groupName,
            VersionKey = versionKey
        };
        return hubClient.Install.GetVersion("", request);
    }
}