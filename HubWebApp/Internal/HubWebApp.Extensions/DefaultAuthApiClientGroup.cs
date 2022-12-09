using XTI_HubWebAppApi;
using XTI_WebAppClient;

namespace HubWebApp.Extensions;

internal sealed class DefaultAuthApiClientGroup : IAuthApiClientGroup
{
    private readonly Authentication auth;

    public DefaultAuthApiClientGroup(Authentication auth)
    {
        this.auth = auth;
    }

    public async Task<ILoginResult> Authenticate(LoginCredentials model)
    {
        var result = await auth.Authenticate(model.UserName, model.Password);
        return new DefaultLoginResult(result.Token);
    }

    private sealed class DefaultLoginResult : ILoginResult
    {
        public DefaultLoginResult(string token)
        {
            Token = token;
        }

        public string Token { get; set; }
    }
}
