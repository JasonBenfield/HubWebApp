using XTI_App.Abstractions;

namespace XTI_Hub
{
    public sealed class HubRoles
    {
        internal static HubRoles Instance = new HubRoles();

        public AppRoleName Admin { get; } = AppRoleName.Admin;
        public AppRoleName Installer { get; } = new AppRoleName(nameof(Installer));
        public AppRoleName Publisher { get; } = new AppRoleName(nameof(Publisher));
        public AppRoleName ViewApp { get; } = new AppRoleName(nameof(ViewApp));
        public AppRoleName AddUser { get; } = new AppRoleName(nameof(AddUser));
        public AppRoleName EditUser { get; } = new AppRoleName(nameof(EditUser));
        public AppRoleName ViewUser { get; } = new AppRoleName(nameof(ViewUser));
    }
}
