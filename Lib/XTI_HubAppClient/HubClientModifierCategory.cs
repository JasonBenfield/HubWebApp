using System.Threading.Tasks;
using XTI_App.Abstractions;

namespace XTI_HubAppClient
{
    internal sealed class HubClientModifierCategory : IModifierCategory
    {
        private readonly HubAppClient hubClient;
        private readonly HubClientAppContext appContext;
        private readonly ModifierCategoryName name;

        public HubClientModifierCategory(HubAppClient hubClient, HubClientAppContext appContext, ModifierCategoryModel model)
        {
            this.hubClient = hubClient;
            this.appContext = appContext;
            ID = new EntityID(model.ID);
            name = new ModifierCategoryName(model.Name);
        }

        public EntityID ID { get; }
        public ModifierCategoryName Name() => name;

        public async Task<IModifier> Modifier(ModifierKey modKey)
        {
            var appModifier = await appContext.GetModifierKey();
            var request = new GetModCategoryModifierRequest
            {
                CategoryID = ID.Value,
                ModifierKey = modKey.Value
            };
            var modifier = await hubClient.ModCategory.GetModifier(appModifier, request);
            return new HubClientModifier(hubClient, appContext, modifier);
        }

    }
}
