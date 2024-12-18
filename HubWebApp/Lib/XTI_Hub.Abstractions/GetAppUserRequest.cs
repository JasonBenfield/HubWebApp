namespace XTI_Hub.Abstractions;

public sealed class GetAppUserRequest
{
    public GetAppUserRequest()
        : this("", 0)
    {
    }

    public GetAppUserRequest(string app, int userID)
    {
        App = app;
        UserID = userID;
    }

    public string App { get; set; }
    public int UserID { get; set; }
}