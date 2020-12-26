using HubWebApp.Apps;
using HubWebApp.Core;
using HubWebApp.UserAdminApi;
using System;
using XTI_App;
using XTI_App.Api;
using XTI_WebApp.Api;

namespace HubWebApp.Api
{
    public sealed class HubAppApi : WebAppApi
    {
        public HubAppApi
        (
            IAppApiUser user,
            IServiceProvider services
        )
            : base
            (
                  HubAppKey.Key,
                  user,
                  ResourceAccess.AllowAuthenticated()
                    .WithAllowed(HubRoles.Instance.Admin)
            )
        {
            UserAdmin = AddGroup(u => new UserAdminGroup(this, Access, u, new UserAdminActionFactory(services)));
            Apps = AddGroup(u => new AppsGroup(this, Access, u, new AppsActionFactory(services)));
            App = AddGroup(u => new AppGroup(this, Access, u, new AppsActionFactory(services)));
        }

        public UserAdminGroup UserAdmin { get; }
        public AppsGroup Apps { get; }
        public AppGroup App { get; }
    }
}
