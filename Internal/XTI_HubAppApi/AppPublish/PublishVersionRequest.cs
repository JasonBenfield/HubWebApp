using XTI_App.Abstractions;

namespace XTI_HubAppApi.AppPublish
{
    public sealed class PublishVersionRequest
    {
        public AppKey AppKey { get; set; }
        public AppVersionKey VersionKey { get; set; }
    }
}
