using HubWebAppApi.Apps;
using System.Linq;
using System.Threading.Tasks;
using XTI_App;
using XTI_App.Api;

namespace HubWebAppApi.Users
{
    public sealed class GetUserRoleAccessAction : AppAction<GetUserRoleAccessRequest, UserRoleAccessModel>
    {
        private readonly AppFromPath appFromPath;
        private readonly AppFactory factory;

        internal GetUserRoleAccessAction(AppFromPath appFromPath, AppFactory factory)
        {
            this.appFromPath = appFromPath;
            this.factory = factory;
        }

        public async Task<UserRoleAccessModel> Execute(GetUserRoleAccessRequest model)
        {
            var app = await appFromPath.Value();
            var user = await factory.Users().User(model.UserID);
            var modifier = await factory.Modifiers().Modifier(model.ModifierID);
            var unassignedRoles = await user.ExplicitlyUnassignedRoles(modifier);
            var assignedRoles = await user.ExplicitlyAssignedRoles(modifier);
            return new UserRoleAccessModel
            (
                unassignedRoles.Select(role => role.ToModel()).ToArray(),
                assignedRoles.Select(role => role.ToModel()).ToArray()
            );
        }
    }
}
