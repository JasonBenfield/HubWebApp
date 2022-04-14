using XTI_App.Secrets;
using XTI_WebAppClient;

namespace XTI_HubAppClient.Extensions;

public sealed class SystemUserXtiToken : AuthenticatorXtiToken
{
    public SystemUserXtiToken(IAuthClient authClient, ISystemUserCredentials credentials) 
        : base(authClient, credentials)
    {
    }
}