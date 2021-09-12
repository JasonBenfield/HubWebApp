using System.Threading.Tasks;
using XTI_Hub;
using XTI_App.Api;

namespace XTI_HubAppApi.UserInquiry
{
    public sealed class GetUserAction : AppAction<int, AppUserModel>
    {
        private readonly AppFactory factory;

        public GetUserAction(AppFactory factory)
        {
            this.factory = factory;
        }

        public async Task<AppUserModel> Execute(int userID)
        {
            var user = await factory.Users().User(userID);
            return user.ToModel();
        }
    }
}
