using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed class SystemSetUserAccessRequest
{
    public SystemSetUserAccessRequest()
        : this(0, AppUserName.Anon, new SystemSetUserAccessRoleRequest[0])
    {
    }

    public SystemSetUserAccessRequest(int installationID, AppUserName userName, params SystemSetUserAccessRoleRequest[] roleAssignments)
    {
        InstallationID = installationID;
        UserName = userName.DisplayText;
        RoleAssignments = roleAssignments;
    }

    public int InstallationID { get; set; }
    public string UserName { get; set; }
    public SystemSetUserAccessRoleRequest[] RoleAssignments { get; set; }
}
