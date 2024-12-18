namespace XTI_HubWebAppApiActions.ResourceInquiry;

public sealed class GetRoleAccessAction : AppAction<GetResourceRoleAccessRequest, AppRoleModel[]>
{
    private readonly AppFromPath appFromPath;

    public GetRoleAccessAction(AppFromPath appFromPath)
    {
        this.appFromPath = appFromPath;
    }

    public async Task<AppRoleModel[]> Execute(GetResourceRoleAccessRequest getRequest, CancellationToken stoppingToken)
    {
        var app = await appFromPath.Value();
        var versionKey = AppVersionKey.Parse(getRequest.VersionKey);
        var version = await app.Version(versionKey);
        var resource = await version.Resource(getRequest.ResourceID);
        var allowedRoles = await resource.AllowedRoles();
        var allowedRoleModels = allowedRoles.Select(ar => ar.ToModel()).ToArray();
        return allowedRoleModels;
    }
}