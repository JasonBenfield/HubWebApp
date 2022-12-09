using XTI_Hub.Abstractions;

namespace HubWebApp.IntegrationTests;

internal sealed class GetLoggedInAppsTest
{
    [Test]
    public async Task ShouldGetLoggedInApps()
    {
        var tester = await Setup();
        var hubFactory = tester.Services.GetRequiredService<HubFactory>();
        var user = await hubFactory.Users.UserOrAnon(new AppUserName("test.user"));
        var loggedInApps = await user.GetLoggedInApps();
        loggedInApps.WriteToConsole();
    }

    private async Task<HubActionTester<int, UserAuthenticatorModel[]>> Setup(string envName = "Development")
    {
        var host = new HubTestHost();
        var sp = await host.Setup(envName);
        return  HubActionTester.Create(sp, hubApi => hubApi.UserInquiry.GetUserAuthenticators);
    }
}
