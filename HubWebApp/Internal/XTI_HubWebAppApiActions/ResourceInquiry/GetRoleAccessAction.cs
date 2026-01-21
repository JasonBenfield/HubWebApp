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
        var app = await appFromPath.Value(stoppingToken);
        var versionKey = AppVersionKey.Parse(getRequest.VersionKey);
        var version = await app.Version(versionKey, stoppingToken);
        var resource = await version.Resource(getRequest.ResourceID, stoppingToken);
        var allowedRoles = await resource.AllowedRoles(stoppingToken);
        var allowedRoleModels = allowedRoles.Select(ar => ar.ToModel()).ToArray();
        return allowedRoleModels;
    }
}