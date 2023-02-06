namespace XTI_Hub.Abstractions;

public sealed record InstallLocationModel(int ID, string QualifiedMachineName)
{
    public InstallLocationModel()
        : this(0, "")
    {
    }
}
