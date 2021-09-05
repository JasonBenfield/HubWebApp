using System.Threading.Tasks;
using XTI_App;
using XTI_App.Api;

namespace XTI_HubAppApi.Apps
{
    public sealed class GetResourceGroupModCategoryAction : AppAction<int, ModifierCategoryModel>
    {
        private readonly AppFromPath appFromPath;

        public GetResourceGroupModCategoryAction(AppFromPath appFromPath)
        {
            this.appFromPath = appFromPath;
        }

        public async Task<ModifierCategoryModel> Execute(int groupID)
        {
            var app = await appFromPath.Value();
            var version = await app.CurrentVersion();
            var group = await version.ResourceGroup(groupID);
            var modCategory = await group.ModCategory();
            return modCategory.ToModel();
        }
    }
}
