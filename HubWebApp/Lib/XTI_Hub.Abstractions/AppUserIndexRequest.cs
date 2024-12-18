namespace XTI_Hub.Abstractions;

public sealed class AppUserIndexRequest
{
    public AppUserIndexRequest()
        : this("", 0, "")
    {
    }

    public AppUserIndexRequest(string app, int userID, string returnTo)
    {
        App = app;
        UserID = userID;
        ReturnTo = returnTo;
    }

    public string App { get; set; }
    public int UserID { get; set; }
    public string ReturnTo { get; set; }
}