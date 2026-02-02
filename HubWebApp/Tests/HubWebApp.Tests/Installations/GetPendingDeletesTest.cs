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
                new GetInstallationActivitiesRequest(qualifiedMachineName),
                HubInfo.Roles.Admin,
                HubInfo.Roles.InstallationManager
            );
    }

    [Test]
    public async Task ShouldGetPendingDeletes()
    {
        var tester = await Setup();
        const string machineName = "machine.example.com";
        var installationID = await PrepareDeletePendingInstallation(tester, machineName);
        var installations = await tester.Execute(new GetInstallationActivitiesRequest(machineName));
        Assert.That
        (
            installations.Select(inst => inst.Installation.ID),
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
        var appVersion = await hubApp.CurrentVersion(ct: default);
        var newInstResult = await NewInstallation
        (
            tester,
            new NewInstallationRequest
            (
                versionName: appVersion.Version.ToModel().VersionName,
                qualifiedMachineName: machineName,
                appKey: HubInfo.AppKey,
                domain: "",
                siteName: ""
            )
        );
        var installationID = newInstResult.GetCurrentInstallation().Installation.ID;
        await StartInstallation(tester, new GetInstallationRequest(installationID));
        await Installed(tester, new GetInstallationRequest(installationID));
        var installations = await tester.Execute(new GetInstallationActivitiesRequest(machineName));
        Assert.That
        (
            installations.Length,
            Is.EqualTo(0),
            "Should not include installations that are not pending delete"
        );
    }

    private async Task<HubActionTester<GetInstallationActivitiesRequest, AppVersionInstallationModel[]>> Setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup();
        return HubActionTester.Create(services, hubApi => hubApi.Installations.GetPendingDeletes);
    }

    private async Task<int> PrepareDeletePendingInstallation(HubActionTester<GetInstallationActivitiesRequest, AppVersionInstallationModel[]> tester, string qualifiedMachineName)
    {
        var hubApp = await tester.HubApp();
        var appVersion = await hubApp.CurrentVersion(ct: default);
        var newInstResult = await NewInstallation
        (
            tester,
            new NewInstallationRequest
            (
                versionName: appVersion.Version.ToModel().VersionName,
                qualifiedMachineName: qualifiedMachineName,
                appKey: HubInfo.AppKey,
                domain: "",
                siteName: ""
            )
        );
        await StartInstallation(tester, new GetInstallationRequest(newInstResult.GetCurrentInstallation().Installation.ID));
        await Installed(tester, new GetInstallationRequest(newInstResult.GetCurrentInstallation().Installation.ID));
        await RequestDelete(tester, new GetInstallationRequest(newInstResult.GetCurrentInstallation().Installation.ID));
        return newInstResult.GetCurrentInstallation().Installation.ID;
    }

    private Task<NewInstallationResult> NewInstallation(IHubActionTester tester, NewInstallationRequest model)
    {
        var hubApi = tester.Services.GetRequiredService<HubAppApiFactory>().CreateForSuperUser();
        return hubApi.Install.NewInstallation.Invoke(model);
    }

    private Task StartInstallation(IHubActionTester tester, GetInstallationRequest model)
    {
        var hubApi = tester.Services.GetRequiredService<HubAppApiFactory>().CreateForSuperUser();
        return hubApi.Install.BeginInstallation.Invoke(model);
    }

    private Task Installed(IHubActionTester tester, GetInstallationRequest model)
    {
        var hubApi = tester.Services.GetRequiredService<HubAppApiFactory>().CreateForSuperUser();
        return hubApi.Install.Installed.Invoke(model);
    }

    private Task RequestDelete(IHubActionTester tester, GetInstallationRequest model)
    {
        var hubApi = tester.Services.GetRequiredService<HubAppApiFactory>().CreateForSuperUser();
        return hubApi.Installations.RequestDelete.Invoke(model);
    }
}
