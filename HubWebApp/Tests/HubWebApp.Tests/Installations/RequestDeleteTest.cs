﻿using Microsoft.EntityFrameworkCore;
using XTI_HubDB.Entities;
using XTI_HubWebAppApi.AppInstall;

namespace HubWebApp.Tests;

internal sealed class RequestDeleteTest
{
    [Test]
    public async Task ShouldThrowError_WhenRoleIsNotAssignedToUser()
    {
        var tester = await Setup();
        const string qualifiedMachineName = "machine.example.com";
        var installationID = await PrepareInstallation(tester, qualifiedMachineName);
        await AccessAssertions.Create(tester)
            .ShouldThrowError_WhenAccessIsDenied
            (
                new GetInstallationRequest(installationID),
                HubInfo.Roles.Admin
            );
    }

    [Test]
    public async Task ShouldSetInstallStatusToDeletePending()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
        const string qualifiedMachineName = "machine.example.com";
        var installationID = await PrepareInstallation(tester, qualifiedMachineName);
        await tester.Execute(new GetInstallationRequest(installationID));
        var db = tester.Services.GetRequiredService<IHubDbContext>();
        var installation = await db.Installations.Retrieve()
            .Where(inst => inst.ID == installationID)
            .FirstAsync();
        Assert.That
        (
            installation.Status, 
            Is.EqualTo(InstallStatus.Values.DeletePending.Value),
            "Should set install status to delete pending"
        );
    }

    private async Task<HubActionTester<GetInstallationRequest, EmptyActionResult>> Setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup();
        return HubActionTester.Create(services, hubApi => hubApi.Installations.RequestDelete);
    }

    private async Task<int> PrepareInstallation(HubActionTester<GetInstallationRequest, EmptyActionResult> tester, string qualifiedMachineName)
    {
        var hubApp = await tester.HubApp();
        var version = await hubApp.CurrentVersion();
        var newInstResult = await NewInstallation(tester, new NewInstallationRequest
        {
            VersionName = version.ToVersionModel().VersionName,
            QualifiedMachineName = qualifiedMachineName,
            AppKey = HubInfo.AppKey
        });
        await StartInstallation(tester, new GetInstallationRequest(newInstResult.CurrentInstallationID));
        await Installed(tester, new GetInstallationRequest(newInstResult.CurrentInstallationID));
        return newInstResult.CurrentInstallationID;
    }

    private async Task<NewInstallationResult> NewInstallation(IHubActionTester tester, NewInstallationRequest model)
    {
        var hubApi = tester.Services.GetRequiredService<HubAppApiFactory>().CreateForSuperUser();
        var result = await hubApi.Install.NewInstallation.Execute(model);
        return result.Data;
    }

    private Task StartInstallation(IHubActionTester tester, GetInstallationRequest model)
    {
        var hubApi = tester.Services.GetRequiredService<HubAppApiFactory>().CreateForSuperUser();
        return hubApi.Install.BeginInstallation.Execute(model);
    }

    private Task Installed(IHubActionTester tester, GetInstallationRequest model)
    {
        var hubApi = tester.Services.GetRequiredService<HubAppApiFactory>().CreateForSuperUser();
        return hubApi.Install.Installed.Execute(model);
    }
}
