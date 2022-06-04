namespace XTI_HubAppApi.ExternalAuth;

internal sealed class ExternalAuthKeyAction : AppAction<ExternalAuthKeyModel, string>
{
    private readonly HubFactory hubFactory;
    private readonly StoredObjectFactory storedObjectFactory;

    public ExternalAuthKeyAction(HubFactory hubFactory, StoredObjectFactory storedObjectFactory)
    {
        this.hubFactory = hubFactory;
        this.storedObjectFactory = storedObjectFactory;
    }

    public async Task<string> Execute(ExternalAuthKeyModel model, CancellationToken stoppingToken)
    {
        var app = await hubFactory.Apps.App(model.AppKey);
        var user = await hubFactory.Users.UserByExternalKey(app, model.ExternalUserKey);
        var storedObject = storedObjectFactory.CreateStoredObject(new StorageName("XTI Authenticated"));
        var authKey = await storedObject.Store
        (
            GeneratedStorageKeyType.Values.SixDigit,
            new AuthenticatedModel { UserName = user.UserName().Value },
            TimeSpan.FromMinutes(30)
        );
        return authKey;
    }
}
