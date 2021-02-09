using XTI_App;

namespace HubWebAppApi.Apps
{
    public class RoleAccessModel
    {
        public AppRoleModel[] AllowedRoles { get; set; }
        public AppRoleModel[] DeniedRoles { get; set; }
    }
}
