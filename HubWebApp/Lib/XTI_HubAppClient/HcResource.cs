namespace XTI_HubAppClient
{
    internal sealed class HcResource : IResource
    {
        private readonly ResourceName name;

        public HcResource(ResourceModel model)
        {
            ID = model.ID;
            name = new ResourceName(model.Name);
        }

        public int ID { get; }
        public ResourceName Name() => name;
    }
}
