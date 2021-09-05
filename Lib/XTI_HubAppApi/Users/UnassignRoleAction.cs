using XTI_HubAppApi.Apps;
using System.Threading.Tasks;
using XTI_App;
using XTI_App.Api;

namespace XTI_HubAppApi.Users
{
    public sealed class UnassignRoleAction : AppAction<UserRoleRequest, EmptyActionResult>
    {
        private readonly AppFromPath appFromPath;
        private readonly AppFactory factory;

        public UnassignRoleAction(AppFromPath appFromPath, AppFactory factory)
        {
            this.appFromPath = appFromPath;
            this.factory = factory;
        }

        public async Task<EmptyActionResult> Execute(UserRoleRequest model)
        {
            var app = await appFromPath.Value();
            var user = await factory.Users().User(model.UserID);
            var role = await app.Role(model.RoleID);
            await user.RemoveRole(role);
            return new EmptyActionResult();
        }
    }
}
