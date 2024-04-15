namespace XTI_HubWebAppApi;

public sealed class LoginReturnKey : ILoginReturnKey
{
    private readonly StoredObjectFactory storedObjectFactory;

    public LoginReturnKey(StoredObjectFactory storedObjectFactory)
    {
        this.storedObjectFactory = storedObjectFactory;
    }

    public Task<string> Value(string returnUrl) =>
        storedObjectFactory.CreateStoredObject(new StorageName("Login Return"))
            .Store
            (
                generateKeyModel: GenerateKeyModel.SixDigit(),
                data: new LoginReturnModel { ReturnUrl = returnUrl },
                expireAfter: TimeSpan.FromDays(90),
                isSlidingExpiration: true
            );
}
