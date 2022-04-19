using XTI_App.Abstractions;

namespace XTI_HubAppApi.AppInstall;

public sealed class NewInstallationRequest
{
    public string GroupName { get; set; } = "";
    public AppKey AppKey { get; set; } = AppKey.Unknown;

    public string QualifiedMachineName { get; set; } = "";
}