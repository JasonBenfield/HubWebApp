using XTI_Hub.Abstractions;
using XTI_WebApp.Abstractions;

namespace XTI_HubAppClient.WebApp.Extensions;

internal sealed class LoginReturnKey : ILoginReturnKey
{
    private readonly HubAppClient hubClient;

    public LoginReturnKey(HubAppClient hubClient)
    {
        this.hubClient = hubClient;
    }

    public Task<string> Value(string returnUrl) =>
        hubClient.Auth.LoginReturnKey(new LoginReturnModel { ReturnUrl = returnUrl });
}
