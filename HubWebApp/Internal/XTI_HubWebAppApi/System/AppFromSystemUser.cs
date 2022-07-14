namespace XTI_HubWebAppApi.System;

public sealed class AppFromSystemUser
{
    private readonly ICurrentUserName currentUserName;
    private readonly EfAppContext appContext;

    public AppFromSystemUser(ICurrentUserName currentUserName, EfAppContext appContext)
    {
        this.currentUserName = currentUserName;
        this.appContext = appContext;
    }

    public async Task<AppContextModel> App()
    {
        var userName = await currentUserName.Value();
        var systemUserName = SystemUserName.Parse(userName);
        var app = await appContext.App(systemUserName.AppKey);
        return app;
    }
}
