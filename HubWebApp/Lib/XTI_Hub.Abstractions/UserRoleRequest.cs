namespace XTI_Hub.Abstractions;

public sealed class UserRoleRequest
{
    public UserRoleRequest()
        : this(0, 0, 0)
    {
    }

    public UserRoleRequest(int userID, int modifierID, int roleID)
    {
        UserID = userID;
        ModifierID = modifierID;
        RoleID = roleID;
    }

    public int UserID { get; set; }
    public int ModifierID { get; set; }
    public int RoleID { get; set; }
}