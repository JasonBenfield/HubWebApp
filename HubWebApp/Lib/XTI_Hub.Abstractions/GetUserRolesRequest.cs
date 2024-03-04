namespace XTI_Hub.Abstractions;

public sealed class GetUserRolesRequest
{
    public GetUserRolesRequest()
        : this(0, 0)
    {
    }

    public GetUserRolesRequest(int userID, int modifierID)
    {
        UserID = userID;
        ModifierID = modifierID;
    }

    public int UserID { get; set; }
    public int ModifierID { get; set; }
}
