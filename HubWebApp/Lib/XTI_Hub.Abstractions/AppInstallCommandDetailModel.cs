using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed record AppInstallCommandDetailModel
(
    AppCommandModel Command,
    AppModel App,
    InstallLocationModel Location,
    AppCommandStepModel[] Steps,
    AddInstallCommandRequest InstallRequest,
    XtiVersionModel Version,
    InstallConfigurationModel InstallConfiguration,
    InstallationModel[] Installations
)
{
    public AppInstallCommandDetailModel()
        : this(new(), new(), new(), [], new(), new(), new(), [])
    {
    }

    public string GetRelease() =>
        $"v{Version.VersionNumber.Format()}";

    public InstallationModel CurrentInstallationOrDefault() =>
        Installations.FirstOrDefault(inst => inst.IsCurrent) ?? new InstallationModel();

    public InstallationModel VersionInstallationOrDefault() =>
        Installations.FirstOrDefault(inst => !inst.IsCurrent) ?? new InstallationModel();
}