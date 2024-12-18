using XTI_SupportServiceAppApi.Installations;
using XTI_SupportServiceAppApi.PermanentLog;

// Generated Code
#nullable enable
namespace XTI_SupportServiceAppApi;
public sealed partial class SupportAppApiBuilder
{
    private readonly AppApi source;
    private readonly IServiceProvider sp;
    public SupportAppApiBuilder(IServiceProvider sp, IAppApiUser user)
    {
        source = new AppApi(sp, SupportAppKey.Value, user);
        this.sp = sp;
        Installations = new InstallationsGroupBuilder(source.AddGroup("Installations"));
        PermanentLog = new PermanentLogGroupBuilder(source.AddGroup("PermanentLog"));
        Configure();
    }

    partial void Configure();
    public InstallationsGroupBuilder Installations { get; }
    public PermanentLogGroupBuilder PermanentLog { get; }

    public SupportAppApi Build() => new SupportAppApi(source, this);
}