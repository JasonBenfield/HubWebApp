using XTI_App.Abstractions;
using XTI_Hub.Abstractions;

namespace XTI_HubAppApi.AppInstall;

public sealed class AddOrUpdateVersionsRequest
{
    public AppKey[] Apps { get; set; } = new AppKey[0];
    public XtiVersionModel[] Versions { get; set; } = new XtiVersionModel[0];
}
