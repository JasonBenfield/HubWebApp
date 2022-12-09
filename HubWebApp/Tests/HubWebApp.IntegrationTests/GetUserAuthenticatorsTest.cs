using XTI_Hub.Abstractions;

namespace HubWebApp.IntegrationTests;

internal sealed class GetUserAuthenticatorsTest
{
    [Test]
    public async Task ShouldGetUserAuthenticators()
    {
        var tester = await Setup();
        var userAuthenticators = await tester.Execute(1013, new ModifierKey("General"));
        userAuthenticators.WriteToConsole();
    }

    private async Task<HubActionTester<int, UserAuthenticatorModel[]>> Setup(string envName = "Development")
    {
        var host = new HubTestHost();
        var sp = await host.Setup(envName);
        return  HubActionTester.Create(sp, hubApi => hubApi.UserInquiry.GetUserAuthenticators);
    }
}
