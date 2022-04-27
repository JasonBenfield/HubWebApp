namespace XTI_HubAppApi.ExternalAuth;

public sealed class ExternalLoginRequest
{
    public string ExternalUserKey { get; set; } = "";
    public string StartUrl { get; set; } = "";
    public string ReturnUrl { get; set; } = "";
}
