using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_Hub;

namespace XTI_HubAppApi.AppList;

public sealed class GetAppsAction : AppAction<EmptyRequest, AppWithModKeyModel[]>
{
    private readonly AppFactory appFactory;
    private readonly IUserContext userContext;

    public GetAppsAction(AppFactory appFactory, IUserContext userContext)
    {
        this.appFactory = appFactory;
        this.userContext = userContext;
    }

    public async Task<AppWithModKeyModel[]> Execute(EmptyRequest model)
    {
        var currentUser = await userContext.User();
        var user = await appFactory.Users.User(currentUser.ID.Value);
        var apps = await appFactory.Apps.All();
        var hubApp = apps.First(a => a.Key().Equals(HubInfo.AppKey));
        var appsModCategory = await hubApp.ModCategory(HubInfo.ModCategories.Apps);
        var allowedApps = new List<AppWithModKeyModel>();
        foreach (var app in apps.Where(a => !a.Key().Equals(AppKey.Unknown)))
        {
            var modifier = await appsModCategory.AddOrUpdateModifier(app.ID.Value, app.Key().Name.DisplayText);
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