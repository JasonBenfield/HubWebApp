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

    public async Task ShouldThrowError_WhenModifierIsBlank(TModel model)
    {
        await tester.LoginAsAdmin();
        var ex = Assert.ThrowsAsync<Exception>
        (
            () => tester.Execute(model),
            "Should throw error when modifier is blank"
        );
        Assert.That(ex?.Message, Is.EqualTo(AppErrors.ModifierIsRequired));
    }

    public void ShouldAllowAnonymous(TModel model) => ShouldAllowAnonymous(model, ModifierKey.Default);

    public void ShouldAllowAnonymous(TModel model, ModifierKey modKey)
    {
        Assert.DoesNotThrowAsync
        (
            () => tester.Execute(model, modKey),
            "Should allow access to anonymous user"
        );
    }

    public async Task ShouldThrowError_WhenAccessIsDenied(TModel model, params AppRoleName[] allowedRoles)
    {
        var modifier = await tester.DefaultModifier();
        await ShouldThrowError_WhenAccessIsDenied(model, modifier, allowedRoles);
    }

    public Task ShouldThrowError_WhenAccessIsDenied(TModel model, Modifier modifier, params AppRoleName[] allowedRoles) =>
        ShouldThrowError_WhenAccessIsDenied(model, new AppRoleName[0], modifier, allowedRoles);

    public async Task ShouldThrowError_WhenAccessIsDenied(TModel model, AppRoleName[] rolesToKeep, Modifier modifier, params AppRoleName[] allowedRoles)
    {
        var loggedInUser = await tester.Login();
        var factory = tester.Services.GetRequiredService<HubFactory>();
        var app = await factory.Apps.AppOrUnknown(HubInfo.AppKey);
        foreach (var roleName in allowedRoles)
        {
            await SetUserRoles(loggedInUser, rolesToKeep, modifier, roleName);
            Assert.DoesNotThrowAsync
            (
                () => tester.Execute(model, modifier.ToModel().ModKey),
                $"Should have access with role '{roleName.DisplayText}'"
            );
        }
        var hubApp = await tester.HubApp();
        var hubAppRoles = await hubApp.Roles();
        var roles = hubAppRoles
            .Select(r => r.ToModel().Name)
            .Where(r => !r.Equals(AppRoleName.DenyAccess));
        var deniedRoles = roles.Except(allowedRoles).ToArray();
        foreach (var roleName in deniedRoles)
        {
            await SetUserRoles(loggedInUser, new AppRoleName[0], modifier, roleName);
            Assert.ThrowsAsync<AccessDeniedException>
            (
                () => tester.Execute(model, modifier.ToModel().ModKey),
                $"Should not have access with role '{roleName.DisplayText}'"
            );
        }
        await SetUserRoles(loggedInUser, new AppRoleName[0], modifier, AppRoleName.DenyAccess);
        Assert.ThrowsAsync<AccessDeniedException>
        (
            () => tester.Execute(model, modifier.ToModel().ModKey),
            $"Should not have access with role '{AppRoleName.DenyAccess.DisplayText}'"
        );
    }

    private async Task SetUserRoles(AppUser loggedInUser, AppRoleName[] rolesToKeep, Modifier modifier, params AppRoleName[] roleNames)
    {
        var factory = tester.Services.GetRequiredService<HubFactory>();
        var app = await factory.Apps.AppOrUnknown(HubInfo.AppKey);
        var userRoles = await loggedInUser.Modifier(modifier).AssignedRoles();
        foreach (var userRole in userRoles)
        {
            await loggedInUser.Modifier(modifier).UnassignRole(userRole);
        }
        foreach (var roleName in roleNames)
        {
            var role = await app.Role(roleName);
            await loggedInUser.Modifier(modifier).AssignRole(role);
        }
        if (rolesToKeep.Any())
        {
            var defaultModifier = await app.DefaultModifier();
            foreach(var roleName in rolesToKeep)
            {
                var role = await app.Role(roleName);
                await loggedInUser.Modifier(defaultModifier).AssignRole(role);
            }
        }
    }
}