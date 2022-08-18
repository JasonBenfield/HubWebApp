namespace XTI_HubWebAppApi.ExternalAuth;

internal sealed class ExternalAuthKeyAction : AppAction<ExternalAuthKeyModel, string>
{
    private readonly AppFromPath appFromPath;
    private readonly HubFactory hubFactory;
    private readonly StoredObjectFactory storedObjectFactory;

    public ExternalAuthKeyAction(AppFromPath appFromPath, HubFactory hubFactory, StoredObjectFactory storedObjectFactory)
    {
        this.appFromPath = appFromPath;
        this.hubFactory = hubFactory;
        this.storedObjectFactory = storedObjectFactory;
    }

    public async Task<string> Execute(ExternalAuthKeyModel model, CancellationToken stoppingToken)
    {
        var app = await appFromPath.Value();
        var user = await hubFactory.Users.UserByExternalKey(app, model.ExternalUserKey);
        var storedObject = storedObjectFactory.CreateStoredObject(new StorageName("XTI Authenticated"));
        var authKey = await storedObject.Store
        (
            GenerateKeyModel.SixDigit(),
            new AuthenticatedModel { UserName = user.ToModel().UserName.Value },
            TimeSpan.FromMinutes(30)
        );
        return authKey;
    }
}
