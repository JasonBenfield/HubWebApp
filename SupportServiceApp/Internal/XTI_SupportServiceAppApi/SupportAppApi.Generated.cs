using XTI_SupportServiceAppApi.Installations;
using XTI_SupportServiceAppApi.PermanentLog;

// Generated Code
#nullable enable
namespace XTI_SupportServiceAppApi;
public sealed partial class SupportAppApi : AppApiWrapper
{
    internal SupportAppApi(AppApi source, SupportAppApiBuilder builder) : base(source)
    {
        Installations = builder.Installations.Build();
        PermanentLog = builder.PermanentLog.Build();
        Configure();
    }

    partial void Configure();
    public InstallationsGroup Installations { get; }
    public PermanentLogGroup PermanentLog { get; }
}