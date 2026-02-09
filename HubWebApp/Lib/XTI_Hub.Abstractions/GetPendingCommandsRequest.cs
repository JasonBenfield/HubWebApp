namespace XTI_Hub.Abstractions;

public sealed class GetPendingCommandsRequest
{
    public GetPendingCommandsRequest()
        : this([], [])
    {
    }

    public GetPendingCommandsRequest(AppCommandName[] commandNames, string[] machineNames)
    {
        CommandNames = commandNames.Select(cn => cn.Value).ToArray();
        MachineNames = machineNames;
    }

    public string[] CommandNames { get; set; }
    public string[] MachineNames { get; set; }
}
