using NUnit.Framework;
using System.Threading.Tasks;
using XTI_WebApp.Api;

namespace HubWebApp.Tests
{
    public sealed class RedirectToAppTest
    {
        [Test]
        public async Task ShouldAddModifierToUrl()
        {
            var tester = await setup();
            var adminUser = await tester.AdminUser();
            var hubApp = await tester.HubApp();
            var result = await tester.Execute(hubApp.ID.Value, adminUser);
            var hubAppModifier = await tester.HubAppModifier();
            Assert.That
            (
                result.Url,
                Is.EqualTo($"/Hub/Current/App/Index/{hubAppModifier.ModKey().Value}").IgnoreCase,
                "Should add app modifier when redirecting to app"
            );
        }

        private async Task<HubActionTester<int, WebRedirectResult>> setup()
        {
            var host = new HubTestHost();
            var services = await host.Setup();
            return HubActionTester.Create(services, hubApi => hubApi.Apps.RedirectToApp);
        }
    }
}
