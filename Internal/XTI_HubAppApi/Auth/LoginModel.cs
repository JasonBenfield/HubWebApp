namespace XTI_HubAppApi.Auth;

public sealed class LoginModel
{
    public LoginCredentials Credentials { get; set; } = new LoginCredentials();
    public string StartUrl { get; set; } = "";
    public string ReturnUrl { get; set; } = "";
}