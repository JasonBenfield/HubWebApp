using System.Linq;
using System.Threading.Tasks;
using XTI_App.Api;

namespace HubWebApp.Apps
{
    public sealed class GetResourceRoleAccessAction : AppAction<int, RoleAccessModel>
    {
        private readonly AppFromPath appFromPath;

        public GetResourceRoleAccessAction(AppFromPath appFromPath)
        {
            this.appFromPath = appFromPath;
        }

        public async Task<RoleAccessModel> Execute(int resourceID)
        {
            var app = await appFromPath.Value();
            var resource = await app.Resource(resourceID);
            var allowedRoles = await resource.AllowedRoles();
            var deniedRoles = await resource.DeniedRoles();
            var roleAccess = new RoleAccessModel
            {
                AllowedRoles = allowedRoles.Select(ar => ar.ToModel()).ToArray(),
                DeniedRoles = deniedRoles.Select(dr => dr.ToModel()).ToArray()
            };
            return roleAccess;
        }
    }
}
