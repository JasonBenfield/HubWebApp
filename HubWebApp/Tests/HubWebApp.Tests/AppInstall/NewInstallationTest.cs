using Microsoft.EntityFrameworkCore;
using XTI_HubDB.EF;
using XTI_HubDB.Entities;

namespace HubWebApp.Tests;

sealed class NewInstallationTest
{
    [Test]
    public async Task ShouldRequireVersionName()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
        var request = new NewInstallationRequest
        (
            versionName: AppVersionName.None,
            appKey: HubInfo.AppKey,
            qualifiedMachineName: "destination.example.com",
            domain: "",
            siteName: ""
        );
        var ex = Assert.ThrowsAsync<ValidationFailedException>(() => tester.Execute(request));
        Assert.That(ex?.Errors.Select(e => e.Message), Is.EquivalentTo(new[] { AppErrors.VersionNameIsRequired }));
    }

    [Test]
    public async Task ShouldRequireMachineName()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
        var request = new NewInstallationRequest
        (
            versionName: new AppVersionName("HubWebApp"),
            appKey: HubInfo.AppKey,
            qualifiedMachineName: "",
            domain: "",
            siteName: ""
        );
        var ex = Assert.ThrowsAsync<ValidationFailedException>(() => tester.Execute(request));
        Assert.That(ex?.Errors.Select(e => e.Message), Is.EquivalentTo(new[] { AppErrors.MachineNameIsRequired }));
    }

    [Test]
    public async Task ShouldAddInstallLocation()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
        var request = new NewInstallationRequest
        (
            versionName: new AppVersionName("HubWebApp"),
            appKey: HubInfo.AppKey,
            qualifiedMachineName: "destination.example.com",
            domain: "",
            siteName: ""
        );
        await tester.Execute(request);
        var installLocations = await GetInstallLocations(tester);
        Assert.That(installLocations.Length, Is.EqualTo(1), "Should add install location");
        Assert.That(installLocations[0].QualifiedMachineName, Is.EqualTo(request.QualifiedMachineName), "Should add install location");
    }

    [Test]
    public async Task ShouldNotAddInstallLocationWithDuplicateQualifiedMachineName()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
        var request = new NewInstallationRequest
        (
            versionName: new AppVersionName("HubWebApp"),
            appKey: HubInfo.AppKey,
            qualifiedMachineName: "destination.example.com",
            domain: "",
            siteName: ""
        );
        await tester.Execute(request);
        await tester.Execute(request);
        var installLocations = await GetInstallLocations(tester);
        Assert.That(installLocations.Length, Is.EqualTo(1), "Should not add duplicate install location");
    }

    [Test]
    public async Task ShouldAddCurrentInstallation()
    {
        var tester = await Setup();
        var hubApp = await tester.HubApp();
        await hubApp.CurrentVersion();
        await tester.LoginAsAdmin();
        var request = new NewInstallationRequest
        (
            versionName: new AppVersionName("HubWebApp"),
            appKey: HubInfo.AppKey,
            qualifiedMachineName: "destination.example.com",
            domain: "",
            siteName: ""
        );
        await tester.Execute(request);
        var currentInstallation = await GetCurrentInstallation(tester);
        var installationVersion = await GetVersion(tester, currentInstallation);
        Assert.That(currentInstallation?.AppVersionID, Is.EqualTo(installationVersion.ID), "Should add current installation");
        var installLocations = await GetInstallLocations(tester);
        Assert.That(currentInstallation?.LocationID, Is.EqualTo(installLocations[0].ID));
    }

    [Test]
    public async Task ShouldAddCurrentInstallationWithInstallPendingStatus()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
        var request = new NewInstallationRequest
        (
            versionName: new AppVersionName("HubWebApp"),
            appKey: HubInfo.AppKey,
            qualifiedMachineName: "destination.example.com",
            domain: "",
            siteName: ""
        );
        await tester.Execute(request);
        var currentInstallation = await GetCurrentInstallation(tester);
        Assert.That(currentInstallation?.Status, Is.EqualTo(InstallStatus.Values.InstallPending.Value), "Should add current installation with install pending status");
    }

    [Test]
    public async Task ShouldAddDuplicateCurrentInstallation()
    {
        var tester = await Setup();
        var hubApp = await tester.HubApp();
        var version = await hubApp.CurrentVersion();
        await tester.LoginAsAdmin();
        var request = new NewInstallationRequest
        (
            versionName: new AppVersionName("HubWebApp"),
            appKey: HubInfo.AppKey,
            qualifiedMachineName: "destination.example.com",
            domain: "",
            siteName: ""
        );
        await tester.Execute(request);
        await tester.Execute(request);
        var db = tester.Services.GetRequiredService<HubDbContext>();
        var unknownLocation = await getUnknownInstallLocation(db);
        var installations = await db.Installations.Retrieve().Where(inst => inst.LocationID != unknownLocation.ID).ToArrayAsync();
        var currentInstallations = installations.Where(inst => inst.IsCurrent).ToArray();
        Assert.That(currentInstallations.Length, Is.EqualTo(2), "Should not add duplicate current locations");
    }

    [Test]
    public async Task ShouldAddVersionInstallation()
    {
        Environment.SetEnvironmentVariable("DOTNET_ENVIRONMENT", "Production");
        var tester = await Setup();
        var hubApp = await tester.HubApp();
        var version = await hubApp.CurrentVersion();
        await tester.LoginAsAdmin();
        var request = new NewInstallationRequest
        (
            versionName: new AppVersionName("HubWebApp"),
            appKey: HubInfo.AppKey,
            qualifiedMachineName: "destination.example.com",
            domain: "",
            siteName: ""
        );
        await tester.Execute(request);
        var versionInstallation = await GetVersionInstallation(tester, version);
        Assert.That
        (
            versionInstallation?.Status,
            Is.EqualTo(InstallStatus.Values.InstallPending),
            "Should add version installation"
        );
    }

    [Test]
    public async Task ShouldAddDuplicateVersionInstallations()
    {
        Environment.SetEnvironmentVariable("DOTNET_ENVIRONMENT", "Production");
        var tester = await Setup();
        var hubApp = await tester.HubApp();
        var appVersion = await hubApp.CurrentVersion();
        await tester.LoginAsAdmin();
        var request = new NewInstallationRequest
        (
            versionName: new AppVersionName("HubWebApp"),
            appKey: HubInfo.AppKey,
            qualifiedMachineName: "destination.example.com",
            domain: "",
            siteName: ""
        );
        await tester.Execute(request);
        await tester.Execute(request);
        var db = tester.Services.GetRequiredService<HubDbContext>();
        var installations = await db.Installations.Retrieve().ToArrayAsync();
        var appVersionID = db.AppVersions.Retrieve()
            .Where(av => av.AppID == appVersion.App.ToModel().ID && av.VersionID == appVersion.Version.ToModel().ID)
            .Select(av => av.ID);
        var versionInstallations = installations
            .Where(inst => !inst.IsCurrent && appVersionID.Contains(inst.AppVersionID))
            .ToArray();
        Assert.That(versionInstallations.Length, Is.EqualTo(2), "Should not add duplicate version installation");
    }

    private static Task<InstallLocationEntity[]> GetInstallLocations(IHubActionTester tester)
    {
        var db = tester.Services.GetRequiredService<HubDbContext>();
        return db.InstallLocations.Retrieve()
            .Where(loc => loc.QualifiedMachineName != "unknown")
            .ToArrayAsync();
    }

    private static async Task<InstallationEntity> GetCurrentInstallation(IHubActionTester tester)
    {
        var db = tester.Services.GetRequiredService<HubDbContext>();
        var unknownLocation = await getUnknownInstallLocation(db);
        var installations = await db.Installations.Retrieve()
            .Where(inst => inst.LocationID != unknownLocation.ID)
            .ToArrayAsync();
        var currentInstallation = installations.First(inst => inst.IsCurrent);
        return currentInstallation;
    }

    private static Task<AppXtiVersionEntity> GetVersion(IHubActionTester tester, InstallationEntity installation)
    {
        var db = tester.Services.GetRequiredService<IHubDbContext>();
        return db.AppVersions.Retrieve()
            .Where(av => av.ID == installation.AppVersionID)
            .FirstAsync();
    }

    private static async Task<InstallationEntity> GetVersionInstallation(IHubActionTester tester, AppVersion appVersion)
    {
        var db = tester.Services.GetRequiredService<HubDbContext>();
        var appVersionID = db.AppVersions.Retrieve()
            .Where(av => av.AppID == appVersion.App.ToModel().ID && av.VersionID == appVersion.Version.ToModel().ID)
            .Select(av => av.ID);
        var unknownLocation = await getUnknownInstallLocation(db);
        var installations = await db.Installations.Retrieve().Where(inst => inst.LocationID != unknownLocation.ID).ToArrayAsync();
        var versionInstallation = installations
            .First(inst => !inst.IsCurrent && appVersionID.Contains(inst.AppVersionID));
        return versionInstallation;
    }

    private static Task<InstallLocationEntity> getUnknownInstallLocation(HubDbContext db) =>
        db.InstallLocations.Retrieve()
            .FirstAsync(loc => loc.QualifiedMachineName == "unknown");

    private async Task<HubActionTester<NewInstallationRequest, NewInstallationResult>> Setup()
    {
        var host = new HubTestHost();
        var sp = await host.Setup();
        return HubActionTester.Create(sp, hubApi => hubApi.Install.NewInstallation);
    }
}