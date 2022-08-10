using XTI_App.Api;

namespace XTI_HubAppClient
{
    public sealed class HcUserContext : ISourceUserContext
    {
        private readonly HubAppClient hubClient;
        private readonly ICurrentUserName currentUserName;
        private readonly AppVersionKey versionKey;

        public HcUserContext(HubAppClient hubClient, ICurrentUserName currentUserName, AppVersionKey versionKey)
        {
            this.hubClient = hubClient;
            this.currentUserName = currentUserName;
            this.versionKey = versionKey;
        }

        public async Task<UserContextModel> User()
        {
            var userName = await currentUserName.Value();
            var user = await User(userName);
            return user;
        }

        public Task<UserContextModel> User(AppUserName userName) =>
            hubClient.System.GetUserContext
            (
                new GetUserContextRequest
                {
                    UserName = userName.Value,
                    VersionKey = versionKey.Value
                }
            );
    }
}
