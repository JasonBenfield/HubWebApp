namespace XTI_HubWebAppApiActions.AppInquiry;

public sealed class GetRolesAction : AppAction<EmptyRequest, AppRoleModel[]>
{
    private readonly AppFromPath appFromPath;

    public GetRolesAction(AppFromPath appFromPath)
    {
        this.appFromPath = appFromPath;
    }

    public async Task<AppRoleModel[]> Execute(EmptyRequest model, CancellationToken stoppingToken)
    {
        var app = await appFromPath.Value();
        var roles = await app.Roles();
        return roles.Select(r => r.ToModel()).ToArray();
    }
}
