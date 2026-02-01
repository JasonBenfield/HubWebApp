namespace HubWebApp.IntegrationTests;

internal sealed class GetLoggedInAppsTest
{
    [Test]
    public async Task ShouldGetLoggedInApps()
    {
        var sp = await Setup();
        var hubFactory = sp.GetRequiredService<EfHubDB>();
        var user = await hubFactory.Users.UserOrAnon(new AppUserName("test.user"), ct: default);
        var loggedInApps = await user.GetLoggedInApps(ct: default);
        loggedInApps.WriteToConsole();
    }

    private Task<IServiceProvider> Setup(string envName = "Development")
    {
        var host = new HubTestHost();
        return host.Setup(envName);
    }
}
