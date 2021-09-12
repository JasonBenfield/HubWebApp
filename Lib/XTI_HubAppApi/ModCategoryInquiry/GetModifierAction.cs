using System.Threading.Tasks;
using XTI_Hub;
using XTI_App.Abstractions;
using XTI_App.Api;

namespace XTI_HubAppApi.ModCategoryInquiry
{
    public sealed class GetModCategoryModifierRequest
    {
        public int CategoryID { get; set; }
        public string ModifierKey { get; set; }
    }
    public sealed class GetModifierAction : AppAction<GetModCategoryModifierRequest, ModifierModel>
    {
        private readonly AppFromPath appFromPath;

        public GetModifierAction(AppFromPath appFromPath)
        {
            this.appFromPath = appFromPath;
        }

        public async Task<ModifierModel> Execute(GetModCategoryModifierRequest model)
        {
            var app = await appFromPath.Value();
            var modCategory = await app.ModCategory(model.CategoryID);
            var modKey = new ModifierKey(model.ModifierKey);
            var modifier = await modCategory.Modifier(modKey);
            var modifierModel = modifier.ToModel();
            return modifierModel;
        }
    }
}
