using XTI_Hub.Abstractions;

namespace HubWebApp.IntegrationTests;

internal sealed class InstallConfigurationTest
{
    [Test]
    public async Task ShouldGetInstallConfigurations()
    {
        var sp = await Setup();
        var tester = HubActionTester.Create(sp, api => api.Install.GetInstallConfigurations);
        var installConfigs = await tester.Execute
        (
            new GetInstallConfigurationsRequest
            (
                repoOwner: "JasonBenfield",
                repoName: "HubWebApp",
                configurationName: ""
            )
        );
        installConfigs.WriteToConsole();
    }

    [Test]
    public async Task ShouldConfigureInstallTemplate()
    {
        var sp = await Setup();
        var tester = HubActionTester.Create(sp, api => api.Install.ConfigureInstallTemplate);
        var installTemplate = await tester.Execute
        (
            new ConfigureInstallTemplateRequest
            (
                templateName: "Test",
                destinationMachineName: "srvtest1.xartogg.com",
                domain: "development.guinevere.com",
                siteName: "Development"
            )
        );
        installTemplate.WriteToConsole();
    }

    [Test]
    public async Task ShouldConfigureInstall()
    {
        var sp = await Setup();
        var tester = HubActionTester.Create(sp, api => api.Install.ConfigureInstall);
        var installTemplate = await tester.Execute
        (
            new ConfigureInstallRequest
            (
                repoOwner: "JasonBenfield",
                repoName: "HubWebApp",
                configurationName: "Default",
                appKey: AppKey.WebApp("Authenticator"),
                templateName: "Test",
                installSequence: 10
            )
        );
        installTemplate.WriteToConsole();
    }

    [Test]
    public async Task ShouldDeleteInstallConfiguration()
    {
        var sp = await Setup();
        var tester = HubActionTester.Create(sp, api => api.Install.DeleteInstallConfiguration);
        await tester.Execute
        (
            new DeleteInstallConfigurationRequest
            (
                repoOwner: "JasonBenfield",
                repoName: "HubWebApp",
                configurationName: "Default",
                appKey: AppKey.WebApp("Authenticator")
            )
        );
    }

    private Task<IServiceProvider> Setup(string envName = "Development")
    {
        var host = new HubTestHost();
        return host.Setup(envName);
    }
}
