using XTI_App.Api;

namespace XTI_HubAppClient
{
    public sealed class HcUserContext : ISourceUserContext
    {
        private readonly HubAppClient hubClient;
        private readonly HcAppContext appContext;

        public HcUserContext(HubAppClient hubClient, HcAppContext appContext)
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
                user = new HcUser(hubClient, appContext, _user);
            }
            return user;
        }

        public async Task<IAppUser> User()
        {
            var user = await hubClient.UserInquiry.GetCurrentUser();
            return new HcUser(hubClient, appContext, user);
        }
    }
}
