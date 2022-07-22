using XTI_Forms;
using XTI_HubWebAppApi.Auth;
using XTI_HubWebAppApi.UserList;
using XTI_HubWebAppApi.UserMaintenance;

namespace HubWebApp.Tests;

internal sealed class ChangePasswordTest
{
    [Test]
    public async Task ShouldThrowError_WhenModifierIsBlank()
    {
        var tester = await setup();
        var userToEdit = await addUser(tester, "userToEdit");
        var form = createChangePasswordForm(userToEdit);
        await AccessAssertions.Create(tester).ShouldThrowError_WhenModifierIsBlank(form);
    }

    [Test]
    public async Task ShouldThrowError_WhenAccessIsDenied()
    {
        var tester = await setup();
        var modifier = await tester.GeneralUserGroupModifier();
        var userToEdit = await addUser(tester, "userToEdit");
        var form = createChangePasswordForm(userToEdit);
        await AccessAssertions.Create(tester)
            .ShouldThrowError_WhenAccessIsDenied
            (
                form,
                modifier,
                HubInfo.Roles.Admin,
                HubInfo.Roles.EditUser
            );
    }

    [Test]
    public async Task ShouldThrowError_WhenPasswordIsBlank()
    {
        var tester = await setup();
        var modifier = await tester.GeneralUserGroupModifier();
        var userToEdit = await addUser(tester, "userToEdit");
        var form = createChangePasswordForm(userToEdit);
        form.Password.SetValue("");
        form.Confirm.SetValue("");
        var ex = Assert.ThrowsAsync<ValidationFailedException>(() => tester.Execute(form, modifier));
        Assert.That
        (
            ex?.Errors.Select(err => err.Message),
            Is.EquivalentTo(new[] { FormErrors.MustNotBeNullOrWhitespace }),
            "Should throw error when password is blank"
        );
    }

    [Test]
    public async Task ShouldThrowError_WhenConfirmDoesNotEqualPassword()
    {
        var tester = await setup();
        var modifier = await tester.GeneralUserGroupModifier();
        var userToEdit = await addUser(tester, "userToEdit");
        var form = createChangePasswordForm(userToEdit);
        form.Confirm.SetValue(form.Password.Value() + "Different");
        var ex = Assert.ThrowsAsync<ValidationFailedException>(() => tester.Execute(form, modifier));
        Assert.That
        (
            ex?.Errors.Select(err => err.Message),
            Is.EquivalentTo(new[] { string.Format(FormErrors.FieldsMustBeEqual, "Password") }),
            "Should throw error when password is blank"
        );
    }

    [Test]
    public async Task ShouldChangePassword()
    {
        var tester = await setup();
        var modifier = await tester.GeneralUserGroupModifier();
        var userToEdit = await addUser(tester, "userToEdit");
        var form = createChangePasswordForm(userToEdit);
        await tester.Execute(form, modifier);
        var loginTester = tester.Create(api => api.Auth.VerifyLogin);
        var loginForm = new VerifyLoginForm();
        loginForm.UserName.SetValue("userToEdit");
        loginForm.Password.SetValue("Changed12345");
        Assert.DoesNotThrowAsync(() => loginTester.Execute(loginForm));
    }

    private async Task<AppUser> addUser(IHubActionTester tester, string userName)
    {
        var addUserTester = tester.Create(hubApi => hubApi.Users.AddOrUpdateUser);
        await addUserTester.LoginAsAdmin();
        var modifier = await tester.GeneralUserGroupModifier();
        var userID = await addUserTester.Execute
        (
            new AddUserModel
            {
                UserName = userName,
                Password = "Password12345"
            },
            modifier
        );
        var factory = tester.Services.GetRequiredService<HubFactory>();
        var user = await factory.Users.UserByUserName(new AppUserName(userName));
        return user;
    }

    private static ChangePasswordForm createChangePasswordForm(AppUser userToEdit)
    {
        var form = new ChangePasswordForm();
        form.UserID.SetValue(userToEdit.ToModel().ID);
        form.Password.SetValue("Changed12345");
        form.Confirm.SetValue("Changed12345");
        return form;
    }

    private async Task<HubActionTester<ChangePasswordForm, EmptyActionResult>> setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup();
        return HubActionTester.Create(services, hubApi => hubApi.UserMaintenance.ChangePassword);
    }
}
