using Microsoft.EntityFrameworkCore;
using XTI_Core;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class AppLogEntryRepository
{
    private readonly HubFactory factory;

    public AppLogEntryRepository(HubFactory factory)
    {
        this.factory = factory;
    }

    public async Task<LogEntry> LogEvent
    (
        AppRequest request,
        string logEntryKey,
        DateTimeOffset timeOccurred,
        AppEventSeverity severity,
        string caption,
        string message,
        string detail,
        int actualCount,
        string sourceLogEntryKey,
        string category
    )
    {
        var logEntryEntity = await factory.DB.Transaction
        (
            () => Log
            (
                request,
                logEntryKey,
                timeOccurred,
                severity,
                caption,
                message,
                detail,
                actualCount,
                sourceLogEntryKey,
                category
            )
        );
        return factory.CreateLogEntry(logEntryEntity);
    }

    private async Task<LogEntryEntity> Log
    (
        AppRequest request,
        string logEntryKey,
        DateTimeOffset timeOccurred,
        AppEventSeverity severity,
        string caption,
        string message,
        string detail,
        int actualCount,
        string sourceLogEntryKey,
        string category
    )
    {
        var logEntryEntity = await AddOrUpdateLogEntry
        (
            request,
            logEntryKey,
            timeOccurred,
            severity,
            caption,
            message,
            detail,
            actualCount,
            category
        );
        if (!string.IsNullOrWhiteSpace(sourceLogEntryKey))
        {
            var sourceEntryEntity = await AddSourceLogEntryIfNotExists
            (
                request,
                timeOccurred,
                severity,
                actualCount,
                sourceLogEntryKey
            );
            await AddSourceLinkIfNotExists(logEntryEntity, sourceEntryEntity);
        }
        return logEntryEntity;
    }

    private async Task<LogEntryEntity> AddOrUpdateLogEntry
    (
        AppRequest request, 
        string logEntryKey, 
        DateTimeOffset timeOccurred, 
        AppEventSeverity severity, 
        string caption, 
        string message, 
        string detail, 
        int actualCount, 
        string category
    )
    {
        var logEntryEntity = await GetLogEntryByKey(logEntryKey);
        if (logEntryEntity == null)
        {
            logEntryEntity = new LogEntryEntity
            {
                RequestID = request.ID,
                EventKey = logEntryKey,
                TimeOccurred = timeOccurred,
                Severity = severity.Value,
                Caption = caption,
                Message = message,
                Detail = detail,
                ActualCount = actualCount,
                Category = category
            };
            await factory.DB.LogEntries.Create(logEntryEntity);
        }
        else
        {
            await factory.DB.LogEntries.Update
            (
                logEntryEntity,
                evt =>
                {
                    evt.RequestID = request.ID;
                    evt.TimeOccurred = timeOccurred;
                    evt.Severity = severity.Value;
                    evt.Caption = caption;
                    evt.Message = message;
                    evt.Detail = detail;
                    evt.ActualCount = actualCount;
                    evt.Category = category;
                }
            );
        }
        return logEntryEntity;
    }

    private async Task<LogEntryEntity> AddSourceLogEntryIfNotExists(AppRequest request, DateTimeOffset timeOccurred, AppEventSeverity severity, int actualCount, string sourceLogEntryKey)
    {
        var sourceEntryEntity = await GetLogEntryByKey(sourceLogEntryKey);
        if (sourceEntryEntity == null)
        {
            sourceEntryEntity = new LogEntryEntity
            {
                RequestID = request.ID,
                EventKey = sourceLogEntryKey,
                TimeOccurred = timeOccurred,
                Severity = severity.Value,
                Caption = "Placeholder",
                Message = "Placeholder",
                Detail = "Placeholder",
                ActualCount = actualCount
            };
            await factory.DB.LogEntries.Create(sourceEntryEntity);
        }
        return sourceEntryEntity;
    }

    private async Task AddSourceLinkIfNotExists(LogEntryEntity logEntryEntity, LogEntryEntity sourceEntryEntity)
    {
        var linkExists = await factory.DB.SourceLogEntries.Retrieve()
            .Where(src => src.SourceID == sourceEntryEntity.ID && src.TargetID == logEntryEntity.ID)
            .AnyAsync();
        if (!linkExists)
        {
            await factory.DB.SourceLogEntries.Create
            (
                new SourceLogEntryEntity
                {
                    SourceID = sourceEntryEntity.ID,
                    TargetID = logEntryEntity.ID
                }
            );
        }
    }

    public async Task<LogEntry> LogEntryByKey(string eventKey)
    {
        var entity = await GetLogEntryByKey(eventKey);
        return factory.CreateLogEntry
        (
            entity ?? throw new Exception($"Log Entry not found with key '{eventKey}'")
        );
    }

    public async Task<LogEntry> LogEntry(int id)
    {
        var entity = await factory.DB.LogEntries.Retrieve()
            .Where(e => e.ID == id)
            .FirstOrDefaultAsync();
        return factory.CreateLogEntry
        (
            entity ?? throw new Exception($"Log Entry not found with ID '{id}'")
        );
    }

    internal async Task<LogEntry> SourceLogEntryOrDefault(int forTargetID)
    {
        LogEntryEntity? entity;
        var sourceID = await factory.DB.SourceLogEntries.Retrieve()
            .Where(src => src.TargetID == forTargetID)
            .Select(src => src.SourceID)
            .FirstOrDefaultAsync();
        if (sourceID > 0)
        {
            entity = await factory.DB
                .LogEntries.Retrieve()
                .Where(e => e.ID == sourceID)
                .FirstAsync();
        }
        else
        {
            entity = new LogEntryEntity();
        }
        return factory.CreateLogEntry(entity);
    }

    internal async Task<LogEntry> TargetLogEntryOrDefault(int forSourceID)
    {
        LogEntryEntity? entity;
        var targetID = await factory.DB.SourceLogEntries.Retrieve()
            .Where(src => src.SourceID == forSourceID)
            .Select(src => src.TargetID)
            .FirstOrDefaultAsync();
        if (targetID > 0)
        {
            entity = await factory.DB
                .LogEntries.Retrieve()
                .Where(e => e.ID == targetID)
                .FirstAsync();
        }
        else
        {
            entity = new LogEntryEntity();
        }
        return factory.CreateLogEntry(entity);
    }

    private Task<LogEntryEntity?> GetLogEntryByKey(string eventKey) =>
        factory.DB.LogEntries.Retrieve()
            .Where(e => e.EventKey == eventKey)
            .FirstOrDefaultAsync();

    internal Task<LogEntry[]> RetrieveByRequest(AppRequest request) =>
        factory.DB.LogEntries.Retrieve()
            .Where(e => e.RequestID == request.ID)
            .Select(e => factory.CreateLogEntry(e))
            .ToArrayAsync();

    internal Task<LogEntry[]> MostRecentLoggedErrorsForVersion(App app, XtiVersion version, int howMany)
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
                (req, res) => new { RequestID = req.ID, res.GroupID }
            )
            .Join
            (
                factory.DB.ResourceGroups
                    .Retrieve(),
                res => res.GroupID,
                rg => rg.ID,
                (res, rg) => new { res.RequestID, rg.AppVersionID }
            )
            .Where(rg => appVersionID.Contains(rg.AppVersionID))
            .Select(rg => rg.RequestID);
        return MostRecentErrors(howMany, requestIDs);
    }

    internal Task<LogEntry[]> MostRecentErrorsForResourceGroup(ResourceGroup group, int howMany)
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
                (req, res) => new { RequestID = req.ID, res.GroupID }
            )
            .Where(rg => rg.GroupID == group.ID)
            .Select(rg => rg.RequestID);
        return MostRecentErrors(howMany, requestIDs);
    }

    internal Task<LogEntry[]> MostRecentErrorsForResource(Resource resource, int howMany)
    {
        var requestIDs = factory.DB
            .Requests
            .Retrieve()
            .Where(r => r.ResourceID == resource.ID)
            .Select(r => r.ResourceID);
        return MostRecentErrors(howMany, requestIDs);
    }

    private Task<LogEntry[]> MostRecentErrors(int howMany, IQueryable<int> requestIDs) =>
        factory.DB
            .LogEntries
            .Retrieve()
            .Where
            (
                evt => evt.Severity >= AppEventSeverity.Values.ValidationFailed.Value
                    && requestIDs.Any(id => evt.RequestID == id)
            )
            .OrderByDescending(evt => evt.TimeOccurred)
            .Take(howMany)
            .Select(evt => factory.CreateLogEntry(evt))
            .ToArrayAsync();

}