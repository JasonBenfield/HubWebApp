using XTI_App;

namespace XTI_HubAppApi.Users
{
    public sealed class UserRoleAccessModel
    {
        public UserRoleAccessModel(AppRoleModel[] unassignedRoles, AppRoleModel[] assignedRoles)
        {
            UnassignedRoles = unassignedRoles;
            AssignedRoles = assignedRoles;
        }

        public AppRoleModel[] UnassignedRoles { get; }
        public AppRoleModel[] AssignedRoles { get; }
    }
}
