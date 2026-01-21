using Microsoft.EntityFrameworkCore;
using XTI_Core;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class LogEntryRepository
{
    private readonly HubFactory factory;

    public LogEntryRepository(HubFactory factory)
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
        string category,
        CancellationToken ct
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
                category,
                ct
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
        string category,
        CancellationToken ct
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
            category,
            ct
        );
        if (!string.IsNullOrWhiteSpace(sourceLogEntryKey))
        {
            var sourceEntryEntity = await AddSourceLogEntryIfNotExists
            (
                request,
                timeOccurred,
                severity,
                actualCount,
                sourceLogEntryKey,
                ct
            );
            await AddSourceLinkIfNotExists(logEntryEntity, sourceEntryEntity, ct);
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
        string category,
        CancellationToken ct
    )
    {
        caption = new TruncatedText(caption, 1000).Value;
        message = new TruncatedText(message, 5000).Value;
        detail = new TruncatedText(detail, 32000).Value;
        category = new TruncatedText(category, 500).Value;
        var logEntryEntity = await GetLogEntryByKey(logEntryKey, ct);
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
            await factory.DB.LogEntries.Create(logEntryEntity, ct);
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
                },
                ct
            );
        }
        return logEntryEntity;
    }

    private async Task<LogEntryEntity> AddSourceLogEntryIfNotExists(AppRequest request, DateTimeOffset timeOccurred, AppEventSeverity severity, int actualCount, string sourceLogEntryKey, CancellationToken ct)
    {
        var sourceEntryEntity = await GetLogEntryByKey(sourceLogEntryKey, ct);
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
            await factory.DB.LogEntries.Create(sourceEntryEntity, ct);
        }
        return sourceEntryEntity;
    }

    private async Task AddSourceLinkIfNotExists(LogEntryEntity logEntryEntity, LogEntryEntity sourceEntryEntity, CancellationToken ct)
    {
        var linkExists = await factory.DB.SourceLogEntries.Retrieve()
            .Where(src => src.SourceID == sourceEntryEntity.ID && src.TargetID == logEntryEntity.ID)
            .AnyAsync(ct);
        if (!linkExists)
        {
            await factory.DB.SourceLogEntries.Create
            (
                new SourceLogEntryEntity
                {
                    SourceID = sourceEntryEntity.ID,
                    TargetID = logEntryEntity.ID
                },
                ct
            );
        }
    }

    public async Task<LogEntry> LogEntryOrDefaultByKey(string eventKey, CancellationToken ct)
    {
        var entity = await GetLogEntryByKey(eventKey, ct);
        return factory.CreateLogEntry
        (
            entity ?? new LogEntryEntity()
        );
    }

    public async Task<LogEntry> LogEntry(int id, CancellationToken ct)
    {
        var entity = await factory.DB.LogEntries.Retrieve()
            .Where(e => e.ID == id)
            .FirstOrDefaultAsync(ct);
        return factory.CreateLogEntry
        (
            entity ?? throw new Exception($"Log Entry not found with ID '{id}'")
        );
    }

    internal async Task<LogEntry> SourceLogEntryOrDefault(int forTargetID, CancellationToken ct)
    {
        var sourceIDs = factory.DB.SourceLogEntries.Retrieve()
            .Where(src => src.TargetID == forTargetID)
            .Select(src => src.SourceID);
        var entities = await factory.DB
            .LogEntries.Retrieve()
            .Where(e => sourceIDs.Contains(e.ID))
            .ToArrayAsync(ct);
        return factory.CreateLogEntry(entities.FirstOrDefault() ?? new());
    }

    internal async Task<LogEntry> TargetLogEntryOrDefault(int forSourceID, CancellationToken ct)
    {
        var targetIDs = factory.DB.SourceLogEntries.Retrieve()
            .Where(src => src.SourceID == forSourceID)
            .Select(src => src.TargetID);
        var entities = await factory.DB
            .LogEntries.Retrieve()
            .Where(e => targetIDs.Contains(e.ID))
            .ToArrayAsync(ct);
        return factory.CreateLogEntry(entities.FirstOrDefault() ?? new());
    }

    private Task<LogEntryEntity?> GetLogEntryByKey(string eventKey, CancellationToken ct) =>
        factory.DB.LogEntries.Retrieve()
            .Where(e => e.EventKey == eventKey)
            .FirstOrDefaultAsync(ct);

    internal Task<LogEntry[]> RetrieveByRequest(AppRequest request, CancellationToken ct) =>
        factory.DB.LogEntries.Retrieve()
            .Where(e => e.RequestID == request.ID)
            .Select(e => factory.CreateLogEntry(e))
            .ToArrayAsync(ct);

    internal Task<LogEntry[]> MostRecentLoggedErrorsForVersion(App app, XtiVersion version, int howMany, CancellationToken ct)
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
        return MostRecentErrors(howMany, requestIDs, ct);
    }

    internal Task<LogEntry[]> MostRecentErrorsForResourceGroup(ResourceGroup group, int howMany, CancellationToken ct)
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
        return MostRecentErrors(howMany, requestIDs, ct);
    }

    internal Task<LogEntry[]> MostRecentErrorsForResource(Resource resource, int howMany, CancellationToken ct)
    {
        var requestIDs = factory.DB
            .Requests
            .Retrieve()
            .Where(r => r.ResourceID == resource.ID)
            .Select(r => r.ResourceID);
        return MostRecentErrors(howMany, requestIDs, ct);
    }

    private Task<LogEntry[]> MostRecentErrors(int howMany, IQueryable<int> requestIDs, CancellationToken ct) =>
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
            .ToArrayAsync(ct);

}