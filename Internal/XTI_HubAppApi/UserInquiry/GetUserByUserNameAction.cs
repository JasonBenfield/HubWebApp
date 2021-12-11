using System.Threading.Tasks;
using XTI_Hub;
using XTI_App.Abstractions;
using XTI_App.Api;

namespace XTI_HubAppApi.UserInquiry
{
    public sealed class GetUserByUserNameAction : AppAction<string, AppUserModel>
    {
        private readonly AppFactory factory;

        public GetUserByUserNameAction(AppFactory factory)
        {
            this.factory = factory;
        }

        public async Task<AppUserModel> Execute(string userName)
        {
            var user = await factory.Users.User(new AppUserName(userName));
            return user.ToModel();
        }
    }
}
