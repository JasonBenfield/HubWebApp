namespace HubWebApp.Tests;

internal sealed class GetCurrentUserTest
{
    [Test]
    public async Task ShouldGetCurrentUser()
    {
        var tester = await Setup();
        var loggedIn = await tester.Login();
        var user = await tester.Execute(new EmptyRequest());
        var loggedInModel = loggedIn.ToModel();
        Assert.That(user.UserName, Is.EqualTo(loggedInModel.UserName), "Should get current user");
    }

    private async Task<HubActionTester<EmptyRequest, AppUserModel>> Setup()
    {
        var host = new HubTestHost();
        var sp = await host.Setup();
        return HubActionTester.Create(sp, hubApi => hubApi.CurrentUser.GetUser);
    }
}
