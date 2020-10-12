using HubWebApp.AuthApi;
using HubWebApp.Core;
using HubWebApp.UserAdminApi;
using XTI_App.Api;

namespace HubWebApp.Api
{
    public sealed class HubAppApi : AppApi
    {
        public HubAppApi
        (
            IAppApiUser user,
            string version,
            AuthGroupFactory authGroupFactory,
            UserAdminGroupFactory userAdminFactory
        )
            : base
            (
                  HubAppKey.Value,
                  version,
                  user,
                  ResourceAccess.AllowAuthenticated()
                    .WithAllowed(HubRoles.Instance.Admin)
            )
        {
            Auth = AddGroup((u) => new AuthGroup(this, authGroupFactory));
            AuthApi = AddGroup((u) => new AuthApiGroup(this, authGroupFactory));
            UserAdmin = AddGroup(u => new UserAdminGroup(this, u, userAdminFactory));
        }

        public AuthGroup Auth { get; }
        public AuthApiGroup AuthApi { get; }
        public UserAdminGroup UserAdmin { get; }
    }
}
