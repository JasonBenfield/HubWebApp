namespace XTI_Hub.Abstractions;

public sealed class BeginAppCommandStepRequest
{
    public BeginAppCommandStepRequest()
        : this(0, "")
    {
    }

    public BeginAppCommandStepRequest(int commandID, string activity)
    {
        CommandID = commandID;
        Activity = activity;
    }

    public int CommandID { get; set; }
    public string Activity { get; set; }
}
