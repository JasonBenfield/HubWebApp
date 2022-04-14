using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_Hub;

namespace XTI_HubAppApi.ResourceInquiry;

public sealed class GetRoleAccessAction : AppAction<GetResourceRoleAccessRequest, AppRoleModel[]>
{
    private readonly AppFromPath appFromPath;

    public GetRoleAccessAction(AppFromPath appFromPath)
    {
        this.appFromPath = appFromPath;
    }

    public async Task<AppRoleModel[]> Execute(GetResourceRoleAccessRequest model)
    {
        var app = await appFromPath.Value();
        var versionKey = AppVersionKey.Parse(model.VersionKey);
        var version = await app.Version(versionKey);
        var resource = await version.Resource(model.ResourceID);
        var allowedRoles = await resource.AllowedRoles();
        var allowedRoleModels = allowedRoles.Select(ar => ar.ToModel()).ToArray();
        return allowedRoleModels;
    }
}