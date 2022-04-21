using XTI_App.Abstractions;
using XTI_App.Api;

namespace XTI_HubAppApi.AppInstall;

public sealed class RegisterAppRequest
{
    public string Domain { get; set; } = "";

    public AppVersionKey VersionKey { get; set; } = AppVersionKey.None;

    public AppApiTemplateModel AppTemplate { get; set; } = new AppApiTemplateModel();
}