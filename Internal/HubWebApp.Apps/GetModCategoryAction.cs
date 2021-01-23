using System.Threading.Tasks;
using XTI_App;
using XTI_App.Api;

namespace HubWebApp.Apps
{
    public sealed class GetModCategoryAction : AppAction<int, ModifierCategoryModel>
    {
        private readonly AppFromPath appFromPath;

        public GetModCategoryAction(AppFromPath appFromPath)
        {
            this.appFromPath = appFromPath;
        }

        public async Task<ModifierCategoryModel> Execute(int categoryID)
        {
            var app = await appFromPath.Value();
            var modCategory = await app.ModCategory(categoryID);
            return modCategory.ToModel();
        }
    }
}
