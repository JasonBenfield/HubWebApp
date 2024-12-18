namespace XTI_Hub.Abstractions;

public sealed class GetUserRequest
{
    public GetUserRequest()
        : this(0)
    {
    }
    public GetUserRequest(int userID)
    {
        UserID = userID;
    }

    public int UserID { get; set; }
}
