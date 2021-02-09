using HubWebAppApi.Apps;
using System.Threading.Tasks;
using XTI_App;
using XTI_App.Api;

namespace HubWebAppApi.Users
{
    public sealed class GetUserRolesAction : AppAction<int, AppUserRoleModel[]>
    {
        private readonly AppFromPath appFromPath;
        private readonly AppFactory factory;

        public GetUserRolesAction(AppFromPath appFromPath, AppFactory factory)
        {
            this.appFromPath = appFromPath;
            this.factory = factory;
        }

        public async Task<AppUserRoleModel[]> Execute(int userID)
        {
            var app = await appFromPath.Value();
            var user = await factory.Users().User(userID);
            var userRoles = await user.AssignedRoles(app);
            return userRoles;
        }
    }
}
