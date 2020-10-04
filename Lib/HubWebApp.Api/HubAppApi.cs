using HubWebApp.AuthApi;
using HubWebApp.UserAdminApi;
using XTI_App;
using XTI_App.Api;

namespace HubWebApp.Api
{
    public sealed class HubAppApi : AppApi
    {
        public static readonly string AppKeyValue = "Hub";
        public static readonly AppKey AppKey = new AppKey(AppKeyValue);

        public HubAppApi
        (
            IAppApiUser user,
            IAuthGroupFactory authGroupFactory,
            IUserAdminFactory userAdminFactory
        )
            : base(AppKeyValue, user)
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
