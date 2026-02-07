namespace XTI_Hub.Abstractions;

public sealed class BeginInstallationRequest
{
    public BeginInstallationRequest()
        : this(0, false)
    {
    }

    public BeginInstallationRequest(int commandID, bool isCurrent)
    {
        CommandID = commandID;
        IsCurrent = isCurrent;
    }

    public int CommandID { get; set; }
    public bool IsCurrent { get; set; }
}
