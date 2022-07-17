using XTI_Hub.Abstractions;
using XTI_HubWebAppApi.AppInstall;

namespace HubWebApp.Tests;

public sealed class RegisterAppTest
{
    [Test]
    public async Task ShouldAddWebApp()
    {
        var tester = await setup();
        await tester.LoginAsAdmin();
        var request = createRequest(tester);
        await tester.Execute(request);
        var app = await getApp(tester);
        var appModel = app.ToModel();
        Assert.That(appModel.AppKey, Is.EqualTo(FakeInfo.AppKey), "Should add app");
        Assert.That(appModel.Title, Is.EqualTo($"{FakeInfo.AppKey.Name.DisplayText} {FakeInfo.AppKey.Type.DisplayText}"), "Should set app title");
    }

    private static async Task<App> getApp(IHubActionTester tester)
    {
        var appFactory = tester.Services.GetRequiredService<HubFactory>();
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
            VersionKey = AppVersionKey.Current
        };
        return request;
    }

    [Test]
    public async Task ShouldAddCurrentVersion()
    {
        var tester = await setup();
        await tester.LoginAsAdmin();
        var request = createRequest(tester);
        await tester.Execute(request);
        var app = await getApp(tester);
        Assert.DoesNotThrowAsync(() => app.CurrentVersion());
    }

    [Test]
    public async Task ShouldAddRoles()
    {
        var tester = await setup();
        await tester.LoginAsAdmin();
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
        Assert.That(appRoles.Select(r => r.ToModel().Name), Is.EquivalentTo(roleNames), "Should add role names from app role names");
    }

    [Test]
    public async Task ShouldAddUnknownApp()
    {
        var tester = await setup();
        await tester.LoginAsAdmin();
        var request = createRequest(tester);
        await tester.Execute(request);
        var appFactory = tester.Services.GetRequiredService<HubFactory>();
        var app = await appFactory.Apps.App(AppKey.Unknown);
        Assert.That(app.ToModel().ID, Is.GreaterThan(0), "Should add unknown app");
    }

    [Test]
    public async Task ShouldAddUnknownResourceGroup()
    {
        var tester = await setup();
        await tester.LoginAsAdmin();
        var request = createRequest(tester);
        await tester.Execute(request);
        var appFactory = tester.Services.GetRequiredService<HubFactory>();
        var app = await appFactory.Apps.App(AppKey.Unknown);
        var version = await app.CurrentVersion();
        var group = await version.ResourceGroupByName(ResourceGroupName.Unknown);
        Assert.That(group.ID, Is.GreaterThan(0), "Should add unknown resource group");
    }

    [Test]
    public async Task ShouldAddUnknownResource()
    {
        var tester = await setup();
        await tester.LoginAsAdmin();
        var request = createRequest(tester);
        await tester.Execute(request);
        var appFactory = tester.Services.GetRequiredService<HubFactory>();
        var app = await appFactory.Apps.App(AppKey.Unknown);
        var version = await app.CurrentVersion();
        var group = await version.ResourceGroupByName(ResourceGroupName.Unknown);
        var resource = await group.ResourceByName(ResourceName.Unknown);
        Assert.That(resource.ID, Is.GreaterThan(0), "Should add unknown resource");
    }

    [Test]
    public async Task ShouldAddResourceGroupsFromAppTemplateGroups()
    {
        var tester = await setup();
        await tester.LoginAsAdmin();
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
        await tester.LoginAsAdmin();
        var request = createRequest(tester);
        await tester.Execute(request);
        var app = await getApp(tester);
        var version = await app.CurrentVersion();
        var employeeGroup = (await version.ResourceGroups())
            .First(g => g.Name().Equals("Employee"));
        var allowedRoles = await employeeGroup.AllowedRoles();
        Assert.That
        (
            allowedRoles.Select(r => r.ToModel().Name),
            Is.EquivalentTo(new[] { AppRoleName.Admin }),
            "Should add allowed group roles from group template"
        );
    }

    [Test]
    public async Task ShouldAddResourcesFromAppTemplateActions()
    {
        var tester = await setup();
        await tester.LoginAsAdmin();
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
        await tester.LoginAsAdmin();
        var request = createRequest(tester);
        await tester.Execute(request);
        var app = await getApp(tester);
        var version = await app.CurrentVersion();
        var employeeGroup = (await version.ResourceGroups()).First(g => g.Name().Equals("Employee"));
        var addEmployeeAction = await employeeGroup.ResourceByName(new ResourceName("AddEmployee"));
        var allowedRoles = await addEmployeeAction.AllowedRoles();
        Assert.That
        (
            allowedRoles.Select(r => r.ToModel().Name),
            Is.EquivalentTo(new[] { AppRoleName.Admin, FakeAppRoles.Instance.Manager }),
            "Should add allowed resource roles from action template"
        );
    }

    [Test]
    public async Task ShouldAddDefaultModifierCategory()
    {
        var tester = await setup();
        await tester.LoginAsAdmin();
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
        await tester.LoginAsAdmin();
        var request = createRequest(tester);
        await tester.Execute(request);
        var app = await getApp(tester);
        var category = await app.ModCategory(ModifierCategoryName.Default);
        Assert.That(category.ID, Is.GreaterThan(0), "Should add default modifier to app");
    }

    [Test]
    public async Task ShouldSetModifierCategoryForGroup()
    {
        var tester = await setup();
        await tester.LoginAsAdmin();
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
        await tester.LoginAsAdmin();
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
        await tester.LoginAsAdmin();
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
        await tester.LoginAsAdmin();
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
        await tester.LoginAsAdmin();
        var request = createRequest(tester);
        await tester.Execute(request);
        var app = await getApp(tester);
        var version = await app.CurrentVersion();
        var employeeGroup = await version.ResourceGroupByName(new ResourceGroupName("Employee"));
        var resource = await employeeGroup.ResourceByName(new ResourceName("AddEmployee"));
        var resourceModel = resource.ToModel();
        Assert.That(resourceModel.IsAnonymousAllowed, Is.False, "Should deny anonymous");
    }

    private async Task<HubActionTester<RegisterAppRequest, AppModel>> setup()
    {
        var host = new HubTestHost();
        var sp = await host.Setup();
        var hubAdmin = sp.GetRequiredService<IHubAdministration>();
        var versionName = new AppVersionName("FakeWebApp");
        await hubAdmin.AddOrUpdateApps(versionName, new[] { new AppDefinitionModel(FakeInfo.AppKey) });
        await hubAdmin.AddOrUpdateVersions
        (
            new[] { FakeInfo.AppKey },
            new[]
            {
                new XtiVersionModel
                {
                    VersionName = versionName,
                    VersionKey = new AppVersionKey(1),
                    VersionNumber = new AppVersionNumber(1,0,0),
                    Status = AppVersionStatus.Values.Current,
                    VersionType = AppVersionType.Values.Major,
                    TimeAdded = DateTime.Now
                }
            }
        );
        return HubActionTester.Create(sp, hubApi => hubApi.Install.RegisterApp);
    }
}