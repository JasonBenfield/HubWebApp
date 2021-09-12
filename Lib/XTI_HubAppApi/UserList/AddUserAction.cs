using System.Threading.Tasks;
using XTI_App.Api;
using XTI_Hub;
using XTI_Core;
using XTI_App.Abstractions;

namespace XTI_HubAppApi.UserList
{
    public sealed class AddUserAction : AppAction<AddUserModel, int>
    {
        public AddUserAction(AppFactory appFactory, IHashedPasswordFactory hashedPasswordFactory, Clock clock)
        {
            this.appFactory = appFactory;
            this.hashedPasswordFactory = hashedPasswordFactory;
            this.clock = clock;
        }

        private readonly AppFactory appFactory;
        private readonly IHashedPasswordFactory hashedPasswordFactory;
        private readonly Clock clock;

        public async Task<int> Execute(AddUserModel model)
        {
            var userName = new AppUserName(model.UserName);
            var hashedPassword = hashedPasswordFactory.Create(model.Password);
            var timeAdded = clock.Now();
            var user = await appFactory.Users().Add(userName, hashedPassword, timeAdded);
            return user.ID.Value;
        }
    }
}
