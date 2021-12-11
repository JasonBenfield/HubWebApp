using XTI_App.Abstractions;

namespace XTI_HubAppApi.AppInstall
{
    public sealed class BeginInstallationRequest
    {
        public string QualifiedMachineName { get; set; }
        public AppKey AppKey { get; set; }
        public string VersionKey { get; set; }
    }
}
