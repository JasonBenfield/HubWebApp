namespace XTI_Hub.Abstractions;

public sealed class RegisterUserAuthenticatorRequest
{
    public RegisterUserAuthenticatorRequest()
        :this(new AuthenticatorKey(), 0, "")
    {
    }

    public RegisterUserAuthenticatorRequest(AuthenticatorKey authenticatorKey, int userID, string externalUserKey)
    {
        AuthenticatorKey = authenticatorKey.DisplayText;
        UserID = userID;
        ExternalUserKey = externalUserKey;
    }

    public string AuthenticatorKey { get; set; }
    public int UserID { get; set; }
    public string ExternalUserKey { get; set; }
}
