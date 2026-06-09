using XTI_HubWebAppApiActions;

namespace HubWebApp.Tests;

internal sealed class ConfigureInstallTemplateTest
{
    [Test]
    public async Task ShouldAddInstallTemplate()
    {
        var sp = await Setup();
        var installTemplate = await ConfigureInstallTemplate
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
        Assert.That(installTemplate.IsFound(), Is.True);
        Assert.That(installTemplate.TemplateName, Is.EqualTo("Template 1"));
        Assert.That(installTemplate.DestinationMachineName, Is.EqualTo("server1"));
        Assert.That(installTemplate.Domain, Is.EqualTo("www.example.com"));
        Assert.That(installTemplate.SiteName, Is.EqualTo("Default"));
    }

    [Test]
    public async Task ShouldUpdateInstallTemplate()
    {
        var sp = await Setup();
        var originalInstallTemplate = await ConfigureInstallTemplate
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
        var updatedInstallTemplate = await ConfigureInstallTemplate
        (
            sp,
            new ConfigureInstallTemplateRequest
            (
                templateName: "Template 1",
                destinationMachineName: "server2",
                domain: "www.anotherexample.com",
                siteName: "Other"
            )
        );
        Assert.That(originalInstallTemplate.ID, Is.EqualTo(updatedInstallTemplate.ID));
        Assert.That(originalInstallTemplate.TemplateName, Is.EqualTo(updatedInstallTemplate.TemplateName));
        Assert.That(updatedInstallTemplate.DestinationMachineName, Is.EqualTo("server2"));
        Assert.That(updatedInstallTemplate.Domain, Is.EqualTo("www.anotherexample.com"));
        Assert.That(updatedInstallTemplate.SiteName, Is.EqualTo("Other"));
    }

    [Test]
    public async Task ShouldAddMultipleInstallTemplates()
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
        Assert.That(installTemplate1.ID, Is.Not.EqualTo(installTemplate2.ID));
        Assert.That(installTemplate1.TemplateName, Is.EqualTo("Template 1"));
        Assert.That(installTemplate1.DestinationMachineName, Is.EqualTo("server1"));
        Assert.That(installTemplate1.Domain, Is.EqualTo("www.example.com"));
        Assert.That(installTemplate1.SiteName, Is.EqualTo("Default"));
        Assert.That(installTemplate2.TemplateName, Is.EqualTo("Template 2"));
        Assert.That(installTemplate2.DestinationMachineName, Is.EqualTo("server2"));
        Assert.That(installTemplate2.Domain, Is.EqualTo("www.example2.com"));
        Assert.That(installTemplate2.SiteName, Is.EqualTo("Default2"));
    }

    [Test]
    public async Task ShouldRequireTemplateName()
    {
        var sp = await Setup();
        var ex = Assert.ThrowsAsync<ValidationFailedException>
        (
            () => ConfigureInstallTemplate
            (
                sp,
                new ConfigureInstallTemplateRequest
                (
                    templateName: "",
                    destinationMachineName: "server1",
                    domain: "www.example.com",
                    siteName: "Default"
                )
            )
        );
        Assert.That
        (
            ex?.Errors.Select(e=>e.Message),
            Is.EquivalentTo([InstallErrors.TemplateNameIsRequired])
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

}
