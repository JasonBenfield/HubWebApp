using XTI_App;

namespace HubWebApp.Core
{
    public sealed class HubRoles
    {
        public static HubRoles Instance = new HubRoles();

        public AppRoleName Admin { get; } = new AppRoleName(nameof(Admin));
    }
}
