using System.Threading.Tasks;
using XTI_App.Abstractions;
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
            var user = await hubClient.UserInquiry.GetCurrentUser();
            return new AppUserName(user.UserName);
        }

        public async Task<IAppUser> User(AppUserName userName)
        {
            var user = await hubClient.UserInquiry.GetUserByUserName(userName.Value);
            return new HubClientUser(hubClient, appContext, user);
        }

        public async Task<IAppUser> User()
        {
            var user = await hubClient.UserInquiry.GetCurrentUser();
            return new HubClientUser(hubClient, appContext, user);
        }
    }
}
