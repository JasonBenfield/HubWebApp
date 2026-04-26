using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed record AppCommandSummaryModel
(
    AppCommandModel Command,
    AppModel App,
    InstallLocationModel Location
)
{
    public AppCommandSummaryModel()
        : this(new(), new(), new())
    {
    }
}
