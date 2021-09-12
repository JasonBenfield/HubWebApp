using System.Linq;
using System.Threading.Tasks;
using XTI_Hub;
using XTI_App.Api;

namespace XTI_HubAppApi.AppInquiry
{
    public sealed class GetRolesAction : AppAction<EmptyRequest, AppRoleModel[]>
    {
        private readonly AppFromPath appFromPath;

        public GetRolesAction(AppFromPath appFromPath)
        {
            this.appFromPath = appFromPath;
        }

        public async Task<AppRoleModel[]> Execute(EmptyRequest model)
        {
            var app = await appFromPath.Value();
            var roles = await app.Roles();
            var roleModels = roles.Select(r => r.ToModel()).ToArray();
            return roleModels;
        }
    }
}
