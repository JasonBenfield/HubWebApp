using System.Linq;
using System.Threading.Tasks;
using XTI_App;
using XTI_App.Api;

namespace XTI_HubAppApi.Apps
{
    public sealed class GetModCategoryResourceGroupsAction : AppAction<int, ResourceGroupModel[]>
    {
        private readonly AppFromPath appFromPath;

        public GetModCategoryResourceGroupsAction(AppFromPath appFromPath)
        {
            this.appFromPath = appFromPath;
        }

        public async Task<ResourceGroupModel[]> Execute(int categoryID)
        {
            var app = await appFromPath.Value();
            var modCategory = await app.ModCategory(categoryID);
            var resourceGroups = await modCategory.ResourceGroups();
            return resourceGroups.Select(rg => rg.ToModel()).ToArray();
        }
    }
}
