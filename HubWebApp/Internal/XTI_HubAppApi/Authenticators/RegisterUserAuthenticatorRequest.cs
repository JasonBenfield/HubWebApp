namespace XTI_HubAppApi.Authenticators;

public sealed class RegisterUserAuthenticatorRequest
{
    public int UserID { get; set; }
    public string ExternalUserKey { get; set; } = "";
}
