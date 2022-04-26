﻿using Microsoft.EntityFrameworkCore;
using XTI_Core;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class AppEventRepository
{
    private readonly AppFactory factory;

    public AppEventRepository(AppFactory factory)
    {
        this.factory = factory;
    }

    public async Task<AppEvent> LogEvent(AppRequest request, string eventKey, DateTimeOffset timeOccurred, AppEventSeverity severity, string caption, string message, string detail, int actualCount)
    {
        var record = await factory.DB.Events.Retrieve().FirstOrDefaultAsync(evt => evt.EventKey == eventKey);
        if (record == null)
        {
            record = new AppEventEntity
            {
                RequestID = request.ID.Value,
                EventKey = eventKey,
                TimeOccurred = timeOccurred,
                Severity = severity.Value,
                Caption = caption,
                Message = message,
                Detail = detail,
                ActualCount = actualCount
            };
            await factory.DB.Events.Create(record);
        }
        else
        {
            await factory.DB.Events.Update
            (
                record,
                evt =>
                {
                    evt.RequestID = request.ID.Value;
                    evt.EventKey = eventKey;
                    evt.TimeOccurred = timeOccurred;
                    evt.Severity = severity.Value;
                    evt.Caption = caption;
                    evt.Message = message;
                    evt.Detail = detail;
                    evt.ActualCount = actualCount;
                }
            );
        }
        return factory.CreateEvent(record);
    }

    internal Task<AppEvent[]> RetrieveByRequest(AppRequest request)
    {
        return factory.DB.Events.Retrieve()
            .Where(e => e.RequestID == request.ID.Value)
            .Select(e => factory.CreateEvent(e))
            .ToArrayAsync();
    }

    internal Task<AppEvent[]> MostRecentErrorsForVersion(App app, XtiVersion version, int howMany)
    {
        var appVersionID = factory.Versions.QueryAppVersionID(app, version);
        var requestIDs = factory.DB
            .Requests
            .Retrieve()
            .Join
            (
                factory.DB.Resources
                    .Retrieve(),
                req => req.ResourceID,
                res => res.ID,
                (req, res) => new { RequestID = req.ID, GroupID = res.GroupID }
            )
            .Join
            (
                factory.DB.ResourceGroups
                    .Retrieve(),
                res => res.GroupID,
                rg => rg.ID,
                (res, rg) => new { RequestID = res.RequestID, AppVersionID = rg.AppVersionID }
            )
            .Where(rg => appVersionID.Contains(rg.AppVersionID))
            .Select(rg => rg.RequestID);
        return mostRecentErrors(howMany, requestIDs);
    }

    internal Task<AppEvent[]> MostRecentErrorsForResourceGroup(ResourceGroup group, int howMany)
    {
        var requestIDs = factory.DB
            .Requests
            .Retrieve()
            .Join
            (
                factory.DB
                    .Resources
                    .Retrieve(),
                req => req.ResourceID,
                res => res.ID,
                (req, res) => new { RequestID = req.ID, GroupID = res.GroupID }
            )
            .Where(rg => rg.GroupID == group.ID.Value)
            .Select(rg => rg.RequestID);
        return mostRecentErrors(howMany, requestIDs);
    }

    internal Task<AppEvent[]> MostRecentErrorsForResource(Resource resource, int howMany)
    {
        var requestIDs = factory.DB
            .Requests
            .Retrieve()
            .Where(r => r.ResourceID == resource.ID.Value)
            .Select(r => r.ResourceID);
        return mostRecentErrors(howMany, requestIDs);
    }

    private Task<AppEvent[]> mostRecentErrors(int howMany, IQueryable<int> requestIDs) =>
        factory.DB
            .Events
            .Retrieve()
            .Where
            (
                evt => evt.Severity >= AppEventSeverity.Values.ValidationFailed.Value
                    && requestIDs.Any(id => evt.RequestID == id)
            )
            .OrderByDescending(evt => evt.TimeOccurred)
            .Take(howMany)
            .Select(evt => factory.CreateEvent(evt))
            .ToArrayAsync();

}