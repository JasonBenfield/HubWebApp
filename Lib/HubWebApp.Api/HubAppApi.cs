using HubWebApp.AuthApi;
using HubWebApp.UserAdminApi;
using XTI_WebApp.Api;

namespace HubWebApp.Api
{
    public sealed class HubAppApi : AppApi
    {
        public static readonly string AppKey = "Hub";

        public HubAppApi
        (
            WebAppUser user,
            IAuthGroupFactory authGroupFactory,
            IUserAdminFactory userAdminFactory
        )
            : base(AppKey, user)
        {
            Auth = AddGroup((u) => new AuthGroup(this, authGroupFactory));
            UserAdmin = AddGroup(u => new UserAdminGroup(this, u, userAdminFactory));
        }

        public AuthGroup Auth { get; }
        public UserAdminGroup UserAdmin { get; }
    }
}
