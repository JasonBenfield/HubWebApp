namespace XTI_HubAppApi;

public sealed class HubAppApiFactory : AppApiFactory
{
    private readonly IServiceProvider sp;

    public HubAppApiFactory(IServiceProvider sp)
    {
        this.sp = sp;
    }

    public new HubAppApi Create(IAppApiUser user) => (HubAppApi)base.Create(user);
    public new HubAppApi CreateForSuperUser() => (HubAppApi)base.CreateForSuperUser();

    protected override IAppApi _Create(IAppApiUser user) => new HubAppApi(user, sp);
}