using XTI_HubWebAppApi.Home;

namespace XTI_HubWebAppApi;

partial class HubAppApi
{
    private HomeGroup? _Home;

    public HomeGroup Home { get => _Home ?? throw new ArgumentNullException(nameof(_Home)); }

    partial void createHomeGroup(IServiceProvider sp)
    {
        _Home = new HomeGroup
        (
            source.AddGroup(nameof(Home)),
            sp
        );
    }
}
