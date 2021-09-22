using XTI_App.Abstractions;

namespace XTI_HubAppClient
{
    internal sealed class HubClientResource : IResource
    {
        private readonly ResourceName name;

        public HubClientResource(ResourceModel model)
        {
            ID = new EntityID(model.ID);
            name = new ResourceName(model.Name);
        }

        public EntityID ID { get; }
        public ResourceName Name() => name;
    }
}
