// Generated Code
namespace XTI_AuthenticatorAppClient;
public sealed class AuthenticatorAppClientVersion
{
    public static AuthenticatorAppClientVersion Version(string value) => new AuthenticatorAppClientVersion(value);
    public AuthenticatorAppClientVersion(IHostEnvironment hostEnv) : this(getValue(hostEnv))
    {
    }

    private static string getValue(IHostEnvironment hostEnv)
    {
        string value;
        if (hostEnv.IsProduction())
        {
            value = "V1389";
        }
        else
        {
            value = "Current";
        }

        return value;
    }

    private AuthenticatorAppClientVersion(string value)
    {
        Value = value;
    }

    public string Value { get; }
}