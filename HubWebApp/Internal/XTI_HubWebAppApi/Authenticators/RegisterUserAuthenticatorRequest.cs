namespace XTI_HubWebAppApi.Authenticators;

public sealed class RegisterUserAuthenticatorRequest
{
    public int UserID { get; set; }
    public string ExternalUserKey { get; set; } = "";
}
