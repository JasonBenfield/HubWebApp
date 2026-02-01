using Microsoft.EntityFrameworkCore;
using XTI_Core;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class EfLogEntries
{
    private readonly EfHubDB db;

    public EfLogEntries(EfHubDB db)
    {
        this.db = db;
    }

    public async Task<EfLogEntry> LogEvent
    (
        EfAppRequest request,
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
        var logEntryEntity = await db.Context.Transaction
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
        return new EfLogEntry(db, logEntryEntity);
    }

    private async Task<LogEntryEntity> Log
    (
        EfAppRequest request,
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
        EfAppRequest request,
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
            await db.Context.LogEntries.Create(logEntryEntity, ct);
        }
        else
        {
            await db.Context.LogEntries.Update
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

    private async Task<LogEntryEntity> AddSourceLogEntryIfNotExists(EfAppRequest request, DateTimeOffset timeOccurred, AppEventSeverity severity, int actualCount, string sourceLogEntryKey, CancellationToken ct)
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
            await db.Context.LogEntries.Create(sourceEntryEntity, ct);
        }
        return sourceEntryEntity;
    }

    private async Task AddSourceLinkIfNotExists(LogEntryEntity logEntryEntity, LogEntryEntity sourceEntryEntity, CancellationToken ct)
    {
        var linkExists = await db.Context.SourceLogEntries.Retrieve()
            .Where(src => src.SourceID == sourceEntryEntity.ID && src.TargetID == logEntryEntity.ID)
            .AnyAsync(ct);
        if (!linkExists)
        {
            await db.Context.SourceLogEntries.Create
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

    public async Task<EfLogEntry> LogEntryOrDefaultByKey(string eventKey, CancellationToken ct)
    {
        var logEntry = await GetLogEntryByKey(eventKey, ct);
        return new EfLogEntry
        (
            db,
            logEntry ?? new LogEntryEntity()
        );
    }

    public async Task<EfLogEntry> LogEntry(int id, CancellationToken ct)
    {
        var logEntry = await db.Context.LogEntries.Retrieve()
            .Where(e => e.ID == id)
            .FirstOrDefaultAsync(ct);
        return new EfLogEntry
        (
            db,
            logEntry ?? throw new Exception($"Log Entry not found with ID '{id}'")
        );
    }

    internal async Task<EfLogEntry> SourceLogEntryOrDefault(int forTargetID, CancellationToken ct)
    {
        var sourceIDs = db.Context.SourceLogEntries.Retrieve()
            .Where(src => src.TargetID == forTargetID)
            .Select(src => src.SourceID);
        var logEntries = await db.Context.LogEntries.Retrieve()
            .Where(e => sourceIDs.Contains(e.ID))
            .ToArrayAsync(ct);
        return new EfLogEntry(db, logEntries.FirstOrDefault() ?? new());
    }

    internal async Task<EfLogEntry> TargetLogEntryOrDefault(int forSourceID, CancellationToken ct)
    {
        var targetIDs = db.Context.SourceLogEntries.Retrieve()
            .Where(src => src.SourceID == forSourceID)
            .Select(src => src.TargetID);
        var logEntries = await db.Context.LogEntries.Retrieve()
            .Where(e => targetIDs.Contains(e.ID))
            .ToArrayAsync(ct);
        return new EfLogEntry(db, logEntries.FirstOrDefault() ?? new());
    }

    private Task<LogEntryEntity?> GetLogEntryByKey(string eventKey, CancellationToken ct) =>
        db.Context.LogEntries.Retrieve()
            .Where(e => e.EventKey == eventKey)
            .FirstOrDefaultAsync(ct);

    internal Task<EfLogEntry[]> RetrieveByRequest(EfAppRequest request, CancellationToken ct) =>
        db.Context.LogEntries.Retrieve()
            .Where(e => e.RequestID == request.ID)
            .Select(e => new EfLogEntry(db, e))
            .ToArrayAsync(ct);

    internal Task<EfLogEntry[]> MostRecentLoggedErrorsForVersion(EfApp app, EfVersion version, int howMany, CancellationToken ct)
    {
        var appVersionID = db.Versions.QueryAppVersionID(app, version);
        var requestIDs = db.Context
            .Requests
            .Retrieve()
            .Join
            (
                db.Context.Resources
                    .Retrieve(),
                req => req.ResourceID,
                res => res.ID,
                (req, res) => new { RequestID = req.ID, res.GroupID }
            )
            .Join
            (
                db.Context.ResourceGroups
                    .Retrieve(),
                res => res.GroupID,
                rg => rg.ID,
                (res, rg) => new { res.RequestID, rg.AppVersionID }
            )
            .Where(rg => appVersionID.Contains(rg.AppVersionID))
            .Select(rg => rg.RequestID);
        return MostRecentErrors(howMany, requestIDs, ct);
    }

    internal Task<EfLogEntry[]> MostRecentErrorsForResourceGroup(EfResourceGroup group, int howMany, CancellationToken ct)
    {
        var requestIDs = db.Context
            .Requests
            .Retrieve()
            .Join
            (
                db.Context
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

    internal Task<EfLogEntry[]> MostRecentErrorsForResource(EfResource resource, int howMany, CancellationToken ct)
    {
        var requestIDs = db.Context.Requests.Retrieve()
            .Where(r => r.ResourceID == resource.ID)
            .Select(r => r.ResourceID);
        return MostRecentErrors(howMany, requestIDs, ct);
    }

    private Task<EfLogEntry[]> MostRecentErrors(int howMany, IQueryable<int> requestIDs, CancellationToken ct) =>
        db.Context.LogEntries.Retrieve()
            .Where
            (
                le =>
                    le.Severity >= AppEventSeverity.Values.ValidationFailed.Value &&
                    requestIDs.Any(id => le.RequestID == id)
            )
            .OrderByDescending(evt => evt.TimeOccurred)
            .Take(howMany)
            .Select(le => new EfLogEntry(db, le))
            .ToArrayAsync(ct);

}