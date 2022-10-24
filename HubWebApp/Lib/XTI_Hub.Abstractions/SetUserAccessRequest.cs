using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed class SetUserAccessRequest
{
    public SetUserAccessRequest()
        :this(AppUserName.Anon, new SetUserAccessRoleRequest[0])
    {
    }

    public SetUserAccessRequest(AppUserName userName, params SetUserAccessRoleRequest[] roleAssignments)
    {
        UserName = userName.DisplayText;
        RoleAssignments = roleAssignments;
    }

    public string UserName { get; set; }
    public SetUserAccessRoleRequest[] RoleAssignments { get; set; }
}
