using XTI_App.Api;
using XTI_Hub.Abstractions;

namespace XTI_HubAppApi.AppPublish;

public sealed class EndPublishAction : AppAction<PublishVersionRequest, XtiVersionModel>
{
    private readonly IHubAdministration hubAdministration;

    public EndPublishAction(IHubAdministration hubAdministration)
    {
        this.hubAdministration = hubAdministration;
    }

    public Task<XtiVersionModel> Execute(PublishVersionRequest model) => hubAdministration.EndPublish(model.VersionName, model.VersionKey);
}