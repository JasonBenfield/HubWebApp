using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;
using XTI_Core;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class AppSessionRepository
{
    private readonly HubFactory factory;

    internal AppSessionRepository(HubFactory factory)
    {
        this.factory = factory;
    }

    public async Task<AppSession> Session(string sessionKey, CancellationToken ct)
    {
        var record = await GetSession(sessionKey, ct);
        return factory.CreateSession(record ?? throw new Exception($"Session '{sessionKey}' not found"));
    }

    public async Task<AppSession> Session(int id, CancellationToken ct)
    {
        var record = await factory.DB.Sessions.Retrieve()
            .FirstOrDefaultAsync(s => s.ID == id, ct);
        return factory.CreateSession(record ?? throw new Exception($"Session not found with ID {id}"));
    }

    public async Task<AppSession> SessionOrPlaceHolder(string sessionKey, DateTimeOffset now, CancellationToken ct)
    {
        var record = await GetSession(sessionKey, ct);
        if (record == null)
        {
            record = await AddPlaceHolderSession(sessionKey, new GeneratedKey().Value(), now, ct);
        }
        return factory.CreateSession(record);
    }

    private Task<AppSessionEntity?> GetSession(string sessionKey, CancellationToken ct) =>
        factory.DB
            .Sessions
            .Retrieve()
            .FirstOrDefaultAsync(s => s.SessionKey == sessionKey, ct);

    private static readonly string defaultRequestKey = "default";

    internal async Task<AppSession> DefaultSession(DateTimeOffset now, CancellationToken ct)
    {
        var record = await factory.DB
            .Sessions
            .Retrieve()
            .FirstOrDefaultAsync
            (
                r => r.RequesterKey == defaultRequestKey && r.TimeStarted >= now.Date,
                ct
            );
        if (record == null)
        {
            record = await AddPlaceHolderSession(new GeneratedKey().Value(), defaultRequestKey, now, ct);
        }
        return factory.CreateSession(record);
    }

    private async Task<AppSessionEntity> AddPlaceHolderSession(string sessionKey, string requesterKey, DateTimeOffset now, CancellationToken ct)
    {
        var user = await factory.Users.Anon(ct);
        var record = await Add
        (
            sessionKey,
            user,
            now,
            DateTimeOffset.MaxValue,
            requesterKey,
            "",
            "",
            ct
        );
        return record;
    }

    public Task<AppSession[]> ActiveSessions(DateTimeRange timeRange, CancellationToken ct) =>
        factory.DB
            .Sessions
            .Retrieve()
            .Where(s => s.TimeEnded == DateTimeOffset.MaxValue && s.TimeStarted >= timeRange.Start && s.TimeStarted <= timeRange.End)
            .Select(s => factory.CreateSession(s))
            .ToArrayAsync(ct);

    public Task<AppSession[]> SessionsByTimeRange(DateTimeRange timeRange, CancellationToken ct) =>
        factory.DB
            .Sessions
            .Retrieve()
            .Where(s => s.TimeStarted >= timeRange.Start && s.TimeStarted <= timeRange.End)
            .Select(s => factory.CreateSession(s))
            .ToArrayAsync(ct);

    public async Task<AppSession> AddOrUpdate(string sessionKey, AppUser user, DateTimeOffset timeStarted, DateTimeOffset timeEnded, string requesterKey, string userAgent, string remoteAddress, CancellationToken ct)
    {
        var record = await GetSession(sessionKey, ct);
        if (record == null)
        {
            record = await Add(sessionKey, user, timeStarted, timeEnded, requesterKey, userAgent, remoteAddress, ct);
        }
        else
        {
            await Update(record, user, timeStarted, timeEnded, requesterKey, userAgent, remoteAddress, ct);
        }
        return factory.CreateSession(record);
    }

    private async Task<AppSessionEntity> Add(string sessionKey, AppUser user, DateTimeOffset timeStarted, DateTimeOffset timeEnded, string requesterKey, string userAgent, string remoteAddress, CancellationToken ct)
    {
        var record = new AppSessionEntity
        {
            SessionKey = sessionKey,
            UserID = user.ID,
            RequesterKey = requesterKey ?? "",
            TimeStarted = timeStarted,
            TimeEnded = timeEnded,
            UserAgent = userAgent ?? "",
            RemoteAddress = remoteAddress ?? ""
        };
        await factory.DB.Sessions.Create(record, ct);
        return record;
    }

    public Task Update(AppSessionEntity record, AppUser user, DateTimeOffset timeStarted, DateTimeOffset timeEnded, string requesterKey, string userAgent, string remoteAddress, CancellationToken ct) =>
        factory.DB
            .Sessions
            .Update
            (
                record,
                r =>
                {
                    if (!user.IsUserName(new AppUserName()) && !user.IsUserName(AppUserName.Anon))
                    {
                        r.UserID = user.ID;
                    }
                    if(timeStarted < r.TimeStarted)
                    {
                        r.TimeStarted = timeStarted;
                    }
                    if(timeEnded.Year < 9999)
                    {
                        r.TimeEnded = timeEnded;
                    }
                    r.RequesterKey = requesterKey;
                    r.UserAgent = userAgent ?? "";
                    r.RemoteAddress = remoteAddress ?? "";
                },
                ct
            );

    public async Task PurgeLogs(DateTimeOffset since, CancellationToken ct)
    {
        factory.DB.SetTimeout(TimeSpan.FromMinutes(5));
        var sessionIDs = factory.DB
            .Sessions.Retrieve()
            .Where(s => s.TimeStarted < since)
            .Select(s => s.ID);
        var requestIDs = factory.DB
            .Requests.Retrieve()
            .Where(r => sessionIDs.Contains(r.SessionID))
            .Select(r => r.ID);
        var entryIDs = factory.DB.LogEntries.Retrieve()
            .Where(e => requestIDs.Contains(e.RequestID))
            .Select(e => e.ID);
        var sourceLogEntries = await factory.DB.SourceLogEntries.Retrieve()
            .Where(src => entryIDs.Contains(src.SourceID))
            .ToArrayAsync(ct);
        foreach (var sourceLogEntry in sourceLogEntries)
        {
            await factory.DB.SourceLogEntries.Delete(sourceLogEntry, ct);
        }
        var targetLogEntries = await factory.DB.SourceLogEntries.Retrieve()
            .Where(src => entryIDs.Contains(src.TargetID))
            .ToArrayAsync(ct);
        foreach (var targetLogEntry in targetLogEntries)
        {
            await factory.DB.SourceLogEntries.Delete(targetLogEntry, ct);
        }
        var logEntries = await factory.DB.LogEntries.Retrieve()
            .Where(e => requestIDs.Contains(e.RequestID))
            .ToArrayAsync(ct);
        foreach (var logEntry in logEntries)
        {
            await factory.DB.LogEntries.Delete(logEntry, ct);
        }

        var sourceRequests = await factory.DB.SourceRequests.Retrieve()
            .Where(r => requestIDs.Contains(r.SourceID))
            .ToArrayAsync(ct);
        foreach(var sourceRequest in sourceRequests)
        {
            await factory.DB.SourceRequests.Delete(sourceRequest, ct);
        }

        var targetRequests = await factory.DB.SourceRequests.Retrieve()
            .Where(r => requestIDs.Contains(r.TargetID))
            .ToArrayAsync(ct);
        foreach (var targetRequest in targetRequests)
        {
            await factory.DB.SourceRequests.Delete(targetRequest, ct);
        }

        var requests = await factory.DB.Requests.Retrieve()
            .Where(r => requestIDs.Contains(r.ID))
            .ToArrayAsync(ct);
        foreach (var request in requests)
        {
            await factory.DB.Requests.Delete(request, ct);
        }
        var sessions = await factory.DB.Sessions.Retrieve()
            .Where(s => sessionIDs.Contains(s.ID))
            .ToArrayAsync(ct);
        foreach (var session in sessions)
        {
            await factory.DB.Sessions.Delete(session, ct);
        }
    }
}