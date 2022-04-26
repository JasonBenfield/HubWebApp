﻿using XTI_App.Api;
using XTI_Hub;

namespace XTI_HubAppApi.AppUserMaintenance;

internal sealed class UnassignRoleAction : AppAction<UserRoleRequest, EmptyActionResult>
{
    private readonly AppFromPath appFromPath;
    private readonly AppFactory factory;
    private readonly ICachedUserContext userContext;

    public UnassignRoleAction(AppFromPath appFromPath, AppFactory factory, ICachedUserContext userContext)
    {
        this.appFromPath = appFromPath;
        this.factory = factory;
        this.userContext = userContext;
    }

    public async Task<EmptyActionResult> Execute(UserRoleRequest model)
    {
        var app = await appFromPath.Value();
        var user = await factory.Users.User(model.UserID);
        Modifier modifier;
        if (model.ModifierID > 0)
        {
            modifier = await app.Modifier(model.ModifierID);
        }
        else
        {
            modifier = await app.DefaultModifier();
        }
        var role = await app.Role(model.RoleID);
        await user.Modifier(modifier).UnassignRole(role);
        userContext.ClearCache(user.UserName());
        return new EmptyActionResult();
    }
}