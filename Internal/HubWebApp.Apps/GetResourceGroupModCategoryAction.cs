using System.Threading.Tasks;
using XTI_App;
using XTI_App.Api;

namespace HubWebApp.Apps
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
            var group = await app.ResourceGroup(groupID);
            var modCategory = await group.ModCategory();
            return modCategory.ToModel();
        }
    }
}
