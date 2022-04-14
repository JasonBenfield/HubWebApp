using XTI_App.Abstractions;

namespace XTI_HubAppApi.AppInstall;

public sealed class NewInstallationRequest
{
    public AppKey AppKey { get; set; } = AppKey.Unknown;

    public string QualifiedMachineName { get; set; } = "";
}