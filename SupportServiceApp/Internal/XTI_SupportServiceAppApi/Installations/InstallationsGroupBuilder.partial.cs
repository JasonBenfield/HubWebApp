using XTI_Core;
using XTI_Schedule;

namespace XTI_SupportServiceAppApi.Installations;

partial class InstallationsGroupBuilder
{
    partial void Configure()
    {
        Delete .RunContinuously()
            .Interval(TimeSpan.FromHours(1))
            .AddSchedule
            (
                Schedule.EveryDay().At(TimeRange.AllDay())
            );
    }
}
