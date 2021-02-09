using HubWebAppApi.Apps;
using System.Linq;
using System.Threading.Tasks;
using XTI_App;
using XTI_App.Api;

namespace HubWebAppApi.Users
{
    public sealed class GetUserRoleAccessAction : AppAction<int, UserRoleAccessModel>
    {
        private readonly AppFromPath appFromPath;
        private readonly AppFactory factory;

        public GetUserRoleAccessAction(AppFromPath appFromPath, AppFactory factory)
        {
            this.appFromPath = appFromPath;
            this.factory = factory;
        }

        public async Task<UserRoleAccessModel> Execute(int userID)
        {
            var app = await appFromPath.Value();
            var user = await factory.Users().User(userID);
            var unassignedRoles = await user.UnassignedRoles(app);
            var assignedRoles = await user.AssignedRoles(app);
            return new UserRoleAccessModel
            (
                unassignedRoles.Select(role => role.ToModel()).ToArray(),
                assignedRoles
            );
        }
    }
}
