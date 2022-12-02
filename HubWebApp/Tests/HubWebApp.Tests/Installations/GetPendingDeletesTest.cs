using XTI_HubWebAppApi.AppInstall;

namespace HubWebApp.Tests;

internal sealed class GetPendingDeletesTest
{
    [Test]
    public async Task ShouldThrowError_WhenRoleIsNotAssignedToUser()
    {
        var tester = await Setup();
        const string qualifiedMachineName = "machine.example.com";
        await PrepareDeletePendingInstallation(tester, qualifiedMachineName);
        await AccessAssertions.Create(tester)
            .ShouldThrowError_WhenAccessIsDenied
            (
                new GetPendingDeletesRequest(qualifiedMachineName),
                HubInfo.Roles.Admin
            );
    }

    [Test]
    public async Task ShouldGetPendingDeletes()
    {
        var tester = await Setup();
        const string machineName = "machine.example.com";
        var installationID = await PrepareDeletePendingInstallation(tester, machineName);
        var installations = await tester.Execute(new GetPendingDeletesRequest(machineName));
        Assert.That
        (
            installations.Select(inst => inst.ID),
            Is.EqualTo(new[] { installationID }),
            "Should get pending deletes"
        );
    }

    [Test]
    public async Task ShouldNotIncludeOtherInstallations()
    {
        var tester = await Setup();
        const string machineName = "machine.example.com";
        var hubApp = await tester.HubApp();
        var version = await hubApp.CurrentVersion();
        var newInstResult = await NewInstallation(tester, new NewInstallationRequest
        {
            VersionName = version.ToVersionModel().VersionName,
            QualifiedMachineName = machineName,
            AppKey = HubInfo.AppKey
        });
        var installationID = newInstResult.CurrentInstallationID;
        await StartInstallation(tester, new GetInstallationRequest(installationID));
        await Installed(tester, new GetInstallationRequest(installationID));
        var installations = await tester.Execute(new GetPendingDeletesRequest(machineName));
        Assert.That
        (
            installations.Length,
            Is.EqualTo(0),
            "Should not include installations that are not pending delete"
        );
    }

    private async Task<HubActionTester<GetPendingDeletesRequest, InstallationModel[]>> Setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup();
        return HubActionTester.Create(services, hubApi => hubApi.Installations.GetPendingDeletes);
    }

    private async Task<int> PrepareDeletePendingInstallation(HubActionTester<GetPendingDeletesRequest, InstallationModel[]> tester, string qualifiedMachineName)
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
        await RequestDelete(tester, new GetInstallationRequest(newInstResult.CurrentInstallationID));
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

    private Task RequestDelete(IHubActionTester tester, GetInstallationRequest model)
    {
        var hubApi = tester.Services.GetRequiredService<HubAppApiFactory>().CreateForSuperUser();
        return hubApi.Installations.RequestDelete.Execute(model);
    }
}
