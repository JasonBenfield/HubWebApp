using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed record AppDeleteCommandDetailModel
(
    AppCommandModel Command, 
    AppModel App,
    InstallLocationModel Location,
    AppCommandStepModel[] Steps, 
    XtiVersionModel Version,
    InstallationModel Installation
)
{
    public AppDeleteCommandDetailModel()
        : this(new(), new(), new(), [], new(), new())
    {
    }
}
