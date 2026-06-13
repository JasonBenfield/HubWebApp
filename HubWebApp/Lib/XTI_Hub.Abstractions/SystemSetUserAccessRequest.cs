using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed class SystemSetUserAccessRequest
{
    public SystemSetUserAccessRequest()
        : this(AppUserName.Anon, [])
    {
    }

    public SystemSetUserAccessRequest(AppUserName userName, params SystemSetUserAccessRoleRequest[] roleAssignments)
    {
        UserName = userName.DisplayText;
        RoleAssignments = roleAssignments;
    }

    public string UserName { get; set; }
    public SystemSetUserAccessRoleRequest[] RoleAssignments { get; set; }
}
