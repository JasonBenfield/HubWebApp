using HubWebAppApi.Apps;
using System.Threading.Tasks;
using XTI_App;
using XTI_App.Api;

namespace HubWebAppApi.Users
{
    public sealed class UnassignRoleAction : AppAction<int, EmptyActionResult>
    {
        private readonly AppFromPath appFromPath;

        public UnassignRoleAction(AppFromPath appFromPath)
        {
            this.appFromPath = appFromPath;
        }

        public async Task<EmptyActionResult> Execute(int userRoleID)
        {
            var app = await appFromPath.Value();
            var userRole = await app.UserRole(userRoleID);
            await userRole.Delete();
            return new EmptyActionResult();
        }
    }
}
