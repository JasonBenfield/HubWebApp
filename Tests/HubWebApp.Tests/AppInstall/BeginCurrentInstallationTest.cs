using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;
using XTI_App.Abstractions;
using XTI_Hub;
using XTI_HubAppApi;
using XTI_HubAppApi.AppInstall;
using XTI_HubDB.Entities;

namespace HubWebApp.Tests
{
    sealed class BeginCurrentInstallationTest
    {
        [Test]
        public async Task ShouldSetCurrentInstallationStatusToInstallStarted()
        {
            var tester = await setup();
            var hubApp = await tester.HubApp();
            var version = await hubApp.CurrentVersion();
            var adminUser = await tester.AdminUser();
            const string qualifiedMachineName = "machine.example.com";
            var newInstResult = await newInstallation(tester, new NewInstallationRequest
            {
                QualifiedMachineName = qualifiedMachineName,
                VersionID = version.ID.Value
            });
            var request = new BeginInstallationRequest
            {
                QualifiedMachineName = qualifiedMachineName,
                AppKey = HubInfo.AppKey,
                VersionKey = version.Key().Value
            };
            await tester.Execute(request, adminUser);
            var currentInstallation = await getInstallation(tester, newInstResult.CurrentInstallationID);
            Assert.That
            (
                InstallStatus.Values.Value(currentInstallation.Status),
                Is.EqualTo(InstallStatus.Values.InstallStarted),
                "Should set current installation status to install started"
            );
        }

        [Test]
        public async Task ShouldReturnInstallationID()
        {
            var tester = await setup();
            var hubApp = await tester.HubApp();
            var version = await hubApp.CurrentVersion();
            var adminUser = await tester.AdminUser();
            const string qualifiedMachineName = "machine.example.com";
            var newInstResult = await newInstallation(tester, new NewInstallationRequest
            {
                QualifiedMachineName = qualifiedMachineName,
                VersionID = version.ID.Value
            });
            var request = new BeginInstallationRequest
            {
                QualifiedMachineName = qualifiedMachineName,
                AppKey = HubInfo.AppKey,
                VersionKey = version.Key().Value
            };
            var installationID = await tester.Execute(request, adminUser);
            Assert.That
            (
                installationID,
                Is.EqualTo(newInstResult.CurrentInstallationID),
                "Should return current installation ID"
            );
        }

        [Test]
        public async Task ShouldSetCurrentInstallationVersion()
        {
            var tester = await setup();
            var hubApp = await tester.HubApp();
            var version = await hubApp.CurrentVersion();
            var adminUser = await tester.AdminUser();
            const string qualifiedMachineName = "machine.example.com";
            await newInstallation(tester, new NewInstallationRequest
            {
                QualifiedMachineName = qualifiedMachineName,
                VersionID = version.ID.Value
            });
            var factory = tester.Services.GetRequiredService<AppFactory>();
            var nextVersion = await factory.Apps.StartNewVersion
            (
                HubInfo.AppKey,
                AppVersionType.Values.Major,
                DateTimeOffset.Now
            );
            await nextVersion.Publishing();
            await nextVersion.Published();
            await newInstallation(tester, new NewInstallationRequest
            {
                QualifiedMachineName = qualifiedMachineName,
                VersionID = nextVersion.ID.Value
            });
            var request = new BeginInstallationRequest
            {
                QualifiedMachineName = qualifiedMachineName,
                AppKey = HubInfo.AppKey,
                VersionKey = nextVersion.Key().Value
            };
            var installationID = await tester.Execute(request, adminUser);
            var currentInstallation = await getInstallation(tester, installationID);
            Assert.That
            (
                currentInstallation.VersionID,
                Is.EqualTo(nextVersion.ID.Value),
                "Should set current installation version"
            );
        }

        private static Task<InstallationEntity> getInstallation(IHubActionTester tester, int installationID)
        {
            var db = tester.Services.GetService<IHubDbContext>();
            return db.Installations.Retrieve()
                .Where(inst => inst.ID == installationID)
                .FirstAsync();
        }

        private async Task<NewInstallationResult> newInstallation(IHubActionTester tester, NewInstallationRequest model)
        {
            var hubApi = tester.Services.GetService<HubAppApiFactory>().CreateForSuperUser();
            var result = await hubApi.Install.NewInstallation.Execute(model);
            return result.Data;
        }

        private async Task<HubActionTester<BeginInstallationRequest, int>> setup()
        {
            var host = new HubTestHost();
            var services = await host.Setup();
            return HubActionTester.Create(services, hubApi => hubApi.Install.BeginCurrentInstallation);
        }
    }
}
