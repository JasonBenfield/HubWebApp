using XTI_App.Abstractions;
using XTI_Hub.Abstractions;

namespace XTI_Installation;

public interface InstallAppProcess
{
    Task Run(string publishedAppDir, InstallConfigurationModel adminInstOptions, AppVersionKey installVersionKey, CancellationToken ct);
}
