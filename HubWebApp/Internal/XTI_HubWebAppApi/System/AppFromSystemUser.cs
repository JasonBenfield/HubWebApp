namespace XTI_HubWebAppApi.System;

public sealed class AppFromSystemUser
{
    private readonly HubFactory hubFactory;
    private readonly ICurrentUserName currentUserName;

    public AppFromSystemUser(HubFactory hubFactory, ICurrentUserName currentUserName)
    {
        this.hubFactory = hubFactory;
        this.currentUserName = currentUserName;
    }

    public async Task<AppContextModel> App(AppVersionKey versionKey)
    {
        var userName = await currentUserName.Value();
        var systemUserName = SystemUserName.Parse(userName);
        var appContext = new EfAppContext(hubFactory, systemUserName.AppKey, versionKey);
        var app = await appContext.App();
        return app;
    }
}
