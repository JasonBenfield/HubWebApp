namespace XTI_HubWebAppApi.AppList;

public sealed class GetAppsAction : AppAction<EmptyRequest, AppModel[]>
{
    private readonly CurrentUser currentUser;

    public GetAppsAction(CurrentUser currentUser)
    {
        this.currentUser = currentUser;
    }

    public async Task<AppModel[]> Execute(EmptyRequest model, CancellationToken stoppingToken)
    {
        var user = await currentUser.Value();
        var permissions = await user.GetAppPermissions();
        var allowedApps = new List<AppModel>();
        foreach (var permission in permissions.Where(p => p.CanView))
        {
            var appModel = permission.App.ToModel();
            if (!appModel.AppKey.Equals(AppKey.Unknown))
            {
                allowedApps.Add(appModel);
            }
        }
        return allowedApps.ToArray();
    }
}