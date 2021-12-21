using XTI_App.Abstractions;

namespace XTI_HubAppApi.AppInstall;

public sealed class GetCurrentInstallationsRequest
{
    public string QualifiedMachineName { get; set; } = "";
    public AppKey AppKey { get; set; } = AppKey.Unknown;
}