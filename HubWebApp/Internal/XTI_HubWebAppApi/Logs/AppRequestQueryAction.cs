﻿using Microsoft.AspNetCore.OData.Query;
using XTI_HubDB.Entities;
using XTI_ODataQuery.Api;

namespace XTI_HubWebAppApi.Logs;

internal sealed class AppRequestQueryAction : QueryAction<AppRequestQueryRequest, ExpandedRequest>
{
    private readonly CurrentAppUser currentUser;
    private readonly IHubDbContext db;

    public AppRequestQueryAction(CurrentAppUser currentUser, IHubDbContext db)
    {
        this.currentUser = currentUser;
        this.db = db;
    }

    public async Task<IQueryable<ExpandedRequest>> Execute(ODataQueryOptions<ExpandedRequest> options, AppRequestQueryRequest model)
    {
        var userGroupPermissions = await currentUser.GetUserGroupPermissions();
        var userGroupIDs = userGroupPermissions
            .Where(p => p.CanView)
            .Select(p => p.UserGroup.ToModel().ID)
            .ToArray();
        var appPermissions = await currentUser.GetAppPermissions();
        var appIDs = appPermissions
            .Where(p => p.CanView)
            .Select(p => p.App.ToModel().ID)
            .ToArray();
        var query = db.ExpandedRequests.Retrieve()
            .Where(r => userGroupIDs.Contains(r.UserGroupID) && appIDs.Contains(r.AppID));
        if (model.SessionID.HasValue)
        {
            query = query.Where(r => r.SessionID == model.SessionID.Value);
        }
        if (model.InstallationID.HasValue)
        {
            query = query.Where(r => r.InstallationID == model.InstallationID.Value);
        }
        return query;
    }
}
