namespace XTI_Hub.Abstractions;

public sealed class InstallConfigurationIDRequest
{
    public InstallConfigurationIDRequest()
        : this(0)
    {
    }

    public InstallConfigurationIDRequest(int configurationID)
    {
        ConfigurationID = configurationID;
    }

    public int ConfigurationID { get; set; }
}
