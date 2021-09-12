using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XTI_Hub;
using XTI_App.Api;

namespace XTI_HubAppApi.ModCategoryInquiry
{
    public sealed class GetModifiersAction : AppAction<int, ModifierModel[]>
    {
        private readonly AppFromPath appFromPath;

        public GetModifiersAction(AppFromPath appFromPath)
        {
            this.appFromPath = appFromPath;
        }

        public async Task<ModifierModel[]> Execute(int modCategoryID)
        {
            var app = await appFromPath.Value();
            var modCategory = await app.ModCategory(modCategoryID);
            var modifiers = await modCategory.Modifiers();
            return modifiers.Select(m => m.ToModel()).ToArray();
        }
    }
}
