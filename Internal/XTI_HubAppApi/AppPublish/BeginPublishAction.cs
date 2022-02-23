using XTI_App.Api;
using XTI_Hub.Abstractions;

namespace XTI_HubAppApi.AppPublish;

public sealed class BeginPublishAction : AppAction<PublishVersionRequest, AppVersionModel>
{
    private readonly IHubAdministration hubAdministration;

    public BeginPublishAction(IHubAdministration hubAdministration)
    {
        this.hubAdministration = hubAdministration;
    }

    public Task<AppVersionModel> Execute(PublishVersionRequest model) => hubAdministration.BeginPublish(model.AppKey, model.VersionKey);
}