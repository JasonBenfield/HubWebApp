using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Threading.Tasks;
using XTI_App.Abstractions;
using XTI_Core;
using XTI_Hub;
using XTI_HubAppApi;
using XTI_HubAppApi.AppRegistration;

namespace HubWebApp.Tests
{
    public sealed class BeginPublishTest
    {
        [Test]
        public async Task ShouldSetStatusToPublishing()
        {
            var tester = await setup();
            var apiFactory = tester.Services.GetService<HubAppApiFactory>();
            var hubApi = apiFactory.CreateForSuperUser();
            var version = await hubApi.AppRegistration.NewVersion.Invoke(new NewVersionRequest
            {
                AppKey = HubInfo.AppKey,
                VersionType = AppVersionType.Values.Patch
            });
            var adminUser = await tester.AdminUser();
            var versionKey = AppVersionKey.Parse(version.VersionKey);
            var request = new GetVersionRequest
            {
                AppKey = HubInfo.AppKey,
                VersionKey = versionKey
            };
            await tester.Execute(request, adminUser);
            version = await hubApi.AppRegistration.GetVersion.Invoke(new GetVersionRequest
            {
                AppKey = HubInfo.AppKey,
                VersionKey = versionKey
            });
            Assert.That(version.Status, Is.EqualTo(AppVersionStatus.Values.Publishing));
        }

        [Test]
        public async Task ShouldAssignVersionNumber_WhenPatchBeginsPublishing()
        {
            var tester = await setup();
            var app = await tester.HubApp();
            var clock = tester.Services.GetService<Clock>();
            var patch = await app.StartNewPatch(clock.Now());
            await patch.Publishing();
            Assert.That(patch.Version().Major, Is.EqualTo(1), "Should assign version number for new patch");
            Assert.That(patch.Version().Minor, Is.EqualTo(0), "Should assign version number for new patch");
            Assert.That(patch.Version().Build, Is.EqualTo(1), "Should assign version number for new patch");
        }

        [Test]
        public async Task ShouldAssignVersionNumber_WhenMinorVersionBeginsPublishing()
        {
            var tester = await setup();
            var app = await tester.HubApp();
            var clock = tester.Services.GetService<Clock>();
            var minorVersion = await app.StartNewMinorVersion(clock.Now());
            await minorVersion.Publishing();
            Assert.That(minorVersion.Version().Major, Is.EqualTo(1), "Should assign version number for new minor version");
            Assert.That(minorVersion.Version().Minor, Is.EqualTo(1), "Should assign version number for new minor version");
            Assert.That(minorVersion.Version().Build, Is.EqualTo(0), "Should assign version number for new minor version");
        }

        [Test]
        public async Task ShouldAssignVersionNumber_WhenMajorVersionBeginsPublishing()
        {
            var tester = await setup();
            var app = await tester.HubApp();
            var clock = tester.Services.GetService<Clock>();
            var majorVersion = await app.StartNewMajorVersion(clock.Now());
            await majorVersion.Publishing();
            Assert.That(majorVersion.Version().Major, Is.EqualTo(2), "Should assign version number for new major version");
            Assert.That(majorVersion.Version().Minor, Is.EqualTo(0), "Should assign version number for new major version");
            Assert.That(majorVersion.Version().Build, Is.EqualTo(0), "Should assign version number for new major version");
        }

        [Test]
        public async Task ShouldIncrementPatchOfCurrent_WhenPatchBeginsPublishing()
        {
            var tester = await setup();
            var app = await tester.HubApp();
            var clock = tester.Services.GetService<Clock>();
            var originalCurrent = await app.StartNewPatch(clock.Now());
            await originalCurrent.Publishing();
            await originalCurrent.Published();
            var patch = await app.StartNewPatch(clock.Now());
            await patch.Publishing();
            Assert.That(patch.Version().Major, Is.EqualTo(1), "Should increment patch of current version");
            Assert.That(patch.Version().Minor, Is.EqualTo(0), "Should increment patch of current version");
            Assert.That(patch.Version().Build, Is.EqualTo(2), "Should increment patch of current version");
        }

        [Test]
        public async Task ShouldIncrementMinorVersionOfCurrent_WhenMinorVersionBeginsPublishing()
        {
            var tester = await setup();
            var app = await tester.HubApp();
            var clock = tester.Services.GetService<Clock>();
            var originalCurrent = await app.StartNewMinorVersion(clock.Now());
            await originalCurrent.Publishing();
            await originalCurrent.Published();
            var minorVersion = await app.StartNewMinorVersion(clock.Now());
            await minorVersion.Publishing();
            Assert.That(minorVersion.Version().Major, Is.EqualTo(1), "Should increment minor of current version");
            Assert.That(minorVersion.Version().Minor, Is.EqualTo(2), "Should increment minor of current version");
            Assert.That(minorVersion.Version().Build, Is.EqualTo(0), "Should increment minor of current version");
        }

        [Test]
        public async Task ShouldIncrementMajorVersionOfCurrent_WhenMajorVersionBeginsPublishing()
        {
            var tester = await setup();
            var app = await tester.HubApp();
            var clock = tester.Services.GetService<Clock>();
            var originalCurrent = await app.StartNewMajorVersion(clock.Now());
            await originalCurrent.Publishing();
            await originalCurrent.Published();
            var majorVersion = await app.StartNewMajorVersion(clock.Now());
            await majorVersion.Publishing();
            Assert.That(majorVersion.Version().Major, Is.EqualTo(3), "Should increment major of current version");
            Assert.That(majorVersion.Version().Minor, Is.EqualTo(0), "Should increment major of current version");
            Assert.That(majorVersion.Version().Build, Is.EqualTo(0), "Should increment major of current version");
        }

        [Test]
        public async Task ShouldRetainMajorVersion_WhenMinorVersionBeginsPublishing()
        {
            var tester = await setup();
            var app = await tester.HubApp();
            var clock = tester.Services.GetService<Clock>();
            var majorVersion = await app.StartNewMajorVersion(clock.Now());
            await majorVersion.Publishing();
            await majorVersion.Published();
            var minorVersion = await app.StartNewMinorVersion(clock.Now());
            await minorVersion.Publishing();
            await minorVersion.Published();
            Assert.That(minorVersion.Version().Major, Is.EqualTo(2), "Should retain major version from the previous current");
            Assert.That(minorVersion.Version().Minor, Is.EqualTo(1), "Should increment minor version");
            Assert.That(minorVersion.Version().Build, Is.EqualTo(0), "Should reset patch");
        }

        [Test]
        public async Task ShouldRetainMajorAndMinorVersion_WhenPatchBeginsPublishing()
        {
            var tester = await setup();
            var app = await tester.HubApp();
            var clock = tester.Services.GetService<Clock>();
            var majorVersion = await app.StartNewMajorVersion(clock.Now());
            await majorVersion.Publishing();
            await majorVersion.Published();
            var minorVersion = await app.StartNewMinorVersion(clock.Now());
            await minorVersion.Publishing();
            await minorVersion.Published();
            var patch = await app.StartNewPatch(clock.Now());
            await patch.Publishing();
            Assert.That(patch.Version().Major, Is.EqualTo(2), "Should retain major version from the previous current");
            Assert.That(patch.Version().Minor, Is.EqualTo(1), "Should retain minor version from the previous current");
            Assert.That(patch.Version().Build, Is.EqualTo(1), "Should increment patch");
        }

        [Test]
        public async Task ShouldResetPatch_WhenMinorVersionBeginsPublishing()
        {
            var tester = await setup();
            var app = await tester.HubApp();
            var clock = tester.Services.GetService<Clock>();
            var patch = await app.StartNewPatch(clock.Now());
            await patch.Publishing();
            await patch.Published();
            var minorVersion = await app.StartNewMinorVersion(clock.Now());
            await minorVersion.Publishing();
            Assert.That(minorVersion.Version().Major, Is.EqualTo(1), "Should reset patch when minor version is publishing");
            Assert.That(minorVersion.Version().Minor, Is.EqualTo(1), "Should reset patch when minor version is publishing");
            Assert.That(minorVersion.Version().Build, Is.EqualTo(0), "Should reset patch when minor version is publishing");
        }

        [Test]
        public async Task ShouldResetPatchAndMinorVersion_WhenMajorVersionBeginsPublishing()
        {
            var tester = await setup();
            var app = await tester.HubApp();
            var clock = tester.Services.GetService<Clock>();
            var patch = await app.StartNewPatch(clock.Now());
            await patch.Publishing();
            await patch.Published();
            var minorVersion = await app.StartNewMinorVersion(clock.Now());
            await minorVersion.Publishing();
            await minorVersion.Published();
            var majorVersion = await app.StartNewMajorVersion(clock.Now());
            await majorVersion.Publishing();
            Assert.That(majorVersion.Version().Major, Is.EqualTo(2), "Should reset minor version and patch when major version is publishing");
            Assert.That(majorVersion.Version().Minor, Is.EqualTo(0), "Should reset minor version and patch when major version is publishing");
            Assert.That(majorVersion.Version().Build, Is.EqualTo(0), "Should reset minor version and patch when major version is publishing");
        }

        private async Task<HubActionTester<GetVersionRequest, AppVersionModel>> setup()
        {
            var host = new HubTestHost();
            var services = await host.Setup();
            return HubActionTester.Create(services, hubApi => hubApi.AppRegistration.BeginPublish);
        }
    }
}
