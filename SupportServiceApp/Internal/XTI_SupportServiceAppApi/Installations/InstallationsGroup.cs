using XTI_Core;
using XTI_Schedule;

namespace XTI_SupportServiceAppApi.Installations;

public sealed class InstallationsGroup : AppApiGroupWrapper
{
    public InstallationsGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        Delete = source.AddAction<EmptyRequest, EmptyActionResult>()
            .Named(nameof(Delete))
            .WithExecution<DeleteAction>()
            .RunContinuously()
                .Interval(TimeSpan.FromHours(1))
                .AddSchedule
                (
                    Schedule.EveryDay().At(TimeRange.AllDay())
                )
            .Build();
    }

    public AppApiAction<EmptyRequest, EmptyActionResult> Delete { get; }
}