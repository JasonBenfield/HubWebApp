namespace XTI_HubWebAppApi.AppList;

public sealed class GetAppsAction : AppAction<EmptyRequest, AppModel[]>
{
    private readonly HubFactory appFactory;
    private readonly IUserContext userContext;

    public GetAppsAction(HubFactory appFactory, IUserContext userContext)
    {
        this.appFactory = appFactory;
        this.userContext = userContext;
    }

    public async Task<AppModel[]> Execute(EmptyRequest model, CancellationToken stoppingToken)
    {
        var currentUser = await userContext.User();
        var user = await appFactory.Users.User(currentUser.User.ID);
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