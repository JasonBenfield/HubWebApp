﻿using XTI_Hub.Abstractions;

namespace HubWebApp.IntegrationTests;

internal sealed class GetUsersWithAnyRoleTest
{
    [Test]
    public async Task ShouldGetUsersWithAnyRole()
    {
        var tester = await Setup();
        var currentUserName = tester.Services.GetRequiredService<FakeCurrentUserName>();
        currentUserName.SetUserName(new SystemUserName(HubInfo.AppKey, Environment.MachineName).UserName);
        var users = await tester.Execute
        (
            new SystemGetUsersWithAnyRoleRequest
            (
                0,
                HubInfo.ModCategories.Apps,
                new ModifierKey("Scheduled Jobs Web App"),
                HubInfo.Roles.PermanentLog
            )
        );
        users.WriteToConsole();
    }

    private async Task<HubActionTester<SystemGetUsersWithAnyRoleRequest, AppUserModel[]>> Setup(string envName = "Development")
    {
        var host = new HubTestHost();
        var sp = await host.Setup(envName);
        return  HubActionTester.Create(sp, hubApi => hubApi.System.GetUsersWithAnyRole);
    }
}
