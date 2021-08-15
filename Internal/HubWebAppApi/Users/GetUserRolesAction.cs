using HubWebAppApi.Apps;
using System.Linq;
using System.Threading.Tasks;
using XTI_App;
using XTI_App.Api;

namespace HubWebAppApi.Users
{
    public sealed class GetUserRolesAction : AppAction<int, AppRoleModel[]>
    {
        private readonly AppFromPath appFromPath;
        private readonly AppFactory factory;

        public GetUserRolesAction(AppFromPath appFromPath, AppFactory factory)
        {
            this.appFromPath = appFromPath;
            this.factory = factory;
        }

        public async Task<AppRoleModel[]> Execute(int userID)
        {
            var app = await appFromPath.Value();
            var user = await factory.Users().User(userID);
            var modifier = await app.DefaultModifier();
            var roles = await user.AssignedRoles(app, modifier);
            var roleModels = roles.Select(r => r.ToModel()).ToArray();
            return roleModels;
        }
    }
}
