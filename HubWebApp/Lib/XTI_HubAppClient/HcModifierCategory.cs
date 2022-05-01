using System.Threading.Tasks;
using XTI_App.Abstractions;

namespace XTI_HubAppClient
{
    internal sealed class HcModifierCategory : IModifierCategory
    {
        private readonly HubAppClient hubClient;
        private readonly HcAppContext appContext;
        private readonly ModifierCategoryName name;

        public HcModifierCategory(HubAppClient hubClient, HcAppContext appContext, ModifierCategoryModel model)
        {
            this.hubClient = hubClient;
            this.appContext = appContext;
            ID = model.ID;
            name = new ModifierCategoryName(model.Name);
        }

        public int ID { get; }
        public ModifierCategoryName Name() => name;

        public async Task<IModifier> ModifierOrDefault(ModifierKey modKey)
        {
            var appModifier = await appContext.GetModifierKey();
            var request = new GetModCategoryModifierRequest
            {
                CategoryID = ID,
                ModifierKey = modKey.Value
            };
            var modifier = await hubClient.ModCategory.GetModifier(appModifier, request);
            return new HcModifier(hubClient, appContext, modifier);
        }

    }
}
