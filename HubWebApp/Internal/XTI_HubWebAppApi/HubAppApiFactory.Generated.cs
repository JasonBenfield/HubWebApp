// Generated Code
namespace XTI_HubWebAppApi;
public sealed class HubAppApiFactory : AppApiFactory
{
    private readonly IServiceProvider sp;
    public HubAppApiFactory(IServiceProvider sp)
    {
        this.sp = sp;
    }

    public new HubAppApi Create(IAppApiUser user) => (HubAppApi)base.Create(user);
    public new HubAppApi CreateForSuperUser() => (HubAppApi)base.CreateForSuperUser();
    protected override IAppApi _Create(IAppApiUser user) => new HubAppApiBuilder(sp, user).Build();
}