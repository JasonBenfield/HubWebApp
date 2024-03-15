using XTI_App.Abstractions;
using XTI_HubWebAppApi;
using XTI_WebApp.Abstractions;
using XTI_WebAppClient;

namespace HubWebApp.Extensions;

internal sealed class DefaultAuthApiClientGroup : IAuthApiClientGroup
{
    private readonly Authentication auth;

    public DefaultAuthApiClientGroup(Authentication auth)
    {
        this.auth = auth;
    }

    public async Task<LoginResult> Authenticate(AuthenticateRequest authRequest)
    {
        var result = await auth.Authenticate(authRequest.UserName, authRequest.Password);
        return new LoginResult(result.Token);
    }
}
