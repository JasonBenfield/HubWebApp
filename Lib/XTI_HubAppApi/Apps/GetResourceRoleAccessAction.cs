using System.Linq;
using System.Threading.Tasks;
using XTI_App;
using XTI_App.Api;

namespace XTI_HubAppApi.Apps
{
    public sealed class GetResourceRoleAccessAction : AppAction<int, AppRoleModel[]>
    {
        private readonly AppFromPath appFromPath;

        public GetResourceRoleAccessAction(AppFromPath appFromPath)
        {
            this.appFromPath = appFromPath;
        }

        public async Task<AppRoleModel[]> Execute(int resourceID)
        {
            var app = await appFromPath.Value();
            var version = await app.CurrentVersion();
            var resource = await version.Resource(resourceID);
            var allowedRoles = await resource.AllowedRoles();
            var allowedRoleModels = allowedRoles.Select(ar => ar.ToModel()).ToArray();
            return allowedRoleModels;
        }
    }
}
