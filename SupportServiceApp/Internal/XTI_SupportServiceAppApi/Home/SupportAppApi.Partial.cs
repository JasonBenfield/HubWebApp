using XTI_SupportServiceAppApi.Home;

namespace XTI_SupportServiceAppApi;

partial class SupportAppApi
{
    private HomeGroup? home;

    public HomeGroup Home { get => home ?? throw new ArgumentNullException(nameof(home)); }

    partial void createHomeGroup(IServiceProvider sp)
    {
        home = new HomeGroup
        (
            source.AddGroup(nameof(Home)),
            sp
        );
    }
}