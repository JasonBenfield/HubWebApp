namespace HubWebApp.Tests;

internal sealed class ChangeCurrentUserPasswordTest
{
    [Test]
    public async Task ShouldNotAllowAnonymous()
    {
        var tester = await Setup();
        var form = CreateForm("Changed12345");
        AccessAssertions.Create(tester).ShouldNotAllowAnonymous(form);
    }

    [Test]
    public async Task ShouldChangePassword()
    {
        var tester = await Setup();
        var loggedInUser = await tester.Login();
        const string password = "Changed12345";
        ChangeCurrentUserPasswordForm form = CreateForm(password);
        await tester.Execute(form);
        var loginTester = tester.Create(api => api.Auth.VerifyLogin);
        var loginForm = new VerifyLoginForm();
        loginForm.UserName.SetValue(loggedInUser.ToModel().UserName.Value);
        loginForm.Password.SetValue(password);
        Assert.DoesNotThrowAsync(() => loginTester.Execute(loginForm));
    }

    private static ChangeCurrentUserPasswordForm CreateForm(string password)
    {
        var form = new ChangeCurrentUserPasswordForm();
        form.Password.SetValue(password);
        form.Confirm.SetValue(password);
        return form;
    }

    private async Task<HubActionTester<ChangeCurrentUserPasswordForm, EmptyActionResult>> Setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup();
        return HubActionTester.Create(services, hubApi => hubApi.CurrentUser.ChangePassword);
    }
}
