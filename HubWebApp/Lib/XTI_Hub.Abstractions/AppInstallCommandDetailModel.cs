using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed record AppInstallCommandDetailModel
(
    AppCommandModel Command,
    AppModel App,
    XtiVersionModel Version,
    InstallConfigurationModel InstallConfiguration,
    AppCommandStepModel[] Steps,
    InstallationModel[] Installations
)
{
    public AppInstallCommandDetailModel()
        : this(new(), new(), new(), new(), [], [])
    {
    }

    public string GetRelease() =>
        $"v{Version.VersionNumber.Format()}";

}