using System.Threading.Tasks;
using XTI_App;
using XTI_App.Api;

namespace XTI_HubAppApi.AppUserMaintenance
{
    public sealed class AssignRoleAction : AppAction<UserRoleRequest, int>
    {
        private readonly AppFromPath appFromPath;
        private readonly AppFactory appFactory;

        public AssignRoleAction(AppFromPath appFromPath, AppFactory appFactory)
        {
            this.appFromPath = appFromPath;
            this.appFactory = appFactory;
        }

        public async Task<int> Execute(UserRoleRequest model)
        {
            var app = await appFromPath.Value();
            var role = await app.Role(model.RoleID);
            var user = await appFactory.Users().User(model.UserID);
            var defaultModifier = await app.DefaultModifier();
            await user.AddRole(role, defaultModifier);
            return role.ID.Value;
        }
    }
}
