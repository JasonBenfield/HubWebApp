using XTI_App.Api;
using XTI_Credentials;
using XTI_WebAppClient;

namespace XTI_HubAppClient.Extensions;

public sealed class ConfigurationXtiToken : AuthenticatorXtiToken
{
    public ConfigurationXtiToken(IAuthClient authClient, XtiTokenOptions options)
        : base
        (
            authClient, 
            new SimpleCredentials
            (
                new CredentialValue
                (
                    options.UserName, 
                    options.Password
                )
            )
        )
    {
    }
}
