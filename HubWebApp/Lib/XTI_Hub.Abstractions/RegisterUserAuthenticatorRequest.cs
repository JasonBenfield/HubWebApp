namespace XTI_Hub.Abstractions;

public sealed class RegisterUserAuthenticatorRequest
{
    public int UserID { get; set; }
    public string ExternalUserKey { get; set; } = "";
}
