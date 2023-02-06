using XTI_Credentials;
using XTI_WebAppClient;

namespace HubWebApp.EndToEndTests;

public sealed class NewUserXtiToken : AuthenticatorXtiToken
{
    public static readonly CredentialValue NewUserCredentials = new("TestUser1", "Password12345");

    public NewUserXtiToken(IAuthClient authClient, ICredentials credentials)
        : base(authClient, credentials)
    {
    }
}