namespace XTI_HubWebAppApiActions.AppInstall;

public sealed class RegisterAppAction : AppAction<RegisterAppRequest, AppModel>
{
    private readonly EfHubDB db;

    public RegisterAppAction(EfHubDB db)
    {
        this.db = db;
    }

    public async Task<AppModel> Execute(RegisterAppRequest registerRequest, CancellationToken stoppingToken)
    {
        var registration = new AppRegistration(db);
        var app = await db.Transaction
        (
            () => registration.Run
            (
                registerRequest.AppTemplate,
                registerRequest.VersionKey,
                stoppingToken
            )
        );
        return app;
    }
}