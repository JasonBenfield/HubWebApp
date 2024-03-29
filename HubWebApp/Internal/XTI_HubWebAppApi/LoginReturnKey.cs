﻿namespace XTI_HubWebAppApi;

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
                GenerateKeyModel.SixDigit(),
                new LoginReturnModel { ReturnUrl = returnUrl },
                TimeSpan.FromDays(90)
            );
}
