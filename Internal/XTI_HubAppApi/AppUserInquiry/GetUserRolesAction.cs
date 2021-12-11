using System.Linq;
using System.Threading.Tasks;
using XTI_Hub;
using XTI_App.Api;

namespace XTI_HubAppApi.AppUserInquiry
{
    public sealed class GetUserRolesRequest
    {
        public int UserID { get; set; }
        public int ModifierID { get; set; }
    }
    public sealed class GetUserRolesAction : AppAction<GetUserRolesRequest, AppRoleModel[]>
    {
        private readonly AppFromPath appFromPath;
        private readonly AppFactory factory;

        public GetUserRolesAction(AppFromPath appFromPath, AppFactory factory)
        {
            this.appFromPath = appFromPath;
            this.factory = factory;
        }

        public async Task<AppRoleModel[]> Execute(GetUserRolesRequest model)
        {
            var app = await appFromPath.Value();
            var user = await factory.Users.User(model.UserID);
            var modifier = await app.Modifier(model.ModifierID);
            var roles = await user.AssignedRoles(modifier);
            var roleModels = roles.Select(r => r.ToModel()).ToArray();
            return roleModels;
        }
    }
}
