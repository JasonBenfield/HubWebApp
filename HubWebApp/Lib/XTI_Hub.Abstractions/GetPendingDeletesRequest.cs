namespace XTI_Hub.Abstractions;

public sealed class GetPendingDeletesRequest
{
    public GetPendingDeletesRequest()
        : this("")
    {
    }

    public GetPendingDeletesRequest(string machineName)
    {
        MachineName = machineName;
    }

    public string MachineName { get; set; }
}
