using XTI_App.Abstractions;

namespace XTI_HubAppClient
{
    public sealed class HubClientRole : IAppRole
    {
        private readonly AppRoleName name;

        public HubClientRole(AppRoleModel model)
        {
            ID = new EntityID(model.ID);
            name = new AppRoleName(model.Name);
        }

        public EntityID ID { get; }

        public AppRoleName Name() => name;
    }
}
