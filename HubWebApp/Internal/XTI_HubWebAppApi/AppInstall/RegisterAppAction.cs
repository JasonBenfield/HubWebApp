namespace XTI_HubWebAppApi.AppInstall;

public sealed class RegisterAppAction : AppAction<RegisterAppRequest, AppWithModKeyModel>
{
    private readonly HubFactory appFactory;

    public RegisterAppAction(HubFactory appFactory)
    {
        this.appFactory = appFactory;
    }

    public async Task<AppWithModKeyModel> Execute(RegisterAppRequest model, CancellationToken stoppingToken)
    {
        var appWithModifier = await new AppRegistration(appFactory).Run
        (
            model.AppTemplate,
            model.VersionKey
        );
        return appWithModifier;
    }
}