using XTI_Core;

namespace XTI_HubWebAppApiActions;

public sealed class LoginReturnKey : ILoginReturnKey
{
    private readonly HubFactory hubFactory;
    private readonly IClock clock;

    public LoginReturnKey(HubFactory hubFactory, IClock clock)
    {
        this.hubFactory = hubFactory;
        this.clock = clock;
    }

    public Task<string> Value(string requesterKey, string returnUrl) =>
        hubFactory.StoredObjects.Store
        (
            new StorageName("Login Return"),
            GenerateKeyModel.TenDigit(),
            new LoginReturnModel(requesterKey, returnUrl),
            clock,
            TimeSpan.FromDays(90),
            isSlidingExpiration: true
        );
}
