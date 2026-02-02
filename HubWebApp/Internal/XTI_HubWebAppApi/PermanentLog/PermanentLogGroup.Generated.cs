using XTI_HubWebAppApiActions.PermanentLog;

// Generated Code
#nullable enable
namespace XTI_HubWebAppApi.PermanentLog;
public sealed partial class PermanentLogGroup : AppApiGroupWrapper
{
    internal PermanentLogGroup(AppApiGroup source, PermanentLogGroupBuilder builder) : base(source)
    {
        LogSessionDetails = builder.LogSessionDetails.Build();
        Configure();
    }

    partial void Configure();
    public AppApiAction<LogSessionDetailsRequest, EmptyActionResult> LogSessionDetails { get; }
}