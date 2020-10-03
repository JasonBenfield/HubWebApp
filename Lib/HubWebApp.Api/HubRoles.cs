using XTI_App;

namespace HubWebApp.Api
{
    public sealed class HubRoles : AppRoleNames
    {
        public static HubRoles Instance = new HubRoles();

        private HubRoles() : base(HubAppApi.AppKey)
        {
            Admin = Add("Admin");
        }

        public AppRoleName Admin { get; }
    }
}
