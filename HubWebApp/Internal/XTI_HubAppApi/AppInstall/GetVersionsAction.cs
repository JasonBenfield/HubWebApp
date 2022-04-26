using XTI_App.Api;
using XTI_Hub;
using XTI_Hub.Abstractions;

namespace XTI_HubAppApi.AppInstall;

internal sealed class GetVersionsAction : AppAction<GetVersionsRequest, XtiVersionModel[]>
{
    private readonly AppFactory appFactory;

    public GetVersionsAction(AppFactory appFactory)
    {
        this.appFactory = appFactory;
    }

    public async Task<XtiVersionModel[]> Execute(GetVersionsRequest request)
    {
        var versions = await appFactory.Versions.VersionsByName(request.VersionName);
        return versions.Select(v => v.ToModel()).ToArray();
    }
}
