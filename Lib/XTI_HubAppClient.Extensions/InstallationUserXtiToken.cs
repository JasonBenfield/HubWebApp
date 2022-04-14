using XTI_App.Secrets;
using XTI_WebAppClient;

namespace XTI_HubAppClient.Extensions;

public sealed class InstallationUserXtiToken : AuthenticatorXtiToken
{
    public InstallationUserXtiToken(IAuthClient authClient, IInstallationUserCredentials credentials) 
        : base(authClient, credentials)
    {
    }
}