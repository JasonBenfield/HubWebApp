using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using XTI_App.Abstractions;
using XTI_Hub;
using XTI_HubAppApi;
using XTI_HubAppApi.AppInstall;
using XTI_HubDB.Entities;

namespace HubWebApp.Tests;

sealed class BeginCurrentInstallationTest
{
    [Test]
    public async Task ShouldSetCurrentInstallationStatusToInstallStarted()
    {
        var tester = await setup();
        var factory = tester.Services.GetRequiredService<AppFactory>();
        var hubApp = await factory.Apps.App(HubInfo.AppKey);
        var version = await hubApp.CurrentVersion();
        tester.LoginAsAdmin();
        const string qualifiedMachineName = "machine.example.com";
        var newInstResult = await newInstallation(tester, new NewInstallationRequest
        {
            QualifiedMachineName = qualifiedMachineName,
            AppKey = HubInfo.AppKey
        });
        var request = new BeginInstallationRequest
        {
            QualifiedMachineName = qualifiedMachineName,
            AppKey = HubInfo.AppKey,
            VersionKey = version.Key().Value
        };
        await tester.Execute(request);
        var currentInstallation = await getInstallation(tester, newInstResult.CurrentInstallationID);
        Assert.That
        (
            InstallStatus.Values.Value(currentInstallation.Status),
            Is.EqualTo(InstallStatus.Values.InstallStarted),
            "Should set current installation status to install started"
        );
    }

    [Test]
    public async Task ShouldReturnInstallationID()
    {
        var tester = await setup();
        var factory = tester.Services.GetRequiredService<AppFactory>();
        var hubApp = await factory.Apps.App(HubInfo.AppKey);
        var version = await hubApp.CurrentVersion();
        tester.LoginAsAdmin();
        const string qualifiedMachineName = "machine.example.com";
        var newInstResult = await newInstallation(tester, new NewInstallationRequest
        {
            QualifiedMachineName = qualifiedMachineName,
            AppKey = HubInfo.AppKey
        });
        var request = new BeginInstallationRequest
        {
            QualifiedMachineName = qualifiedMachineName,
            AppKey = HubInfo.AppKey,
            VersionKey = version.Key().Value
        };
        var installationID = await tester.Execute(request);
        Assert.That
        (
            installationID,
            Is.EqualTo(newInstResult.CurrentInstallationID),
            "Should return current installation ID"
        );
    }

    [Test]
    public async Task ShouldSetCurrentInstallationVersion()
    {
        var tester = await setup();
        var factory = tester.Services.GetRequiredService<AppFactory>();
        tester.LoginAsAdmin();
        const string qualifiedMachineName = "machine.example.com";
        await newInstallation(tester, new NewInstallationRequest
        {
            QualifiedMachineName = qualifiedMachineName,
            AppKey = HubInfo.AppKey
        });
        var nextVersion = await factory.Apps.StartNewVersion
        (
            HubInfo.AppKey,
            "hub.example.com",
            AppVersionType.Values.Major,
            DateTimeOffset.Now
        );
        await nextVersion.Publishing();
        var hubApiFactory = tester.Services.GetRequiredService<HubAppApiFactory>();
        var hubApi = hubApiFactory.CreateForSuperUser();
        await hubApi.Install.RegisterApp.Invoke(new RegisterAppRequest
        {
            AppTemplate = hubApiFactory.CreateTemplate().ToModel(),
            VersionKey = nextVersion.Key().Value,
            Versions = new AppVersionModel[0]
        });
        await nextVersion.Published();
        await newInstallation(tester, new NewInstallationRequest
        {
            QualifiedMachineName = qualifiedMachineName,
            AppKey = HubInfo.AppKey
        });
        var request = new BeginInstallationRequest
        {
            QualifiedMachineName = qualifiedMachineName,
            AppKey = HubInfo.AppKey,
            VersionKey = nextVersion.Key().Value
        };
        var installationID = await tester.Execute(request);
        var currentInstallation = await getInstallation(tester, installationID);
        Assert.That
        (
            currentInstallation.VersionID,
            Is.EqualTo(nextVersion.ID.Value),
            "Should set current installation version"
        );
    }

    private static Task<InstallationEntity> getInstallation(IHubActionTester tester, int installationID)
    {
        var db = tester.Services.GetRequiredService<IHubDbContext>();
        return db.Installations.Retrieve()
            .Where(inst => inst.ID == installationID)
            .FirstAsync();
    }

    private async Task<NewInstallationResult> newInstallation(IHubActionTester tester, NewInstallationRequest model)
    {
        var hubApi = tester.Services.GetRequiredService<HubAppApiFactory>().CreateForSuperUser();
        var result = await hubApi.Install.NewInstallation.Execute(model);
        return result.Data;
    }

    private async Task<HubActionTester<BeginInstallationRequest, int>> setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup();
        return HubActionTester.Create(services, hubApi => hubApi.Install.BeginCurrentInstallation);
    }
}