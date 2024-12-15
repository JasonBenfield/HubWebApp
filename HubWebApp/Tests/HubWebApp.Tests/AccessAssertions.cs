using XTI_HubWebAppApiActions;

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

    public void ShouldNotAllowAnonymous(TModel model) => ShouldNotAllowAnonymous(model, ModifierKey.Default);

    public void ShouldNotAllowAnonymous(TModel model, ModifierKey modKey)
    {
        tester.Logout();
        Assert.ThrowsAsync<AccessDeniedException>
        (
            () => tester.Execute(model, modKey),
            "Should not allow access to anonymous user"
        );
    }

    public void ShouldAllowAnonymous(TModel model) => ShouldAllowAnonymous(model, ModifierKey.Default);

    public void ShouldAllowAnonymous(TModel model, ModifierKey modKey)
    {
        tester.Logout();
        Assert.DoesNotThrowAsync
        (
            () => tester.Execute(model, modKey),
            "Should allow access to anonymous user"
        );
    }

    public Task ShouldThrowError_WhenAccessIsDenied(TModel model, params AppRoleName[] allowedRoles) =>
        ShouldThrowError_WhenAccessIsDenied(() => Task.FromResult(model), allowedRoles);

    public async Task ShouldThrowError_WhenAccessIsDenied(Func<Task<TModel>> createModel, params AppRoleName[] allowedRoles)
    {
        var modifier = await tester.DefaultModifier();
        await ShouldThrowError_WhenAccessIsDenied(createModel, modifier, allowedRoles);
    }

    public Task ShouldThrowError_WhenAccessIsDenied(TModel model, ModifierModel modifier, params AppRoleName[] allowedRoles) =>
        ShouldThrowError_WhenAccessIsDenied(() => Task.FromResult(model), modifier, allowedRoles);

    public Task ShouldThrowError_WhenAccessIsDenied(TModel model, AppRoleName[] rolesToKeep, ModifierModel modifier, params AppRoleName[] allowedRoles) =>
        ShouldThrowError_WhenAccessIsDenied(() => Task.FromResult(model), rolesToKeep, modifier, allowedRoles);

    public Task ShouldThrowError_WhenAccessIsDenied(Func<Task<TModel>> createModel, ModifierModel modifier, params AppRoleName[] allowedRoles) =>
        ShouldThrowError_WhenAccessIsDenied(createModel, new AppRoleName[0], modifier, allowedRoles);

    public async Task ShouldThrowError_WhenAccessIsDenied(Func<Task<TModel>> createModel, AppRoleName[] rolesToKeep, ModifierModel modifier, params AppRoleName[] allowedRoles)
    {
        var modKey = modifier.ModKey;
        var factory = tester.Services.GetRequiredService<HubFactory>();
        var app = await factory.Apps.AppOrUnknown(HubInfo.AppKey);
        foreach (var roleName in allowedRoles)
        {
            var model = await createModel();
            var loggedInUser = await tester.Login();
            await SetUserRoles(loggedInUser, rolesToKeep, modifier, roleName);
            Assert.DoesNotThrowAsync
            (
                () => tester.Execute(model, modKey),
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
            var model = await createModel();
            var loggedInUser = await tester.Login();
            await SetUserRoles(loggedInUser, new AppRoleName[0], modifier, roleName);
            Assert.ThrowsAsync<AccessDeniedException>
            (
                () => tester.Execute(model, modKey),
                $"Should not have access with role '{roleName.DisplayText}'"
            );
        }
        var denyAccessModel = await createModel();
        var denyAccessLoggedInUser = await tester.Login();
        await SetUserRoles(denyAccessLoggedInUser, new AppRoleName[0], modifier, AppRoleName.DenyAccess);
        Assert.ThrowsAsync<AccessDeniedException>
        (
            () => tester.Execute(denyAccessModel, modKey),
            $"Should not have access with role '{AppRoleName.DenyAccess.DisplayText}'"
        );
    }

    private async Task SetUserRoles(AppUser loggedInUser, AppRoleName[] rolesToKeep, ModifierModel modifier, params AppRoleName[] roleNames)
    {
        var factory = tester.Services.GetRequiredService<HubFactory>();
        var app = await factory.Apps.AppOrUnknown(HubInfo.AppKey);
        var efModifier = await app.Modifier(modifier.ID);
        var userRoles = await loggedInUser.Modifier(efModifier).AssignedRoles();
        foreach (var userRole in userRoles)
        {
            await loggedInUser.Modifier(efModifier).UnassignRole(userRole);
        }
        foreach (var roleName in roleNames)
        {
            var role = await app.Role(roleName);
            await loggedInUser.Modifier(efModifier).AssignRole(role);
        }
        if (rolesToKeep.Any())
        {
            var defaultModifier = await app.DefaultModifier();
            foreach (var roleName in rolesToKeep)
            {
                var role = await app.Role(roleName);
                await loggedInUser.Modifier(defaultModifier).AssignRole(role);
            }
        }
    }
}