using XTI_App.Api;

namespace XTI_HubAppClient
{
    public sealed class HcUserContext : ISourceUserContext
    {
        private readonly HubAppClient hubClient;

        public HcUserContext(HubAppClient hubClient)
        {
            this.hubClient = hubClient;
        }

        public Task<UserContextModel> User(AppUserName userName) =>
            hubClient.System.GetUserContext(new GetUserContextRequest { UserName = userName.Value });

        public async Task<UserContextModel> User()
        {
            var userName = await hubClient.UserName();
            var user = await User(new AppUserName(userName));
            return user;
        }
    }
}
