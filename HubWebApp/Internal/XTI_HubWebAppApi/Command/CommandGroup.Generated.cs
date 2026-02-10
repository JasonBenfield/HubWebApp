using XTI_HubWebAppApiActions.Command;

// Generated Code
#nullable enable
namespace XTI_HubWebAppApi.Command;
public sealed partial class CommandGroup : AppApiGroupWrapper
{
    internal CommandGroup(AppApiGroup source, CommandGroupBuilder builder) : base(source)
    {
        GetCommand = builder.GetCommand.Build();
        GetDeleteCommandDetail = builder.GetDeleteCommandDetail.Build();
        GetInstallationCommandDetail = builder.GetInstallationCommandDetail.Build();
        Index = builder.Index.Build();
        Configure();
    }

    partial void Configure();
    public AppApiAction<AppCommandIDRequest, AppCommandModel> GetCommand { get; }
    public AppApiAction<AppCommandIDRequest, AppDeleteCommandDetailModel> GetDeleteCommandDetail { get; }
    public AppApiAction<AppCommandIDRequest, AppInstallCommandDetailModel> GetInstallationCommandDetail { get; }
    public AppApiAction<AppCommandIDRequest, WebViewResult> Index { get; }
}