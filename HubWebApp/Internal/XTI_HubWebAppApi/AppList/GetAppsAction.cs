namespace XTI_HubWebAppApi.AppList;

public sealed class GetAppsAction : AppAction<EmptyRequest, AppWithModKeyModel[]>
{
    private readonly HubFactory appFactory;
    private readonly IUserContext userContext;

    public GetAppsAction(HubFactory appFactory, IUserContext userContext)
    {
        this.appFactory = appFactory;
        this.userContext = userContext;
    }

    public async Task<AppWithModKeyModel[]> Execute(EmptyRequest model, CancellationToken stoppingToken)
    {
        var currentUser = await userContext.User();
        var user = await appFactory.Users.User(currentUser.User.ID);
        var apps = await appFactory.Apps.All();
        var hubApp = apps.First(a => a.Key().Equals(HubInfo.AppKey));
        var appsModCategory = await hubApp.ModCategory(HubInfo.ModCategories.Apps);
        var allowedApps = new List<AppWithModKeyModel>();
        foreach (var app in apps.Where(a => !a.Key().Equals(AppKey.Unknown)))
        {
            var appModel = app.ToAppModel();
            var modifier = await appsModCategory.AddOrUpdateModifier(appModel.PublicKey, appModel.ID, appModel.AppKey.Format());
            var userRoles = await user.Modifier(modifier).AssignedRoles();
            if (userRoles.Any() && !userRoles.Any(ur => ur.Name().Equals(AppRoleName.DenyAccess)))
            {
                allowedApps.Add
                (
                    new AppWithModKeyModel(app.ToAppModel(), modifier.ModKey().DisplayText)
                );
            }
        }
        return allowedApps.ToArray();
    }
}