namespace HubWebApp.Tests;

internal sealed class GetInstallTemplatesTest
{
    [Test]
    public async Task ShouldGetInstallTemplates()
    {
        var sp = await Setup();
        var installTemplate1 = await ConfigureInstallTemplate
        (
            sp,
            new ConfigureInstallTemplateRequest
            (
                templateName: "Template 1",
                destinationMachineName: "server1",
                domain: "www.example.com",
                siteName: "Default"
            )
        );
        var installTemplate2 = await ConfigureInstallTemplate
        (
            sp,
            new ConfigureInstallTemplateRequest
            (
                templateName: "Template 2",
                destinationMachineName: "server2",
                domain: "www.example2.com",
                siteName: "Default2"
            )
        );
        var installTemplates = await GetInstallTemplates(sp);
        Assert.That
        (
            installTemplates.OrderBy(t => t.TemplateName).ToArray(),
            Is.EquivalentTo([installTemplate1, installTemplate2])
        );
    }

    private Task<IServiceProvider> Setup()
    {
        var host = new HubTestHost();
        return host.Setup();
    }

    private Task<InstallConfigurationTemplateModel> ConfigureInstallTemplate(IServiceProvider sp, ConfigureInstallTemplateRequest requestData)
    {
        var tester = HubActionTester.Create(sp, api => api.InstallTemplates.ConfigureInstallTemplate);
        return tester.Execute(requestData);
    }

    private Task<InstallConfigurationTemplateModel[]> GetInstallTemplates(IServiceProvider sp)
    {
        var tester = HubActionTester.Create(sp, api => api.InstallTemplates.GetInstallTemplates);
        return tester.Execute(new());
    }

}
