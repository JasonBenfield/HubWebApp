using XTI_Credentials;
using XTI_WebAppClient;

namespace HubWebApp.EndToEndTests;

public sealed class TesterXtiToken : AuthenticatorXtiToken
{
    public TesterXtiToken(IAuthClient authClient, ICredentials credentials)
        : base(authClient, credentials)
    {
    }
}
