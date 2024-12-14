namespace XTI_Hub.Abstractions;

public sealed class SessionViewRequest
{
    public SessionViewRequest()
        : this(0)
    {
    }

    public SessionViewRequest(int sessionID)
    {
        SessionID = sessionID;
    }

    public int SessionID { get; set; }
}
