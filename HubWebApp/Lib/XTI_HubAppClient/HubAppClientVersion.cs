// Generated Code
namespace XTI_HubAppClient;
public sealed class HubAppClientVersion
{
    public static HubAppClientVersion Version(string value) => new HubAppClientVersion(value);
    public HubAppClientVersion(IHostEnvironment hostEnv) : this(getValue(hostEnv))
    {
    }

    private static string getValue(IHostEnvironment hostEnv)
    {
        string value;
        if (hostEnv.IsProduction())
        {
            value = "V1406";
        }
        else
        {
            value = "Current";
        }

        return value;
    }

    private HubAppClientVersion(string value)
    {
        Value = value;
    }

    public string Value { get; }
}