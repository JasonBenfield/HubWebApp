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
            AppVersionKey version,
            IServiceProvider services
        )
            : base
            (
                  HubAppKey.Key,
                  version,
                  user,
                  ResourceAccess.AllowAuthenticated()
                    .WithAllowed(HubRoles.Instance.Admin)
            )
        {
            UserAdmin = AddGroup(u => new UserAdminGroup(this, Access, u, new UserAdminActionFactory(services)));
            Apps = AddGroup(u => new AppsGroup(this, Access, u, new AppsActionFactory(services)));
        }

        public UserAdminGroup UserAdmin { get; }
        public AppsGroup Apps { get; }
    }
}
