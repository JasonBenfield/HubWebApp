using XTI_App;

namespace HubWebApp.Apps
{
    public class RoleAccessModel
    {
        public AppRoleModel[] AllowedRoles { get; set; }
        public AppRoleModel[] DeniedRoles { get; set; }
    }
}
