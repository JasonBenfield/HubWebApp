﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XTI_App.Api;
using XTI_HubAppApi;
using XTI_HubAppApi.AppInstall;
using Microsoft.Extensions.DependencyInjection;
using XTI_HubDB.Entities;
using Microsoft.EntityFrameworkCore;
using XTI_Hub;

namespace HubWebApp.Tests
{
    sealed class InstalledTest
    {
        [Test]
        public async Task ShouldSetCurrentInstallationStatusToInstalled()
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
            await startCurrentInstallation(tester, new BeginInstallationRequest
            {
                QualifiedMachineName = qualifiedMachineName,
                AppKey = hubApp.Key(),
                VersionKey = version.Key()
            });
            await tester.Execute
            (
                new InstalledRequest(newInstResult.CurrentInstallationID),
                adminUser
            );
            var currentInstallation = await getInstallation(tester, newInstResult.CurrentInstallationID);
            Assert.That
            (
                InstallStatus.Values.Value(currentInstallation?.Status ?? 0),
                Is.EqualTo(InstallStatus.Values.Installed),
                "Should set current installation status to Installed"
            );
        }

        [Test]
        public async Task ShouldSetVersionInstallationStatusToInstalled()
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
            await startVersionInstallation(tester, new BeginInstallationRequest
            {
                QualifiedMachineName = qualifiedMachineName,
                AppKey = hubApp.Key(),
                VersionKey = version.Key()
            });
            await tester.Execute
            (
                new InstalledRequest(newInstResult.VersionInstallationID),
                adminUser
            );
            var versionInstallation = await getInstallation(tester, newInstResult.VersionInstallationID);
            Assert.That
            (
                InstallStatus.Values.Value(versionInstallation?.Status ?? 0),
                Is.EqualTo(InstallStatus.Values.Installed),
                "Should set version installation status to Installed"
            );
        }

        private async Task<HubActionTester<InstalledRequest, EmptyActionResult>> setup()
        {
            var host = new HubTestHost();
            var services = await host.Setup();
            return HubActionTester.Create(services, hubApi => hubApi.Install.Installed);
        }

        private async Task<NewInstallationResult> newInstallation(IHubActionTester tester, NewInstallationRequest model)
        {
            var hubApi = tester.Services.GetService<HubAppApiFactory>().CreateForSuperUser();
            var result = await hubApi.Install.NewInstallation.Execute(model);
            return result.Data;
        }

        private async Task<int> startCurrentInstallation(IHubActionTester tester, BeginInstallationRequest model)
        {
            var hubApi = tester.Services.GetService<HubAppApiFactory>().CreateForSuperUser();
            var result = await hubApi.Install.BeginCurrentInstallation.Execute(model);
            return result.Data;
        }

        private async Task<int> startVersionInstallation(IHubActionTester tester, BeginInstallationRequest model)
        {
            var hubApi = tester.Services.GetService<HubAppApiFactory>().CreateForSuperUser();
            var result = await hubApi.Install.BeginVersionInstallation.Execute(model);
            return result.Data;
        }

        private static Task<InstallationEntity> getInstallation(IHubActionTester tester, int installationID)
        {
            var db = tester.Services.GetService<IHubDbContext>();
            return db.Installations.Retrieve()
                .Where(inst => inst.ID == installationID)
                .FirstAsync();
        }

    }
}
