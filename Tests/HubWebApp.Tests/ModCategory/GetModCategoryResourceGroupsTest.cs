using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XTI_App.Abstractions;
using XTI_Hub;
using XTI_HubAppApi;

namespace HubWebApp.Tests
{
    sealed class GetModCategoryResourceGroupsTest
    {
        [Test]
        public async Task ShouldGetResourceGroups()
        {
            var tester = await setup();
            var app = await tester.HubApp();
            var appsModCategory = await app.ModCategory(HubInfo.ModCategories.Apps);
            var hubAppModifier = await tester.HubAppModifier();
            var adminUser = await tester.AdminUser();
            var resourceGroups = await tester.Execute(appsModCategory.ID.Value, adminUser, hubAppModifier.ModKey());
            Assert.That
            (
                resourceGroups.Select(g => g.Name),
                Has.One.EqualTo(new ResourceGroupName("ModCategory"))
            );
        }

        private async Task<HubActionTester<int, ResourceGroupModel[]>> setup()
        {
            var host = new HubTestHost();
            var services = await host.Setup();
            return HubActionTester.Create(services, hubApi => hubApi.ModCategory.GetResourceGroups);
        }
    }
}
