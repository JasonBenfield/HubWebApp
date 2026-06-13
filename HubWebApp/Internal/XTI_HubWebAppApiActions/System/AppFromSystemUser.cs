namespace XTI_HubWebAppApiActions.System;

public sealed class AppFromSystemUser
{
    private readonly EfHubDB db;
    private readonly ICurrentUserName currentUserName;

    public AppFromSystemUser(EfHubDB db, ICurrentUserName currentUserName)
    {
        this.db = db;
        this.currentUserName = currentUserName;
    }

    public async Task<AppContextModel> AppContext(int installationID, CancellationToken ct)
    {
        AppContextModel appContextModel;
        if (installationID > 0)
        {
            var appKey = await AppKey();
            var efApp = await db.Apps.App(appKey, ct);
            var efInstallation = await efApp.Installation(installationID, ct);
            var efAppVersion = await efInstallation.AppVersion(ct);
            var userName = await currentUserName.Value();
            var appContextFactory = new EfAppContextFactory(db);
            var appContext = appContextFactory.Create(efAppVersion.App.GetAppKey());
            appContextModel = await appContext.App(efAppVersion, ct);
        }
        else
        {
            appContextModel = await App(AppVersionKey.Current, ct);
        }
        return appContextModel;
    }

    public async Task<EfApp> App(CancellationToken ct)
    {
        var appKey = await AppKey();
        var efApp = await db.Apps.App(appKey, ct);
        return efApp;
    }

    private async Task<AppContextModel> App(AppVersionKey versionKey, CancellationToken ct)
    {
        var appKey = await AppKey();
        var appContext = new EfAppContext(db, appKey, versionKey);
        var appContextModel = await appContext.App(ct);
        return appContextModel;
    }

    private async Task<AppKey> AppKey()
    {
        var userName = await currentUserName.Value();
        var systemUserName = SystemUserName.Parse(userName);
        return systemUserName.AppKey;
    }
}
