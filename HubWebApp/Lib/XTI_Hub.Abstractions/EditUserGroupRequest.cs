namespace XTI_Hub.Abstractions;

public sealed class EditUserGroupRequest
{
    public EditUserGroupRequest()
        : this(0, 0)
    {
    }

    public EditUserGroupRequest(int userID, int userGroupID)
    {
        UserID = userID;
        UserGroupID = userGroupID;
    }

    public int UserID { get; set; }
    public int UserGroupID { get; set; }
}
