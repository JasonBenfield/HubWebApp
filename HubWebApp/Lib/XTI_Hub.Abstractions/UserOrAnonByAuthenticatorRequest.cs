namespace XTI_Hub.Abstractions;

public sealed class UserOrAnonByAuthenticatorRequest
{
    public UserOrAnonByAuthenticatorRequest()
        :this(new AuthenticatorKey(), "")
    {        
    }

    public UserOrAnonByAuthenticatorRequest(AuthenticatorKey authenticatorKey, string externalUserKey)
    {
        AuthenticatorKey = authenticatorKey.DisplayText;
        ExternalUserKey = externalUserKey;
    }

    public string AuthenticatorKey { get; set; }
    public string ExternalUserKey { get; set; }
}
