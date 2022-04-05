using XTI_Hub.Abstractions;
using XTI_HubAppApi.AppInstall;

namespace HubWebApp.Tests;

public sealed class RegisterAppTest
{
    [Test]
    public async Task ShouldAddWebApp()
    {
        var tester = await setup();
        tester.LoginAsAdmin();
        var request = createRequest(tester);
        await tester.Execute(request);
        var app = await getApp(tester);
        Assert.That(app.Key(), Is.EqualTo(FakeInfo.AppKey), "Should add app");
        Assert.That(app.Title, Is.EqualTo(FakeInfo.AppKey.Name.DisplayText), "Should set app title");
    }

    private static async Task<App> getApp(IHubActionTester tester)
    {
        var appFactory = tester.Services.GetRequiredService<AppFactory>();
        var app = await appFactory.Apps.App(FakeInfo.AppKey);
        return app;
    }

    private static RegisterAppRequest createRequest(IHubActionTester tester)
    {
        var apiFactory = new FakeAppApiFactory(tester.Services);
        var fakeApi = apiFactory.CreateTemplate();
        var request = new RegisterAppRequest
        {
            AppTemplate = fakeApi.ToModel(),
            VersionKey = AppVersionKey.Current,
            Versions = new XtiVersionModel[0]
        };
        return request;
    }

    [Test]
    public async Task ShouldAddCurrentVersion()
    {
        var tester = await setup();
        tester.LoginAsAdmin();
        var request = createRequest(tester);
        await tester.Execute(request);
        var app = await getApp(tester);
        Assert.DoesNotThrowAsync(() => app.CurrentVersion());
    }

    [Test]
    public async Task ShouldAddRoles()
    {
        var tester = await setup();
        tester.LoginAsAdmin();
        var request = createRequest(tester);
        await tester.Execute(request);
        var app = await getApp(tester);
        var roleNames = new[]
        {
            AppRoleName.Admin,
            AppRoleName.DenyAccess,
            AppRoleName.ManageUserCache,
            FakeInfo.Roles.Manager,
            FakeInfo.Roles.Viewer
        };
        var appRoles = await app.Roles();
        Assert.That(appRoles.Select(r => r.Name()), Is.EquivalentTo(roleNames), "Should add role names from app role names");
    }

    [Test]
    public async Task ShouldAddUnknownApp()
    {
        var tester = await setup();
        tester.LoginAsAdmin();
        var request = createRequest(tester);
        await tester.Execute(request);
        var appFactory = tester.Services.GetRequiredService<AppFactory>();
        var app = await appFactory.Apps.App(AppKey.Unknown);
        Assert.That(app.ID.IsValid(), Is.True, "Should add unknown app");
    }

    [Test]
    public async Task ShouldAddUnknownResourceGroup()
    {
        var tester = await setup();
        tester.LoginAsAdmin();
        var request = createRequest(tester);
        await tester.Execute(request);
        var appFactory = tester.Services.GetRequiredService<AppFactory>();
        var app = await appFactory.Apps.App(AppKey.Unknown);
        var version = await app.CurrentVersion();
        var group = await version.ResourceGroupByName(ResourceGroupName.Unknown);
        Assert.That(group.ID.IsValid(), Is.True, "Should add unknown resource group");
    }

    [Test]
    public async Task ShouldAddUnknownResource()
    {
        var tester = await setup();
        tester.LoginAsAdmin();
        var request = createRequest(tester);
        await tester.Execute(request);
        var appFactory = tester.Services.GetRequiredService<AppFactory>();
        var app = await appFactory.Apps.App(AppKey.Unknown);
        var version = await app.CurrentVersion();
        var group = await version.ResourceGroupByName(ResourceGroupName.Unknown);
        var resource = await group.ResourceByName(ResourceName.Unknown);
        Assert.That(resource.ID.IsValid(), Is.True, "Should add unknown resource");
    }

    [Test]
    public async Task ShouldAddResourceGroupsFromAppTemplateGroups()
    {
        var tester = await setup();
        tester.LoginAsAdmin();
        var request = createRequest(tester);
        await tester.Execute(request);
        var app = await getApp(tester);
        var version = await app.CurrentVersion();
        var groups = (await version.ResourceGroups()).ToArray();
        Assert.That
        (
            groups.Select(g => g.Name()),
            Is.EquivalentTo
            (
                new[]
                {
                        new ResourceGroupName("employee"),
                        new ResourceGroupName("product"),
                        new ResourceGroupName("login"),
                        new ResourceGroupName("home") ,
                        new ResourceGroupName("agenda")
                }
            ),
            "Should add resource groups from template groups"
        );
    }

    [Test]
    public async Task ShouldAddAllowedGroupRoleFromAppGroupTemplate()
    {
        var tester = await setup();
        tester.LoginAsAdmin();
        var request = createRequest(tester);
        await tester.Execute(request);
        var app = await getApp(tester);
        var version = await app.CurrentVersion();
        var employeeGroup = (await version.ResourceGroups())
            .First(g => g.Name().Equals("Employee"));
        var allowedRoles = await employeeGroup.AllowedRoles();
        Assert.That
        (
            allowedRoles.Select(r => r.Name()),
            Is.EquivalentTo(new[] { AppRoleName.Admin }),
            "Should add allowed group roles from group template"
        );
    }

    [Test]
    public async Task ShouldAddResourcesFromAppTemplateActions()
    {
        var tester = await setup();
        tester.LoginAsAdmin();
        var request = createRequest(tester);
        await tester.Execute(request);
        var app = await getApp(tester);
        var version = await app.CurrentVersion();
        var group = await version.ResourceGroupByName(new ResourceGroupName("employee"));
        var resources = (await group.Resources()).ToArray();
        Assert.That
        (
            resources.Select(r => r.Name()),
            Is.EquivalentTo(new[] { new ResourceName("AddEmployee"), new ResourceName("Employee"), new ResourceName("SubmitFakeForm") }),
            "Should add resources from template actions"
        );
    }

    [Test]
    public async Task ShouldAddAllowedResourceRoleFromActionTemplate()
    {
        var tester = await setup();
        tester.LoginAsAdmin();
        var request = createRequest(tester);
        await tester.Execute(request);
        var app = await getApp(tester);
        var version = await app.CurrentVersion();
        var employeeGroup = (await version.ResourceGroups()).First(g => g.Name().Equals("Employee"));
        var addEmployeeAction = await employeeGroup.ResourceByName(new ResourceName("AddEmployee"));
        var allowedRoles = await addEmployeeAction.AllowedRoles();
        Assert.That
        (
            allowedRoles.Select(r => r.Name()),
            Is.EquivalentTo(new[] { AppRoleName.Admin, FakeAppRoles.Instance.Manager }),
            "Should add allowed resource roles from action template"
        );
    }

    [Test]
    public async Task ShouldAddDefaultModifierCategory()
    {
        var tester = await setup();
        tester.LoginAsAdmin();
        var request = createRequest(tester);
        await tester.Execute(request);
        var app = await getApp(tester);
        var modCategory = await app.ModCategory(ModifierCategoryName.Default);
        Assert.That
        (
            modCategory.Name().Equals(ModifierCategoryName.Default),
            Is.True,
            "Should add default modifier category"
        );
    }

    [Test]
    public async Task ShouldAddDefaultModifierCategoryToApp()
    {
        var tester = await setup();
        tester.LoginAsAdmin();
        var request = createRequest(tester);
        await tester.Execute(request);
        var app = await getApp(tester);
        var category = await app.ModCategory(ModifierCategoryName.Default);
        Assert.That(category.ID.IsValid(), Is.True, "Should add default modifier to app");
    }

    [Test]
    public async Task ShouldSetModifierCategoryForGroup()
    {
        var tester = await setup();
        tester.LoginAsAdmin();
        var request = createRequest(tester);
        await tester.Execute(request);
        var app = await getApp(tester);
        var version = await app.CurrentVersion();
        var employeeGroup = await version.ResourceGroupByName(new ResourceGroupName("Employee"));
        var modCategory = await employeeGroup.ModCategory();
        Assert.That(modCategory.Name(), Is.EqualTo(FakeInfo.ModCategories.Department));
    }

    [Test]
    public async Task ShouldAllowAnonymousForResourceGroup()
    {
        var tester = await setup();
        tester.LoginAsAdmin();
        var request = createRequest(tester);
        await tester.Execute(request);
        var app = await getApp(tester);
        var version = await app.CurrentVersion();
        var loginGroup = await version.ResourceGroupByName(new ResourceGroupName("Login"));
        var loginGroupModel = loginGroup.ToModel();
        Assert.That(loginGroupModel.IsAnonymousAllowed, Is.True, "Should allow anonymous");
    }

    [Test]
    public async Task ShouldDenyAnonymousForResourceGroup()
    {
        var tester = await setup();
        tester.LoginAsAdmin();
        var request = createRequest(tester);
        await tester.Execute(request);
        var app = await getApp(tester);
        var version = await app.CurrentVersion();
        var employeeGroup = await version.ResourceGroupByName(new ResourceGroupName("Employee"));
        var employeeGroupModel = employeeGroup.ToModel();
        Assert.That(employeeGroupModel.IsAnonymousAllowed, Is.False, "Should deny anonymous");
    }

    [Test]
    public async Task ShouldAllowAnonymousForResource()
    {
        var tester = await setup();
        tester.LoginAsAdmin();
        var request = createRequest(tester);
        await tester.Execute(request);
        var app = await getApp(tester);
        var version = await app.CurrentVersion();
        var loginGroup = await version.ResourceGroupByName(new ResourceGroupName("Login"));
        var resource = await loginGroup.ResourceByName(new ResourceName("Authenticate"));
        var resourceModel = resource.ToModel();
        Assert.That(resourceModel.IsAnonymousAllowed, Is.True, "Should allow anonymous");
    }

    [Test]
    public async Task ShouldDenyAnonymousForResource()
    {
        var tester = await setup();
        tester.LoginAsAdmin();
        var request = createRequest(tester);
        await tester.Execute(request);
        var app = await getApp(tester);
        var version = await app.CurrentVersion();
        var employeeGroup = await version.ResourceGroupByName(new ResourceGroupName("Employee"));
        var resource = await employeeGroup.ResourceByName(new ResourceName("AddEmployee"));
        var resourceModel = resource.ToModel();
        Assert.That(resourceModel.IsAnonymousAllowed, Is.False, "Should deny anonymous");
    }

    private async Task<HubActionTester<RegisterAppRequest, AppWithModKeyModel>> setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup();
        return HubActionTester.Create(services, hubApi => hubApi.Install.RegisterApp);
    }
}