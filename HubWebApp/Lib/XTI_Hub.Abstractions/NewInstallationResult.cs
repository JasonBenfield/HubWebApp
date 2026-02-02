using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed record NewInstallationResult
(
    InstallationModel CurrentInstallation,
    InstallationModel VersionInstallation,
    InstallLocationModel Location,
    AppModel App,
    XtiVersionModel Version
)
{
    public NewInstallationResult()
        : this(new(), new(), new(), new(), new())
    {
    }

    public string GetRelease() =>
        $"v{Version.VersionNumber.Format()}";

    public AppVersionInstallationModel GetCurrentInstallation() =>
        new AppVersionInstallationModel
        (
            App: App,
            Version: Version,
            Installation: CurrentInstallation
        );

    public AppVersionInstallationModel GetVersionInstallation() =>
        new AppVersionInstallationModel
        (
            App: App,
            Version: Version,
            Installation: CurrentInstallation
        );
}