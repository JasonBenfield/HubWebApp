using XTI_App.Api;

namespace XTI_HubAppClient
{
    public sealed class HcUserContext : ISourceUserContext
    {
        private readonly HubAppClient hubClient;
        private readonly ICurrentUserName currentUserName;
        private readonly InstallationIDAccessor installationIDAccessor;

        public HcUserContext(HubAppClient hubClient, ICurrentUserName currentUserName, InstallationIDAccessor installationIDAccessor)
        {
            this.hubClient = hubClient;
            this.currentUserName = currentUserName;
            this.installationIDAccessor = installationIDAccessor;
        }

        public async Task<UserContextModel> User()
        {
            var userName = await currentUserName.Value();
            var user = await User(userName);
            return user;
        }

        public async Task<UserContextModel> User(AppUserName userName)
        {
            var installationID = await installationIDAccessor.Value();
            var userContextModel = await hubClient.System.GetUserContext
            (
                new GetUserContextRequest
                {
                    UserName = userName.Value,
                    InstallationID = installationID
                }
            );
            return userContextModel;
        }
    }
}
