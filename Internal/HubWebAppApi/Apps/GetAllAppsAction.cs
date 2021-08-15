using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XTI_App;
using XTI_App.Api;

namespace HubWebAppApi.Apps
{
    public sealed class GetAllAppsAction : AppAction<EmptyRequest, AppModel[]>
    {
        private readonly AppFactory appFactory;
        private readonly IUserContext userContext;

        public GetAllAppsAction(AppFactory appFactory, IUserContext userContext)
        {
            this.appFactory = appFactory;
            this.userContext = userContext;
        }

        public async Task<AppModel[]> Execute(EmptyRequest model)
        {
            var currentUser = await userContext.User();
            var user = await appFactory.Users().User(currentUser.ID.Value);
            var apps = await appFactory.Apps().All();
            var hubApp = apps.First(a => a.Key().Equals(HubInfo.AppKey));
            var appsModCategory = await hubApp.ModCategory(HubInfo.ModCategories.Apps);
            var allowedApps = new List<App>();
            foreach (var app in apps)
            {
                var modifier = await appsModCategory.Modifier(app.ID.Value);
                var userRoles = await user.AssignedRoles(app, modifier);
                if (userRoles.Any(ur => ur.Name().Equals(HubRoles.Instance.ViewApp) || ur.Name().Equals(HubRoles.Instance.Admin)))
                {
                    allowedApps.Add(app);
                }
            }
            apps = allowedApps;
            return apps.Select(a => a.ToAppModel()).ToArray();
        }
    }
}
