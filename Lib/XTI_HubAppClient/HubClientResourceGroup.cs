using System.Threading.Tasks;
using XTI_App.Abstractions;

namespace XTI_HubAppClient
{
    internal sealed class HubClientResourceGroup : IResourceGroup
    {
        private readonly HubAppClient hubClient;
        private readonly HubClientAppContext appContext;
        private readonly AppVersionKey versionKey;
        private readonly ResourceGroupName name;

        public HubClientResourceGroup(HubAppClient hubClient, HubClientAppContext appContext, AppVersionKey versionKey, ResourceGroupModel model)
        {
            this.hubClient = hubClient;
            this.appContext = appContext;
            this.versionKey = versionKey;
            ID = new EntityID(model.ID);
            name = new ResourceGroupName(model.Name);
        }

        public EntityID ID { get; }
        public ResourceGroupName Name() => name;

        public async Task<IModifierCategory> ModCategory()
        {
            var appModifier = await appContext.GetModifierKey();
            var request = new GetResourceGroupModCategoryRequest
            {
                VersionKey = versionKey.Value,
                GroupID = ID.Value
            };
            var modCategory = await hubClient.ResourceGroup.GetModCategory(appModifier, request);
            return new HubClientModifierCategory(hubClient, appContext, modCategory);
        }

        public async Task<IResource> Resource(ResourceName name)
        {
            var appModifier = await appContext.GetModifierKey();
            var request = new GetResourceGroupResourceRequest
            {
                VersionKey = versionKey.Value,
                GroupID = ID.Value,
                ResourceName = name.Value
            };
            var resource = await hubClient.ResourceGroup.GetResource(appModifier, request);
            return new HubClientResource(resource);
        }
    }
}
