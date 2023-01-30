using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed record InstallationDetailModel
(
    InstallLocationModel InstallLocation,
    InstallationModel Installation,
    XtiVersionModel Version,
    AppModel App
)
{
    public InstallationDetailModel()
        :this
        (
            new InstallLocationModel(),
            new InstallationModel(),
            new XtiVersionModel(),
            new AppModel()
        )
    {
    }
}
