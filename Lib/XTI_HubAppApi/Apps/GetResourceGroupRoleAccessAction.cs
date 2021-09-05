using System.Linq;
using System.Threading.Tasks;
using XTI_App;
using XTI_App.Api;

namespace XTI_HubAppApi.Apps
{
    public sealed class GetResourceGroupRoleAccessAction : AppAction<int, AppRoleModel[]>
    {
        private readonly AppFromPath appFromPath;

        public GetResourceGroupRoleAccessAction(AppFromPath appFromPath)
        {
            this.appFromPath = appFromPath;
        }

        public async Task<AppRoleModel[]> Execute(int groupID)
        {
            var app = await appFromPath.Value();
            var version = await app.CurrentVersion();
            var group = await version.ResourceGroup(groupID);
            var allowedRoles = await group.AllowedRoles();
            var allowedRoleModels = allowedRoles.Select(ar => ar.ToModel()).ToArray();
            return allowedRoleModels;
        }
    }
}
