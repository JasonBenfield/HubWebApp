using XTI_App;

namespace HubWebApp.Core
{
    public sealed class HubRoles : AppRoleNames
    {
        public static HubRoles Instance = new HubRoles();

        private HubRoles()
        {
            Admin = Add("Admin");
        }

        public AppRoleName Admin { get; }
    }
}
