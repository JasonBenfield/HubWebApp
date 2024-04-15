namespace XTI_Hub.Abstractions;

public sealed class AppUserIDRequest
{
    public AppUserIDRequest()
        : this(0)
    {
    }

    public AppUserIDRequest(int userID)
    {
        UserID = userID;
    }

    public int UserID { get; set; }
}
