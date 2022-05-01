namespace XTI_HubAppApi.AppInquiry;

public sealed class GetRolesAction : AppAction<EmptyRequest, AppRoleModel[]>
{
    private readonly AppFromPath appFromPath;

    public GetRolesAction(AppFromPath appFromPath)
    {
        this.appFromPath = appFromPath;
    }

    public async Task<AppRoleModel[]> Execute(EmptyRequest model)
    {
        var app = await appFromPath.Value();
        var roles = await app.Roles();
        var roleModels = roles.Select(r => r.ToModel()).ToArray();
        return roleModels;
    }
}