using XTI_HubWebAppApi;
using XTI_WebAppClient;

namespace HubWebApp.Extensions;

internal sealed class DefaultAuthClient : IAuthClient
{
    public DefaultAuthClient(AuthenticationFactory auth)
    {
        AuthApi = new DefaultAuthApiClientGroup(auth.CreateForAuthenticate());
    }

    public IAuthApiClientGroup AuthApi { get; }
}
