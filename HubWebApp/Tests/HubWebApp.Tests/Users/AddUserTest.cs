using XTI_Forms;
using XTI_HubWebAppApiActions;

namespace HubWebApp.Tests;

internal sealed class AddUserTest
{
    private static int userSuffix;

    [Test]
    public async Task ShouldThrowError_WhenModifierIsBlank()
    {
        var tester = await Setup();
        await AccessAssertions.Create(tester)
            .ShouldThrowError_WhenModifierIsBlank(CreateForm());
    }

    [Test]
    public async Task ShouldThrowError_WhenAccessIsDenied()
    {
        var tester = await Setup();
        var modifier = await tester.GeneralUserGroupModifier();
        await AccessAssertions.Create(tester)
            .ShouldThrowError_WhenAccessIsDenied
            (
                () =>
                {
                    var form = CreateForm(userSuffix);
                    userSuffix++;
                    return Task.FromResult(form);
                },
                modifier,
                HubInfo.Roles.Admin,
                HubInfo.Roles.AddUser
            );
    }

    [Test]
    public async Task ShouldThrowError_WhenUserNameIsBlank()
    {
        var tester = await Setup();
        var modifier = await tester.GeneralUserGroupModifier();
        var form = CreateForm();
        form.UserName.SetValue("");
        var ex = Assert.ThrowsAsync<ValidationFailedException>(() => tester.Execute(form, modifier));
        Assert.That
        (
            ex?.Errors.Select(err => err.Message),
            Is.EquivalentTo(new[] { FormErrors.MustNotBeNullOrWhitespace }),
            "Should throw error when user name is blank"
        );
    }

    [Test]
    public async Task ShouldThrowError_WhenPasswordIsBlank()
    {
        var tester = await Setup();
        var modifier = await tester.GeneralUserGroupModifier();
        var form = CreateForm();
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
        var tester = await Setup();
        var modifier = await tester.GeneralUserGroupModifier();
        var form = CreateForm();
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
    public async Task ShouldSetPassword()
    {
        var tester = await Setup();
        var modifier = await tester.GeneralUserGroupModifier();
        var form = CreateForm();
        await tester.Execute(form, modifier);
        var loginTester = tester.Create(api => api.Auth.VerifyLogin);
        var loginForm = new VerifyLoginForm();
        loginForm.UserName.SetValue("new.user");
        loginForm.Password.SetValue("Password1234");
        Assert.DoesNotThrowAsync(() => loginTester.Execute(loginForm));
    }

    [Test]
    public async Task ShouldAddUser()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
        var modifier = await tester.GeneralUserGroupModifier();
        var addedUser = await tester.Execute(CreateForm(), modifier);
        var getUserTester = tester.Create(api => api.UserInquiry.GetUser);
        var user = await getUserTester.Execute(new AppUserIDRequest(addedUser.ID), modifier);
        Assert.That(user.UserName, Is.EqualTo("new.user"), "Should add user");
        Assert.That(user.Name, Is.EqualTo("New User"), "Should add user");
        Assert.That(user.Email, Is.EqualTo("new.user@example.com"), "Should add user");
    }

    [Test]
    public async Task ShouldThrowError_WhenUserAlreadyExists()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
        var modifier = await tester.GeneralUserGroupModifier();
        await tester.Execute(CreateForm(), modifier);
        var ex = Assert.ThrowsAsync<AppException>(() => tester.Execute(CreateForm(), modifier));
        Assert.That
        (
            ex?.Message,
            Is.EqualTo(string.Format(AppErrors.UserAlreadyExists, "new.user")),
            "Should throw error when user already exists."
        );
    }

    private AddUserForm CreateForm(int userSuffix = 0)
    {
        var form = new AddUserForm();
        form.UserName.SetValue("new.user" + (userSuffix > 0 ? userSuffix.ToString() : ""));
        form.Password.SetValue("Password1234");
        form.Confirm.SetValue("Password1234");
        form.PersonName.SetValue("New User");
        form.Email.SetValue("new.user@example.com");
        return form;
    }

    private async Task<HubActionTester<AddUserForm, AppUserModel>> Setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup();
        return HubActionTester.Create(services, hubApi => hubApi.Users.AddUser);
    }

}
