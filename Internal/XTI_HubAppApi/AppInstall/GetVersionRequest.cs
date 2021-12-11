using XTI_App.Abstractions;

namespace XTI_HubAppApi.AppInstall
{
    public sealed class GetVersionRequest
    {
        public AppKey AppKey { get; set; }
        public AppVersionKey VersionKey { get; set; }
    }
}
