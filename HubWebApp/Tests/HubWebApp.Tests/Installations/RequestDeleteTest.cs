using Microsoft.EntityFrameworkCore;
using XTI_HubDB.Entities;

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
                new InstallationIDRequest(installationID),
                HubInfo.Roles.Admin,
                HubInfo.Roles.InstallationManager
            );
    }

    [Test]
    public async Task ShouldSetInstallStatusToDeletePending()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
        const string qualifiedMachineName = "machine.example.com";
        var installationID = await PrepareInstallation(tester, qualifiedMachineName);
        await tester.Execute(new InstallationIDRequest(installationID));
        var db = tester.Services.GetRequiredService<HubDbContext>();
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

    private async Task<HubActionTester<InstallationIDRequest, EmptyActionResult>> Setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup();
        return HubActionTester.Create(services, hubApi => hubApi.Installations.RequestDelete);
    }

    private async Task<int> PrepareInstallation(HubActionTester<InstallationIDRequest, EmptyActionResult> tester, string qualifiedMachineName)
    {
        var hubApp = await tester.HubApp();
        var appVersion = await hubApp.CurrentVersion(ct: default);
        var config = await AddDefaultConfiguration(tester, qualifiedMachineName);
        var requestedInstallationDetail = await RequestInstallation
        (
            tester,
            new AddAppInstallCommandRequest
            (
                appKey: HubInfo.AppKey,
                versionKey: appVersion.Version.Key(),
                installConfigurationID: config.ID,
                installAsCurrent: true,
                isAutoStartEnabled: true
            )
        );
        var installation = await StartInstallation(tester, new BeginInstallationRequest(requestedInstallationDetail.Command.ID, true));
        await Installed(tester, new InstallationIDRequest(installation.ID));
        return installation.ID;
    }

    private async Task<InstallConfigurationModel> AddDefaultConfiguration(IHubActionTester tester, string qualifiedMachineName)
    {
        var configTemplate = await AddConfigurationTemplate
        (
            tester,
            new ConfigureInstallTemplateRequest
            (
                templateName: "Default",
                destinationMachineName: qualifiedMachineName,
                domain: "",
                siteName: ""
            )
        );
        var config = await AddConfiguration
        (
            tester,
            new ConfigureInstallRequest
            (
                repoOwner: "JasonBenfield",
                repoName: "Fake",
                configurationName: "Default",
                appKey: HubInfo.AppKey,
                templateName: "Default",
                installSequence: 0
            )
        );
        return config;
    }

    private async Task<InstallConfigurationTemplateModel> AddConfigurationTemplate(IHubActionTester tester, ConfigureInstallTemplateRequest configRequest)
    {
        var hubApi = tester.Services.GetRequiredService<HubAppApiFactory>().CreateForSuperUser();
        var result = await hubApi.Install.ConfigureInstallTemplate.Execute(configRequest);
        return result.Data!;
    }

    private async Task<InstallConfigurationModel> AddConfiguration(IHubActionTester tester, ConfigureInstallRequest configRequest)
    {
        var hubApi = tester.Services.GetRequiredService<HubAppApiFactory>().CreateForSuperUser();
        var result = await hubApi.Install.ConfigureInstall.Execute(configRequest);
        return result.Data!;
    }

    private async Task<AppInstallCommandDetailModel> RequestInstallation(IHubActionTester tester, AddAppInstallCommandRequest requestData)
    {
        var hubApi = tester.Services.GetRequiredService<HubAppApiFactory>().CreateForSuperUser();
        var result = await hubApi.Installations.RequestInstallation.Execute(requestData);
        return result.Data!;
    }

    private Task<InstallationModel> StartInstallation(IHubActionTester tester, BeginInstallationRequest requestData)
    {
        var hubApi = tester.Services.GetRequiredService<HubAppApiFactory>().CreateForSuperUser();
        return hubApi.Installations.BeginInstallation.Invoke(requestData);
    }

    private Task Installed(IHubActionTester tester, InstallationIDRequest model)
    {
        var hubApi = tester.Services.GetRequiredService<HubAppApiFactory>().CreateForSuperUser();
        return hubApi.Installations.Installed.Execute(model);
    }
}
