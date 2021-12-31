using NUnit.Framework;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_App.Fakes;
using XTI_Hub;
using XTI_HubAppApi;

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
        var adminUser = tester.LoginAsAdmin();
        var ex = Assert.ThrowsAsync<Exception>(() => tester.Execute(model));
        Assert.That(ex?.Message, Is.EqualTo(AppErrors.ModifierIsRequired));
    }

    public void ShouldThrowError_WhenModifierIsNotFound(TModel model)
    {
        var adminUser = tester.LoginAsAdmin();
        var modKey = new ModifierKey("NotFound");
        Assert.ThrowsAsync<ModifierNotFoundException>(() => tester.Execute(model, modKey));
    }

    public void ShouldThrowError_WhenModifierIsNotAssignedToUser(TModel model, Modifier modifier)
    {
        var loggedInUser = tester.Login();
        Assert.ThrowsAsync<AccessDeniedException>(() => tester.Execute(model, modifier.ModKey()));
    }

    public void ShouldThrowError_WhenModifierIsNotAssignedToUser_ButRoleIsAssignedToUser(TModel model, AppRoleName role, FakeModifier modifier)
    {
        var loggedInUser = tester.Login(role);
        loggedInUser.AddRole(modifier, AppRoleName.DenyAccess);
        Assert.ThrowsAsync<AccessDeniedException>(() => tester.Execute(model, modifier.ModKey()));
    }

    public void ShouldThrowError_WhenRoleIsNotAssignedToUser(TModel model)
    {
        var loggedInUser = tester.Login();
        Assert.ThrowsAsync<AccessDeniedException>(() => tester.Execute(model));
    }

    public void ShouldThrowError_WhenModifiedRoleIsNotAssignedToUser(TModel model, FakeModifier modifier)
    {
        var loggedInUser = tester.Login();
        loggedInUser.AddRole(modifier, AppRoleName.DenyAccess);
        Assert.ThrowsAsync<AccessDeniedException>(() => tester.Execute(model, modifier.ModKey()));
    }
}