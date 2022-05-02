namespace XTI_HubAppApi.ExternalAuth;

public sealed class ExternalAuthKeyModel
{
    public AppKey AppKey { get; set; } = AppKey.Unknown;
    public string ExternalUserKey { get; set; } = "";
}
