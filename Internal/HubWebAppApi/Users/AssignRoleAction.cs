using HubWebAppApi.Apps;
using System.Threading.Tasks;
using XTI_App;
using XTI_App.Api;

namespace HubWebAppApi.Users
{
    public sealed class AssignRoleRequest
    {
        public int UserID { get; set; }
        public int RoleID { get; set; }
    }

    public sealed class AssignRoleAction : AppAction<AssignRoleRequest, int>
    {
        private readonly AppFromPath appFromPath;
        private readonly AppFactory appFactory;

        public AssignRoleAction(AppFromPath appFromPath, AppFactory appFactory)
        {
            this.appFromPath = appFromPath;
            this.appFactory = appFactory;
        }

        public async Task<int> Execute(AssignRoleRequest model)
        {
            var app = await appFromPath.Value();
            var role = await app.Role(model.RoleID);
            var user = await appFactory.Users().User(model.UserID);
            var userRole = await user.AddRole(role);
            return userRole.ID.Value;
        }
    }
}
