namespace XTI_Hub.Abstractions;

public sealed class UserRoleIDRequest
{
    public UserRoleIDRequest()
        : this(0)
    {
    }

    public UserRoleIDRequest(int userRoleID)
    {
        UserRoleID = userRoleID;
    }

    public int UserRoleID { get; set; }
}
