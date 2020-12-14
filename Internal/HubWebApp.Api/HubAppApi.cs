using HubWebApp.Core;
using HubWebApp.UserAdminApi;
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
            UserAdminGroupFactory userAdminFactory
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
            UserAdmin = AddGroup(u => new UserAdminGroup(this, u, userAdminFactory));
        }

        public UserAdminGroup UserAdmin { get; }
    }
}
