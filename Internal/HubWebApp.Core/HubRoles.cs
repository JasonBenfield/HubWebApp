using XTI_App;

namespace HubWebApp.Core
{
    public sealed class HubRoles : AppRoleNames
    {
        public static HubRoles Instance = new HubRoles();

        private HubRoles() : base(HubAppKey.Key)
        {
            Admin = Add("Admin");
        }

        public AppRoleName Admin { get; }
    }
}
