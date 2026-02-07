namespace XTI_Hub.Abstractions;

public sealed class AppCommandIDRequest
{
    public AppCommandIDRequest()
        : this(0)
    {
    }

    public AppCommandIDRequest(int commandID)
    {
        CommandID = commandID;
    }

    public int CommandID { get; set; }
}
