using XTI_App.Abstractions;
using XTI_Core;

namespace XTI_ServiceAppInstallation;

internal sealed class WinServiceName
{
    public WinServiceName(XtiEnvironment xtiEnv, AppKey appKey)
    {
        var appName = appKey.Name.DisplayText.Replace(" ", "");
        Value = $"Xti_{xtiEnv.EnvironmentName}_{appName}";
    }

    public string Value { get; }
}
