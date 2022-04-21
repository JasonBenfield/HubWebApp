using XTI_App.Abstractions;
using XTI_Hub.Abstractions;

namespace XTI_HubAppApi.AppInstall;

public sealed class AddOrUpdateVersionsRequest
{
    public AppDefinitionModel App { get; set; } = new AppDefinitionModel(AppKey.Unknown, "");
    public XtiVersionModel[] Versions { get; set; } = new XtiVersionModel[0];
}
