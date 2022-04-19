using XTI_App.Abstractions;

namespace XTI_HubAppClient
{
    public sealed class HcRole : IAppRole
    {
        private readonly AppRoleName name;

        public HcRole(AppRoleModel model)
        {
            ID = new EntityID(model.ID);
            name = new AppRoleName(model.Name);
        }

        public EntityID ID { get; }

        public AppRoleName Name() => name;
    }
}
