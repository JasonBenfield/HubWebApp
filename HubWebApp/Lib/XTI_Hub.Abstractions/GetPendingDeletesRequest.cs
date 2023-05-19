namespace XTI_Hub.Abstractions;

public sealed class GetPendingDeletesRequest
{
    public GetPendingDeletesRequest()
        : this(new string[0])
    {
    }

    public GetPendingDeletesRequest(params string[] machineNames)
    {
        MachineNames = machineNames;
    }

    public string[] MachineNames { get; set; }
}
