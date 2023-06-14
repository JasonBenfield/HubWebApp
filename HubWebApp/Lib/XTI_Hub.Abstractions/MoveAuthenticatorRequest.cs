namespace XTI_Hub.Abstractions;

public sealed class MoveAuthenticatorRequest
{
    public MoveAuthenticatorRequest()
        :this(new AuthenticatorKey(), "", 0)
    {    
    }

    public MoveAuthenticatorRequest(AuthenticatorKey authenticatorKey, string externalUserKey, int targetUserID)
    {
        AuthenticatorKey = authenticatorKey.DisplayText;
        ExternalUserKey = externalUserKey;
        TargetUserID = targetUserID;
    }

    public string AuthenticatorKey { get; set; }
    public string ExternalUserKey { get; set; }
    public int TargetUserID { get; set; }
}
