namespace XTI_HubWebAppApiActions.AppInstall;

public sealed class RegisterAppAction : AppAction<RegisterAppRequest, AppModel>
{
    private readonly HubFactory appFactory;

    public RegisterAppAction(HubFactory appFactory)
    {
        this.appFactory = appFactory;
    }

    public async Task<AppModel> Execute(RegisterAppRequest model, CancellationToken stoppingToken)
    {
        var app = await new AppRegistration(appFactory).Run
        (
            model.AppTemplate,
            model.VersionKey
        );
        return app;
    }
}