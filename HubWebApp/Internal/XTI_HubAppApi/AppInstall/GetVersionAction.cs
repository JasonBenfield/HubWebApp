using XTI_App.Api;
using XTI_Hub;
using XTI_Hub.Abstractions;

namespace XTI_HubAppApi.AppInstall;

public sealed class GetVersionAction : AppAction<GetVersionRequest, XtiVersionModel>
{
    private readonly AppFactory appFactory;

    public GetVersionAction(AppFactory appFactory)
    {
        this.appFactory = appFactory;
    }

    public async Task<XtiVersionModel> Execute(GetVersionRequest model)
    {
        var version = await appFactory.Versions.VersionByGroupName(model.GroupName, model.VersionKey);
        return version.ToModel();
    }
}