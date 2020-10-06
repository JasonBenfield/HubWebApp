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
            IAuthGroupFactory authGroupFactory,
            IUserAdminFactory userAdminFactory
        )
            : base
            (
                  HubAppKey.Value,
                  user
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
