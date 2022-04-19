using System.Threading.Tasks;
using XTI_App.Abstractions;

namespace XTI_HubAppClient
{
    internal sealed class HcModifier : IModifier
    {
        private readonly HubAppClient hubClient;
        private readonly HcAppContext appContext;
        private readonly ModifierKey modKey;

        public HcModifier(HubAppClient hubClient, HcAppContext appContext, ModifierModel model)
        {
            this.hubClient = hubClient;
            this.appContext = appContext;
            ID = new EntityID(model.ID);
            modKey = new ModifierKey(model.ModKey);
        }

        public EntityID ID { get; }
        public ModifierKey ModKey() => modKey;

        public async Task<IModifier> DefaultModifier()
        {
            var appModifier = await appContext.GetModifierKey();
            var defaultModifier = await hubClient.App.GetDefaultModifier(appModifier);
            return new HcModifier(hubClient, appContext, defaultModifier);
        }

    }
}
