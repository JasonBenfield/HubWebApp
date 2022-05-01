using System.Text.Json;
using XTI_Core;

namespace XTI_HubAppApi;

public sealed class LoginReturnKey : ILoginReturnKey
{
    private readonly HubFactory factory;
    private readonly IClock clock;

    public LoginReturnKey(HubFactory factory, IClock clock)
    {
        this.factory = factory;
        this.clock = clock;
    }

    public Task<string> Value(string returnUrl) =>
        new StoreObjectProcess(factory).Run
        (
            new StorageName("Login Return"),
            GeneratedStorageKeyType.Values.SixDigit,
            JsonSerializer.Serialize(new LoginReturnModel { ReturnUrl = returnUrl }),
            clock.Now().AddMonths(3)
        );
}
