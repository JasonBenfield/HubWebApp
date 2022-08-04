namespace XTI_HubWebAppApi.AppInstall;

public sealed class AddAdminUserRequest
{
    public AppKey AppKey { get; set; } = AppKey.Unknown;
    public string UserName { get; set; } = "";
    public string Password { get; set; } = "";
}
