namespace XTI_HubAppApi.ResourceGroupInquiry;

public sealed class GetRoleAccessAction : AppAction<GetResourceGroupRoleAccessRequest, AppRoleModel[]>
{
    private readonly AppFromPath appFromPath;

    public GetRoleAccessAction(AppFromPath appFromPath)
    {
        this.appFromPath = appFromPath;
    }

    public async Task<AppRoleModel[]> Execute(GetResourceGroupRoleAccessRequest model, CancellationToken stoppingToken)
    {
        var app = await appFromPath.Value();
        var versionKey = AppVersionKey.Parse(model.VersionKey);
        var version = await app.Version(versionKey);
        var group = await version.ResourceGroup(model.GroupID);
        var allowedRoles = await group.AllowedRoles();
        var allowedRoleModels = allowedRoles.Select(ar => ar.ToModel()).ToArray();
        return allowedRoleModels;
    }
}