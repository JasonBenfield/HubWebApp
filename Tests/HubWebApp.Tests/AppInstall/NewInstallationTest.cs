using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using XTI_Hub;
using XTI_HubAppApi.AppInstall;
using XTI_HubDB.EF;
using XTI_HubDB.Entities;

namespace HubWebApp.Tests
{
    sealed class NewInstallationTest
    {
        [Test]
        public async Task ShouldAddInstallLocation()
        {
            var tester = await setup();
            var hubApp = await tester.HubApp();
            var version = await hubApp.CurrentVersion();
            var adminUser = await tester.AdminUser();
            var request = new NewInstallationRequest
            {
                VersionID = version.ID.Value,
                QualifiedMachineName = "destination.example.com"
            };
            await tester.Execute(request, adminUser);
            var installLocations = await getInstallLocations(tester);
            Assert.That(installLocations.Length, Is.EqualTo(1), "Should add install location");
            Assert.That(installLocations[0].QualifiedMachineName, Is.EqualTo(request.QualifiedMachineName), "Should add install location");
        }

        [Test]
        public async Task ShouldNotAddInstallLocationWithDuplicateQualifiedMachineName()
        {
            var tester = await setup();
            var hubApp = await tester.HubApp();
            var version = await hubApp.CurrentVersion();
            var adminUser = await tester.AdminUser();
            var request = new NewInstallationRequest
            {
                VersionID = version.ID.Value,
                QualifiedMachineName = "destination.example.com"
            };
            await tester.Execute(request, adminUser);
            await tester.Execute(request, adminUser);
            var installLocations = await getInstallLocations(tester);
            Assert.That(installLocations.Length, Is.EqualTo(1), "Should not add duplicate install location");
        }

        [Test]
        public async Task ShouldAddCurrentInstallation()
        {
            var tester = await setup();
            var hubApp = await tester.HubApp();
            var version = await hubApp.CurrentVersion();
            var adminUser = await tester.AdminUser();
            var request = new NewInstallationRequest
            {
                VersionID = version.ID.Value,
                QualifiedMachineName = "destination.example.com"
            };
            await tester.Execute(request, adminUser);
            var currentInstallation = await getCurrentInstallation(tester);
            Assert.That(currentInstallation?.VersionID, Is.EqualTo(version.ID.Value), "Should add current installation");
            var installLocations = await getInstallLocations(tester);
            Assert.That(currentInstallation?.LocationID, Is.EqualTo(installLocations[0].ID));
        }

        [Test]
        public async Task ShouldAddCurrentInstallationWithInstallPendingStatus()
        {
            var tester = await setup();
            var hubApp = await tester.HubApp();
            var version = await hubApp.CurrentVersion();
            var adminUser = await tester.AdminUser();
            var request = new NewInstallationRequest
            {
                VersionID = version.ID.Value,
                QualifiedMachineName = "destination.example.com"
            };
            await tester.Execute(request, adminUser);
            var currentInstallation = await getCurrentInstallation(tester);
            Assert.That(currentInstallation?.Status, Is.EqualTo(InstallStatus.Values.InstallPending.Value), "Should add current installation with install pending status");
        }

        [Test]
        public async Task ShouldNotAddDuplicateCurrentInstallation()
        {
            var tester = await setup();
            var hubApp = await tester.HubApp();
            var version = await hubApp.CurrentVersion();
            var adminUser = await tester.AdminUser();
            var request = new NewInstallationRequest
            {
                VersionID = version.ID.Value,
                QualifiedMachineName = "destination.example.com"
            };
            await tester.Execute(request, adminUser);
            await tester.Execute(request, adminUser);
            var db = tester.Services.GetService<HubDbContext>();
            var installations = await db.Installations.Retrieve().ToArrayAsync();
            var currentInstallations = installations.Where(inst => inst.IsCurrent).ToArray();
            Assert.That(currentInstallations.Length, Is.EqualTo(1), "Should not add duplicate current locations");
        }

        [Test]
        public async Task ShouldAddVersionInstallation()
        {
            var tester = await setup();
            var hubApp = await tester.HubApp();
            var version = await hubApp.CurrentVersion();
            var adminUser = await tester.AdminUser();
            var request = new NewInstallationRequest
            {
                VersionID = version.ID.Value,
                QualifiedMachineName = "destination.example.com"
            };
            await tester.Execute(request, adminUser);
            var versionInstallation = await getVersionInstallation(tester, version);
            Assert.That
            (
                versionInstallation?.Status,
                Is.EqualTo(InstallStatus.Values.InstallPending),
                "Should add version installation"
            );
        }

        [Test]
        public async Task ShouldNotAddDuplicateVersionInstallations()
        {
            var tester = await setup();
            var hubApp = await tester.HubApp();
            var version = await hubApp.CurrentVersion();
            var adminUser = await tester.AdminUser();
            var request = new NewInstallationRequest
            {
                VersionID = version.ID.Value,
                QualifiedMachineName = "destination.example.com"
            };
            await tester.Execute(request, adminUser);
            await tester.Execute(request, adminUser);
            var db = tester.Services.GetService<HubDbContext>();
            var installations = await db.Installations.Retrieve().ToArrayAsync();
            var versionInstallations = installations
                .Where(inst => !inst.IsCurrent && inst.VersionID == version.ID.Value)
                .ToArray();
            Assert.That(versionInstallations.Length, Is.EqualTo(1), "Should not add duplicate version installation");
        }

        [Test]
        public async Task ShouldSetExistingVersionInstallationStatusToInstallPending()
        {
            var tester = await setup();
            var hubApp = await tester.HubApp();
            var version = await hubApp.CurrentVersion();
            var adminUser = await tester.AdminUser();
            var request = new NewInstallationRequest
            {
                VersionID = version.ID.Value,
                QualifiedMachineName = "destination.example.com"
            };
            await tester.Execute(request, adminUser);
            var db = tester.Services.GetService<HubDbContext>();
            var versionInstallation = await getVersionInstallation(tester, version);
            await db.Installations.Update
            (
                versionInstallation,
                inst => inst.Status = InstallStatus.Values.InstallStarted.Value
            );
            await tester.Execute(request, adminUser);
            versionInstallation = await getVersionInstallation(tester, version);
            Assert.That
            (
                versionInstallation.Status,
                Is.EqualTo(InstallStatus.Values.InstallPending.Value),
                "Should set the status of an existing version installation to Install Pending"
            );
        }

        private static Task<InstallLocationEntity[]> getInstallLocations(IHubActionTester tester)
        {
            var db = tester.Services.GetService<HubDbContext>();
            return db.InstallLocations.Retrieve().ToArrayAsync();
        }

        private static async Task<InstallationEntity> getCurrentInstallation(IHubActionTester tester)
        {
            var db = tester.Services.GetService<HubDbContext>();
            var installations = await db.Installations.Retrieve().ToArrayAsync();
            var currentInstallation = installations.FirstOrDefault(inst => inst.IsCurrent);
            return currentInstallation;
        }

        private static async Task<InstallationEntity> getVersionInstallation(IHubActionTester tester, AppVersion appVersion)
        {
            var db = tester.Services.GetService<HubDbContext>();
            var installations = await db.Installations.Retrieve().ToArrayAsync();
            var versionInstallation = installations
                .FirstOrDefault(inst => !inst.IsCurrent && inst.VersionID == appVersion.ID.Value);
            return versionInstallation;
        }

        private async Task<HubActionTester<NewInstallationRequest, NewInstallationResult>> setup()
        {
            var host = new HubTestHost();
            var services = await host.Setup();
            return HubActionTester.Create(services, hubApi => hubApi.Install.NewInstallation);
        }
    }
}
