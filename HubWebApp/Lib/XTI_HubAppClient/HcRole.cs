namespace XTI_HubAppClient
{
    public sealed class HcRole : IAppRole
    {
        private readonly AppRoleName name;

        public HcRole(AppRoleModel model)
        {
            ID = model.ID;
            name = new AppRoleName(model.Name);
        }

        public int ID { get; }

        public AppRoleName Name() => name;
    }
}
