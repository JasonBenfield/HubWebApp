using XTI_App.Abstractions;

namespace XTI_HubAppApi.AppPublish
{
    public sealed class NewVersionRequest
    {
        public AppKey AppKey { get; set; }
        public AppVersionType VersionType { get; set; }
    }
}
