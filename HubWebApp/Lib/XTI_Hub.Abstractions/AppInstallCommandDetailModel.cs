using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed record AppInstallCommandDetailModel
(
    AppCommandModel Command,
    AddInstallCommandRequest InstallRequest,
    AppModel App,
    XtiVersionModel Version,
    InstallLocationModel Location,
    InstallConfigurationModel InstallConfiguration,
    AppCommandStepModel[] Steps,
    InstallationModel[] Installations
)
{
    public AppInstallCommandDetailModel()
        : this(new(), new(), new(), new(), new(), new(), [], [])
    {
    }

    public string GetRelease() =>
        $"v{Version.VersionNumber.Format()}";

    public InstallationModel CurrentInstallationOrDefault() =>
        Installations.FirstOrDefault(inst => inst.IsCurrent) ?? new InstallationModel();

    public InstallationModel VersionInstallationOrDefault() =>
        Installations.FirstOrDefault(inst => !inst.IsCurrent) ?? new InstallationModel();
}