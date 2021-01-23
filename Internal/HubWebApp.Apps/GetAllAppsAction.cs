using HubWebApp.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XTI_App;
using XTI_App.Api;

namespace HubWebApp.Apps
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
            var user = await userContext.UncachedUser();
            var apps = await appFactory.Apps().All();
            var hubApp = apps.First(a => a.Key().Equals(HubInfo.AppKey));
            var appsModCategory = await hubApp.ModCategory(HubInfo.ModCategories.Apps);
            var isModCategoryAdmin = await user.IsModCategoryAdmin(appsModCategory);
            if (!isModCategoryAdmin)
            {
                var userModifiers = await user.Modifiers(appsModCategory);
                var allowedApps = new List<App>();
                foreach (var app in apps)
                {
                    var modifier = await appsModCategory.Modifier(app.ID.Value);
                    if (userModifiers.Any(um => um.ModKey().Equals(modifier.ModKey())))
                    {
                        allowedApps.Add(app);
                    }
                }
                apps = allowedApps;
            }
            return apps.Select(a => a.ToAppModel()).ToArray();
        }
    }
}
