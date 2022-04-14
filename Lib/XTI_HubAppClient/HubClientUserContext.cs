using XTI_App.Api;

namespace XTI_HubAppClient
{
    public sealed class HubClientUserContext : ISourceUserContext
    {
        private readonly HubAppClient hubClient;
        private readonly HubClientAppContext appContext;

        public HubClientUserContext(HubAppClient hubClient, HubClientAppContext appContext)
        {
            this.hubClient = hubClient;
            this.appContext = appContext;
        }

        public async Task<AppUserName> CurrentUserName()
        {
            var userName = await hubClient.UserName();
            return new AppUserName(userName);
        }

        public async Task<IAppUser> User(AppUserName userName)
        {
            IAppUser user;
            var currentUserName = await CurrentUserName();
            if (userName.Equals(currentUserName))
            {
                user = await User();
            }
            else
            {
                var _user = await hubClient.UserInquiry.GetUserByUserName(userName.Value);
                user = new HubClientUser(hubClient, appContext, _user);
            }
            return user;
        }

        public async Task<IAppUser> User()
        {
            var user = await hubClient.UserInquiry.GetCurrentUser();
            return new HubClientUser(hubClient, appContext, user);
        }
    }
}
