using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed record AppDeleteCommandDetailModel
(
    AppCommandModel Command, 
    AppModel App, 
    XtiVersionModel Version,
    InstallationModel Installation,
    AppCommandStepModel[] Steps
)
{
    public AppDeleteCommandDetailModel()
        : this(new(), new(), new(), new(), [])
    {
    }
}
