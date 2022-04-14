using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_Hub.Abstractions;

namespace XTI_HubAppApi.AppInstall;

public sealed class RegisterAppRequest
{
    public XtiVersionModel[] Versions { get; set; } = new XtiVersionModel[0];

    public string Domain { get; set; } = "";

    public AppVersionKey VersionKey { get; set; } = AppVersionKey.None;

    public AppApiTemplateModel AppTemplate { get; set; } = new AppApiTemplateModel();
}