using System.Threading.Tasks;
using XTI_App.Abstractions;

namespace XTI_HubAppClient
{
    internal sealed class HubClientVersion : IAppVersion
    {
        private readonly HubAppClient hubClient;
        private readonly HubClientAppContext appContext;
        private readonly AppVersionKey versionKey;

        public HubClientVersion(HubAppClient hubClient, HubClientAppContext appContext, AppVersionModel model)
        {
            this.hubClient = hubClient;
            this.appContext = appContext;
            ID = new EntityID(model.ID);
            versionKey = AppVersionKey.Parse(model.VersionKey);
        }

        public EntityID ID { get; }
        public AppVersionKey Key() => versionKey;

        public async Task<IResourceGroup> ResourceGroup(ResourceGroupName name)
        {
            var appModifier = await appContext.GetModifierKey();
            var request = new GetVersionResourceGroupRequest
            {
                VersionKey = versionKey.Value,
                GroupName = name.Value
            };
            var group = await hubClient.Version.GetResourceGroup(appModifier, request);
            return new HubClientResourceGroup(hubClient, appContext, versionKey, group);
        }
    }
}
