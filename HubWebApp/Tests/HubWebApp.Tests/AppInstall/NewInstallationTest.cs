using Microsoft.EntityFrameworkCore;
using XTI_Hub.Abstractions;
using XTI_HubWebAppApi.AppInstall;
using XTI_HubDB.EF;
using XTI_HubDB.Entities;

namespace HubWebApp.Tests;

sealed class NewInstallationTest
{
    [Test]
    public async Task ShouldRequireVersionName()
    {
        var tester = await setup();
        tester.LoginAsAdmin();
        var request = new NewInstallationRequest
        {
            AppKey = HubInfo.AppKey,
            QualifiedMachineName = "destination.example.com"
        };
        var ex = Assert.ThrowsAsync<ValidationFailedException>(() => tester.Execute(request));
        Assert.That(ex?.Errors.Select(e => e.Message), Is.EquivalentTo(new[] { AppErrors.VersionNameIsRequired }));
    }

    [Test]
    public async Task ShouldRequireMachineName()
    {
        var tester = await setup();
        tester.LoginAsAdmin();
        var request = new NewInstallationRequest
        {
            VersionName = new AppVersionName("HubWebApp"),
            AppKey = HubInfo.AppKey,
            QualifiedMachineName = ""
        };
        var ex = Assert.ThrowsAsync<ValidationFailedException>(() => tester.Execute(request));
        Assert.That(ex?.Errors.Select(e => e.Message), Is.EquivalentTo(new[] { AppErrors.MachineNameIsRequired }));
    }

    [Test]
    public async Task ShouldAddInstallLocation()
    {
        var tester = await setup();
        tester.LoginAsAdmin();
        var request = new NewInstallationRequest
        {
            VersionName = new AppVersionName("HubWebApp"),
            AppKey = HubInfo.AppKey,
            QualifiedMachineName = "destination.example.com"
        };
        await tester.Execute(request);
        var installLocations = await getInstallLocations(tester);
        Assert.That(installLocations.Length, Is.EqualTo(1), "Should add install location");
        Assert.That(installLocations[0].QualifiedMachineName, Is.EqualTo(request.QualifiedMachineName), "Should add install location");
    }

    [Test]
    public async Task ShouldNotAddInstallLocationWithDuplicateQualifiedMachineName()
    {
        var tester = await setup();
        tester.LoginAsAdmin();
        var request = new NewInstallationRequest
        {
            VersionName = new AppVersionName("HubWebApp"),
            AppKey = HubInfo.AppKey,
            QualifiedMachineName = "destination.example.com"
        };
        await tester.Execute(request);
        await tester.Execute(request);
        var installLocations = await getInstallLocations(tester);
        Assert.That(installLocations.Length, Is.EqualTo(1), "Should not add duplicate install location");
    }

    [Test]
    public async Task ShouldAddCurrentInstallation()
    {
        var tester = await setup();
        var hubApp = await tester.HubApp();
        var version = await hubApp.CurrentVersion();
        tester.LoginAsAdmin();
        var request = new NewInstallationRequest
        {
            VersionName = new AppVersionName("HubWebApp"),
            AppKey = HubInfo.AppKey,
            QualifiedMachineName = "destination.example.com"
        };
        await tester.Execute(request);
        var currentInstallation = await getCurrentInstallation(tester);
        var installationVersion = await getVersion(tester, currentInstallation);
        Assert.That(currentInstallation?.AppVersionID, Is.EqualTo(installationVersion.ID), "Should add current installation");
        var installLocations = await getInstallLocations(tester);
        Assert.That(currentInstallation?.LocationID, Is.EqualTo(installLocations[0].ID));
    }

    [Test]
    public async Task ShouldAddCurrentInstallationWithInstallPendingStatus()
    {
        var tester = await setup();
        tester.LoginAsAdmin();
        var request = new NewInstallationRequest
        {
            VersionName = new AppVersionName("HubWebApp"),
            AppKey = HubInfo.AppKey,
            QualifiedMachineName = "destination.example.com"
        };
        await tester.Execute(request);
        var currentInstallation = await getCurrentInstallation(tester);
        Assert.That(currentInstallation?.Status, Is.EqualTo(InstallStatus.Values.InstallPending.Value), "Should add current installation with install pending status");
    }

    [Test]
    public async Task ShouldAddDuplicateCurrentInstallation()
    {
        var tester = await setup();
        var hubApp = await tester.HubApp();
        var version = await hubApp.CurrentVersion();
        tester.LoginAsAdmin();
        var request = new NewInstallationRequest
        {
            VersionName = new AppVersionName("HubWebApp"),
            AppKey = HubInfo.AppKey,
            QualifiedMachineName = "destination.example.com"
        };
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
        var tester = await setup();
        var hubApp = await tester.HubApp();
        var version = await hubApp.CurrentVersion();
        tester.LoginAsAdmin();
        var request = new NewInstallationRequest
        {
            VersionName = new AppVersionName("HubWebApp"),
            AppKey = HubInfo.AppKey,
            QualifiedMachineName = "destination.example.com"
        };
        await tester.Execute(request);
        var versionInstallation = await getVersionInstallation(tester, version);
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
        var tester = await setup();
        var hubApp = await tester.HubApp();
        var appVersion = await hubApp.CurrentVersion();
        tester.LoginAsAdmin();
        var request = new NewInstallationRequest
        {
            VersionName = new AppVersionName("HubWebApp"),
            AppKey = HubInfo.AppKey,
            QualifiedMachineName = "destination.example.com"
        };
        await tester.Execute(request);
        await tester.Execute(request);
        var db = tester.Services.GetRequiredService<HubDbContext>();
        var installations = await db.Installations.Retrieve().ToArrayAsync();
        var appVersionID = db.AppVersions.Retrieve()
            .Where(av => av.AppID == appVersion.ToAppModel().ID && av.VersionID == appVersion.ToVersionModel().ID)
            .Select(av => av.ID);
        var versionInstallations = installations
            .Where(inst => !inst.IsCurrent && appVersionID.Contains(inst.AppVersionID))
            .ToArray();
        Assert.That(versionInstallations.Length, Is.EqualTo(2), "Should not add duplicate version installation");
    }

    private static Task<InstallLocationEntity[]> getInstallLocations(IHubActionTester tester)
    {
        var db = tester.Services.GetRequiredService<HubDbContext>();
        return db.InstallLocations.Retrieve().Where(loc => loc.QualifiedMachineName != "unknown").ToArrayAsync();
    }

    private static async Task<InstallationEntity> getCurrentInstallation(IHubActionTester tester)
    {
        var db = tester.Services.GetRequiredService<HubDbContext>();
        var unknownLocation = await getUnknownInstallLocation(db);
        var installations = await db.Installations.Retrieve().Where(inst => inst.LocationID != unknownLocation.ID).ToArrayAsync();
        var currentInstallation = installations.First(inst => inst.IsCurrent);
        return currentInstallation;
    }

    private static Task<AppXtiVersionEntity> getVersion(IHubActionTester tester, InstallationEntity installation)
    {
        var db = tester.Services.GetRequiredService<IHubDbContext>();
        return db.AppVersions.Retrieve()
            .Where(av => av.ID == installation.AppVersionID)
            .FirstAsync();
    }

    private static async Task<InstallationEntity> getVersionInstallation(IHubActionTester tester, AppVersion appVersion)
    {
        var db = tester.Services.GetRequiredService<HubDbContext>();
        var appVersionID = db.AppVersions.Retrieve()
            .Where(av => av.AppID == appVersion.ToAppModel().ID && av.VersionID == appVersion.ToVersionModel().ID)
            .Select(av => av.ID);
        var unknownLocation = await getUnknownInstallLocation(db);
        var installations = await db.Installations.Retrieve().Where(inst => inst.LocationID != unknownLocation.ID).ToArrayAsync();
        var versionInstallation = installations
            .First(inst => !inst.IsCurrent && appVersionID.Contains(inst.AppVersionID));
        return versionInstallation;
    }

    private static async Task<InstallLocationEntity> getUnknownInstallLocation(HubDbContext db)
    {
        return await db.InstallLocations.Retrieve().FirstAsync(loc => loc.QualifiedMachineName == "unknown");
    }

    private async Task<HubActionTester<NewInstallationRequest, NewInstallationResult>> setup()
    {
        var host = new HubTestHost();
        var sp = await host.Setup();
        return HubActionTester.Create(sp, hubApi => hubApi.Install.NewInstallation);
    }
}