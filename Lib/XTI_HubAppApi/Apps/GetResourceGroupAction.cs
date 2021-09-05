using System.Threading.Tasks;
using XTI_App;
using XTI_App.Api;

namespace XTI_HubAppApi.Apps
{
    public sealed class GetResourceGroupAction : AppAction<int, ResourceGroupModel>
    {
        private readonly AppFromPath appFromPath;

        public GetResourceGroupAction(AppFromPath appFromPath)
        {
            this.appFromPath = appFromPath;
        }

        public async Task<ResourceGroupModel> Execute(int groupID)
        {
            var app = await appFromPath.Value();
            var version = await app.CurrentVersion();
            var group = await version.ResourceGroup(groupID);
            return group.ToModel();
        }
    }
}
