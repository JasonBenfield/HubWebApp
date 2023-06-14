namespace XTI_HubWebAppApi.ExternalAuth;

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
        var authenticatorKey = new AuthenticatorKey(model.AuthenticatorKey);
        var user = await hubFactory.Users.UserOrAnonByExternalKey(authenticatorKey, model.ExternalUserKey);
        if (user.IsUserName(AppUserName.Anon))
        {
            throw new ExternalUserNotFoundException(authenticatorKey, model.ExternalUserKey);
        }
        var storedObject = storedObjectFactory.CreateStoredObject(new StorageName("XTI Authenticated"));
        var authKey = await storedObject.Store
        (
            GenerateKeyModel.SixDigit(),
            new AuthenticatedModel(user.ToModel().UserName),
            TimeSpan.FromMinutes(30)
        );
        return authKey;
    }
}
