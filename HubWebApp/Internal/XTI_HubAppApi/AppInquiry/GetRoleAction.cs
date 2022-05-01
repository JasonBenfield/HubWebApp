﻿namespace XTI_HubAppApi.AppInquiry;

public sealed class GetRoleAction : AppAction<string, AppRoleModel>
{
    private readonly AppFromPath appFromPath;

    public GetRoleAction(AppFromPath appFromPath)
    {
        this.appFromPath = appFromPath;
    }

    public async Task<AppRoleModel> Execute(string roleName)
    {
        var app = await appFromPath.Value();
        var role = await app.Role(new AppRoleName(roleName));
        var roleModel = role.ToModel();
        return roleModel;
    }
}