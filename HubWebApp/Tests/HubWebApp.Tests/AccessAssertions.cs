namespace HubWebApp.Tests;

internal static class AccessAssertions
{
    public static AppModifierAssertions<TModel, TResult> Create<TModel, TResult>(HubActionTester<TModel, TResult> tester)
    {
        return new AppModifierAssertions<TModel, TResult>(tester);
    }
}

internal sealed class AppModifierAssertions<TModel, TResult>
{
    private readonly HubActionTester<TModel, TResult> tester;

    public AppModifierAssertions(HubActionTester<TModel, TResult> tester)
    {
        this.tester = tester;
    }

    public void ShouldThrowError_WhenModifierIsBlank(TModel model)
    {
        tester.LoginAsAdmin();
        var ex = Assert.ThrowsAsync<Exception>
        (
            () => tester.Execute(model),
            "Should throw error when modifier is blank"
        );
        Assert.That(ex?.Message, Is.EqualTo(AppErrors.ModifierIsRequired));
    }

    public void ShouldAllowAnonymous(TModel model, FakeModifier modifier)
    {
        Assert.DoesNotThrowAsync
        (
            () => tester.Execute(model, modifier.ModKey()),
            "Should allow access to anonymous user"
        );
    }

    public void ShouldThrowError_WhenAccessIsDenied(TModel model, FakeModifier modifier, params AppRoleName[] allowedRoles)
    {
        var loggedInUser = tester.Login();
        var appContext = tester.Services.GetRequiredService<FakeAppContext>();
        foreach (var roleName in allowedRoles)
        {
            clearUserRoles(loggedInUser, modifier);
            loggedInUser.AddRole(roleName);
            Assert.DoesNotThrowAsync
            (
                () => tester.Execute(model, modifier.ModKey()),
                $"Should have access with role '{roleName.DisplayText}'"
            );
        }
        var roles = tester.FakeHubApp()
            .Roles()
            .Select(r => r.Name())
            .Where(r => !r.Equals(AppRoleName.DenyAccess));
        var deniedRoles = roles.Except(allowedRoles).ToArray();
        foreach (var roleName in deniedRoles)
        {
            clearUserRoles(loggedInUser, modifier);
            loggedInUser.AddRole(roleName);
            Assert.ThrowsAsync<AccessDeniedException>
            (
                () => tester.Execute(model, modifier.ModKey()),
                $"Should not have access with role '{roleName.DisplayText}'"
            );
        }
        clearUserRoles(loggedInUser, modifier);
        loggedInUser.AddRole(AppRoleName.DenyAccess);
        Assert.ThrowsAsync<AccessDeniedException>
        (
            () => tester.Execute(model, modifier.ModKey()),
            $"Should not have access with role '{AppRoleName.DenyAccess.DisplayText}'"
        );
    }

    private static void clearUserRoles(FakeAppUser loggedInUser, IModifier modifier)
    {
        var existingRoles = loggedInUser.Roles(modifier);
        foreach (var existingRole in existingRoles)
        {
            loggedInUser.RemoveRole(existingRole);
        }
    }
}