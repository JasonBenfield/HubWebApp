using HubWebApp.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XTI_App;
using XTI_App.Api;

namespace HubWebApp.Apps
{
    public sealed class AllAction : AppAction<EmptyRequest, AppModel[]>
    {
        private readonly AppFactory appFactory;
        private readonly IUserContext userContext;

        public AllAction(AppFactory appFactory, IUserContext userContext)
        {
            this.appFactory = appFactory;
            this.userContext = userContext;
        }

        public async Task<AppModel[]> Execute(EmptyRequest model)
        {
            var user = await userContext.User();
            var apps = await appFactory.Apps().All();
            var hubApp = apps.First(a => a.Key().Equals(HubAppKey.Key));
            var appsModCategory = await hubApp.ModCategory(new ModifierCategoryName("Apps"));
            var isModCategoryAdmin = await user.IsModCategoryAdmin(appsModCategory);
            if (!isModCategoryAdmin)
            {
                var allowedApps = new List<App>();
                foreach (var app in apps)
                {
                    var modifier = await appsModCategory.Modifier(app.ID.Value);
                    var hasModifier = await user.HasModifier(modifier.ModKey());
                    if (hasModifier)
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
