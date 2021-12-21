using NUnit.Framework;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_Hub;

namespace HubWebApp.Tests;

internal sealed class PublishVersionCommandTest
{
    [Test]
    public async Task ShouldRequireValidVersionType()
    {
        var tester = await setup();
        tester.Options.VersionType = "Whatever";
        Assert.ThrowsAsync<ArgumentException>(() => tester.Execute());
    }

    [Test]
    public async Task ShouldBeginPublishingTheVersion()
    {
        var tester = await setup();
        await tester.Execute();
        var versions = await tester.App.Versions();
        var newVersion = versions.First(v => !v.IsCurrent());
        await tester.Checkout(newVersion);
        var key = tester.App.Key();
        tester.Options.CommandBeginPublish(key.Name.Value, key.Type.DisplayText);
        await tester.Execute();
        newVersion = await tester.App.Version(newVersion.Key());
        Assert.That(newVersion.IsPublishing(), Is.True, "Should begin publishing the new version");
    }

    [Test]
    public async Task EndPublishShouldMakeTheVersionCurrent()
    {
        var tester = await setup();
        await tester.Execute();
        var versions = await tester.App.Versions();
        var newVersion = versions.First(v => !v.IsCurrent());
        await tester.Checkout(newVersion);
        var key = tester.App.Key();
        tester.Options.CommandBeginPublish(key.Name.Value, key.Type.DisplayText);
        await tester.Command().Execute(tester.Options);
        tester.Options.CommandCompleteVersion("JasonBenfield", "XTI_App", key.Name.Value, key.Type.DisplayText);
        await tester.Execute();
        newVersion = await tester.App.Version(newVersion.Key());
        Assert.That(newVersion.IsCurrent(), Is.True, "Should make the new version the current version");
    }

    [Test]
    public async Task ShouldNotAllowAPublishedVersionToGoBackToPublishing()
    {
        var tester = await setup();
        await tester.Execute();
        var versions = await tester.App.Versions();
        var newVersion = versions.First(v => !v.IsCurrent());
        await tester.Checkout(newVersion);
        var key = tester.App.Key();
        tester.Options.CommandBeginPublish(key.Name.Value, key.Type.DisplayText);
        await tester.Execute();
        tester.Options.CommandCompleteVersion("JasonBenfield", "XTI_App", key.Name.Value, key.Type.DisplayText);
        await tester.Execute();
        await tester.Checkout(newVersion);
        tester.Options.CommandBeginPublish(key.Name.Value, key.Type.DisplayText);
        Assert.ThrowsAsync<PublishException>(() => tester.Execute());
    }

    private async Task<ManageVersionTester> setup()
    {
        var tester = new ManageVersionTester();
        await tester.Setup();
        var appKey = tester.App.Key();
        tester.Options.CommandNewVersion
        (
            appKey.Name.DisplayText,
            appKey.Type.DisplayText,
            AppVersionType.Values.Patch.DisplayText,
            "JasonBenfield",
            "XTI_App"
        );
        return tester;
    }
}