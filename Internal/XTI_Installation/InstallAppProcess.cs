using XTI_App.Abstractions;
using XTI_Hub.Abstractions;

namespace XTI_Installation;

public interface InstallAppProcess
{
    Task Run(string publishedAppPath, RequestedInstallation requestedInstallation, CancellationToken ct);

}

