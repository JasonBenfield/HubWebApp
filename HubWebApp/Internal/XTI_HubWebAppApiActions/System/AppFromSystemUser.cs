namespace XTI_HubWebAppApiActions.System;

public sealed class AppFromSystemUser
{
    private readonly HubFactory hubFactory;
    private readonly ICurrentUserName currentUserName;

    public AppFromSystemUser(HubFactory hubFactory, ICurrentUserName currentUserName)
    {
        this.hubFactory = hubFactory;
        this.currentUserName = currentUserName;
    }

    public async Task<AppContextModel> App(int installationID, CancellationToken ct)
    {
        AppContextModel appContextModel;
        if(installationID > 0)
        {
            var installation = await hubFactory.Installations.InstallationOrDefault(installationID, ct);
            var appVersion = await installation.AppVersion(ct);
            var userName = await currentUserName.Value();
            var systemUserName = SystemUserName.Parse(userName);
            var appContextFactory = new EfAppContextFactory(hubFactory);
            var appContext = appContextFactory.Create(appVersion.App.GetAppKey());
            appContextModel = await appContext.App(appVersion, ct);
            if (!appContextModel.App.AppKey.Equals(systemUserName.AppKey))
            {
                throw new Exception($"Installation {installationID} for '{appContextModel.App.AppKey.Format()}' is not allowed for system user '{userName.DisplayText}'");
            }
        }
        else
        {
            appContextModel = await App(AppVersionKey.Current, ct);
        }
        return appContextModel;
    }

    private async Task<AppContextModel> App(AppVersionKey versionKey, CancellationToken ct)
    {
        var userName = await currentUserName.Value();
        var systemUserName = SystemUserName.Parse(userName);
        var appContext = new EfAppContext(hubFactory, systemUserName.AppKey, versionKey);
        var appContextModel = await appContext.App(ct);
        return appContextModel;
    }
}
