using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_Hub;

namespace XTI_HubAppApi.AppUserMaintenance;

internal sealed class AllowAccessAction : AppAction<UserModifierKey, EmptyActionResult>
{
    private readonly AppFromPath appFromPath;
    private readonly AppFactory appFactory;

    public AllowAccessAction(AppFromPath appFromPath, AppFactory appFactory)
    {
        this.appFromPath = appFromPath;
        this.appFactory = appFactory;
    }

    public async Task<EmptyActionResult> Execute(UserModifierKey model)
    {
        var app = await appFromPath.Value();
        var denyAccessRole = await app.Role(AppRoleName.DenyAccess);
        var user = await appFactory.Users.User(model.UserID);
        var modifier = await app.Modifier(model.ModifierID);
        await user.Modifier(modifier).RemoveRole(denyAccessRole);
        return new EmptyActionResult();
    }
}