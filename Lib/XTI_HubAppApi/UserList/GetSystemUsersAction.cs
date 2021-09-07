using System.Linq;
using System.Threading.Tasks;
using XTI_App;
using XTI_App.Api;

namespace XTI_HubAppApi.UserList
{
    public sealed class GetSystemUsersAction : AppAction<AppKeyModel, AppUserModel[]>
    {
        private readonly AppFactory appFactory;

        public GetSystemUsersAction(AppFactory appFactory)
        {
            this.appFactory = appFactory;
        }

        public async Task<AppUserModel[]> Execute(AppKeyModel model)
        {
            var appKey = model.ToAppKey();
            var users = await appFactory.Users().SystemUsers(appKey);
            var userModels = users.Select(u => u.ToModel()).ToArray();
            return userModels;
        }
    }
}
