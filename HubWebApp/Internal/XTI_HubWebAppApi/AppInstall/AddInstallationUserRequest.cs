namespace XTI_HubWebAppApi.AppInstall;

public sealed class AddInstallationUserRequest
{
    public string MachineName { get; set; } = "";
    public string Password { get; set; } = "";
}