using System.Linq;
using System.Threading.Tasks;
using XTI_App.Api;

namespace HubWebApp.Apps
{
    public sealed class GetResourceGroupRoleAccessAction : AppAction<int, RoleAccessModel>
    {
        private readonly AppFromPath appFromPath;

        public GetResourceGroupRoleAccessAction(AppFromPath appFromPath)
        {
            this.appFromPath = appFromPath;
        }

        public async Task<RoleAccessModel> Execute(int groupID)
        {
            var app = await appFromPath.Value();
            var group = await app.ResourceGroup(groupID);
            var allowedRoles = await group.AllowedRoles();
            var deniedRoles = await group.DeniedRoles();
            var roleAccess = new RoleAccessModel
            {
                AllowedRoles = allowedRoles.Select(ar => ar.ToModel()).ToArray(),
                DeniedRoles = deniedRoles.Select(dr => dr.ToModel()).ToArray()
            };
            return roleAccess;
        }
    }
}
