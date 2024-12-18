namespace XTI_Hub.Abstractions;

public sealed class AppRequestRequest
{
    public AppRequestRequest()
        : this(0)
    {
    }

    public AppRequestRequest(int requestID)
    {
        RequestID = requestID;
    }

    public int RequestID { get; set; }
}
