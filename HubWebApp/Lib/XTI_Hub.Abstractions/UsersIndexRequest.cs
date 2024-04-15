namespace XTI_Hub.Abstractions;

public sealed class UsersIndexRequest
{
    public UsersIndexRequest()
        : this(0, "")
    {
    }

    public UsersIndexRequest(int userID, string returnTo = "")
    {
        UserID = userID;
        ReturnTo = returnTo;
    }

    public int UserID { get; set; }
    public string ReturnTo { get; set; }
}
