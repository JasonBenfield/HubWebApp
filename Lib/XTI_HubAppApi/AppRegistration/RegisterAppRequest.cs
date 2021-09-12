using XTI_App.Api;
using XTI_Hub;

namespace XTI_HubAppApi.AppRegistration
{
    public sealed class RegisterAppRequest
    {
        public AppVersionModel[] Versions { get; set; } = new AppVersionModel[] { };
        public string VersionKey { get; set; } = "";
        public AppApiTemplateModel AppTemplate { get; set; }
    }
}
