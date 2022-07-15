using XTI_App.Api;

namespace XTI_HubAppClient
{
    public sealed class HcUserContext : ISourceUserContext
    {
        private readonly HubAppClient hubClient;
        private readonly ICurrentUserName currentUserName;

        public HcUserContext(HubAppClient hubClient, ICurrentUserName currentUserName)
        {
            this.hubClient = hubClient;
            this.currentUserName = currentUserName; 
        }

        public Task<UserContextModel> User(AppUserName userName) =>
            hubClient.System.GetUserContext(new GetUserContextRequest { UserName = userName.Value });

        public async Task<UserContextModel> User()
        {
            var userName = await currentUserName.Value();
            var user = await User(userName);
            return user;
        }
    }
}
