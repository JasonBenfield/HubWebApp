using XTI_HubWebAppApi.Periodic;

namespace XTI_HubWebAppApi;

partial class HubAppApi
{
    private PeriodicGroup? _Periodic;

    public PeriodicGroup Periodic { get => _Periodic ?? throw new ArgumentNullException(nameof(_Periodic)); }

    partial void createPeriodicGroup(IServiceProvider sp)
    {
        _Periodic = new PeriodicGroup
        (
            source.AddGroup(nameof(Periodic)),
            sp
        );
    }
}