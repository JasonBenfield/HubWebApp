namespace XTI_Hub.Abstractions;

public sealed class GetInstallationActivitiesRequest
{
    public GetInstallationActivitiesRequest()
        : this([])
    {
    }

    public GetInstallationActivitiesRequest(params string[] machineNames)
    {
        MachineNames = machineNames;
    }

    public string[] MachineNames { get; set; }
}
