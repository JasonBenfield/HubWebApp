using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;
using XTI_Core;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class EfAppSessions
{
    private readonly EfHubDB db;

    internal EfAppSessions(EfHubDB db)
    {
        this.db = db;
    }

    public async Task<EfAppSession> Session(string sessionKey, CancellationToken ct)
    {
        var session = await GetSession(sessionKey, ct);
        return new EfAppSession(db, session ?? throw new Exception($"Session '{sessionKey}' not found"));
    }

    public async Task<EfAppSession> Session(int id, CancellationToken ct)
    {
        var session = await db.Context.Sessions.Retrieve()
            .Where(s => s.ID == id)
            .FirstOrDefaultAsync(ct);
        return new EfAppSession(db, session ?? throw new Exception($"Session not found with ID {id}"));
    }

    public async Task<EfAppSession> SessionOrPlaceHolder(string sessionKey, DateTimeOffset now, CancellationToken ct)
    {
        var session = await GetSession(sessionKey, ct);
        if (session == null)
        {
            session = await AddPlaceHolderSession(sessionKey, new GeneratedKey().Value(), now, ct);
        }
        return new EfAppSession(db, session);
    }

    private Task<AppSessionEntity?> GetSession(string sessionKey, CancellationToken ct) =>
        db.Context.Sessions.Retrieve()
            .Where(s => s.SessionKey == sessionKey)
            .FirstOrDefaultAsync(ct);

    private static readonly string defaultRequestKey = "default";

    internal async Task<EfAppSession> DefaultSession(DateTimeOffset now, CancellationToken ct)
    {
        var session = await db.Context.Sessions.Retrieve()
            .Where(r => r.RequesterKey == defaultRequestKey && r.TimeStarted >= now.Date)
            .FirstOrDefaultAsync(ct);
        if (session == null)
        {
            session = await AddPlaceHolderSession(new GeneratedKey().Value(), defaultRequestKey, now, ct);
        }
        return new EfAppSession(db, session);
    }

    private async Task<AppSessionEntity> AddPlaceHolderSession(string sessionKey, string requesterKey, DateTimeOffset now, CancellationToken ct)
    {
        var efUser = await db.Users.Anon(ct);
        var session = await Add
        (
            sessionKey,
            efUser,
            now,
            DateTimeOffset.MaxValue,
            requesterKey,
            "",
            "",
            ct
        );
        return session;
    }

    public Task<EfAppSession[]> ActiveSessions(DateTimeRange timeRange, CancellationToken ct) =>
        db.Context.Sessions.Retrieve()
            .Where(s => s.TimeEnded == DateTimeOffset.MaxValue && s.TimeStarted >= timeRange.Start && s.TimeStarted <= timeRange.End)
            .Select(s => new EfAppSession(db, s))
            .ToArrayAsync(ct);

    public Task<EfAppSession[]> SessionsByTimeRange(DateTimeRange timeRange, CancellationToken ct) =>
        db.Context.Sessions.Retrieve()
            .Where(s => s.TimeStarted >= timeRange.Start && s.TimeStarted <= timeRange.End)
            .Select(s => new EfAppSession(db, s))
            .ToArrayAsync(ct);

    public async Task<EfAppSession> AddOrUpdate(string sessionKey, EfAppUser efUser, DateTimeOffset timeStarted, DateTimeOffset timeEnded, string requesterKey, string userAgent, string remoteAddress, CancellationToken ct)
    {
        var session = await GetSession(sessionKey, ct);
        if (session == null)
        {
            session = await Add(sessionKey, efUser, timeStarted, timeEnded, requesterKey, userAgent, remoteAddress, ct);
        }
        else
        {
            var efAnonUser = await db.Users.Anon(ct);
            if (efUser.ID != efAnonUser.ID && session.UserID != efAnonUser.ID && session.UserID != efUser.ID)
            {
                throw new Exception($"Session {session.ID} authenticated with user {session.UserID} and cannot be changed to user {efUser.ID}.");
            }
            await Update(session, efUser, timeStarted, timeEnded, requesterKey, userAgent, remoteAddress, ct);
        }
        return new EfAppSession(db, session);
    }

    private async Task<AppSessionEntity> Add(string sessionKey, EfAppUser efUser, DateTimeOffset timeStarted, DateTimeOffset timeEnded, string requesterKey, string userAgent, string remoteAddress, CancellationToken ct)
    {
        var session = new AppSessionEntity
        {
            SessionKey = sessionKey,
            UserID = efUser.ID,
            RequesterKey = requesterKey ?? "",
            TimeStarted = timeStarted,
            TimeEnded = timeEnded,
            UserAgent = userAgent ?? "",
            RemoteAddress = remoteAddress ?? ""
        };
        await db.Context.Sessions.Create(session, ct);
        return session;
    }

    public Task Update(AppSessionEntity record, EfAppUser user, DateTimeOffset timeStarted, DateTimeOffset timeEnded, string requesterKey, string userAgent, string remoteAddress, CancellationToken ct) =>
        db.Context.Sessions.Update
        (
            record,
            r =>
            {
                if (!user.IsUserName(new AppUserName()) && !user.IsUserName(AppUserName.Anon))
                {
                    r.UserID = user.ID;
                }
                if (timeStarted < r.TimeStarted)
                {
                    r.TimeStarted = timeStarted;
                }
                if (timeEnded.Year < 9999)
                {
                    r.TimeEnded = timeEnded;
                }
                if (!string.IsNullOrWhiteSpace(requesterKey))
                {
                    r.RequesterKey = requesterKey;
                }
                if (!string.IsNullOrWhiteSpace(userAgent))
                {
                    r.UserAgent = userAgent;
                }
                if (!string.IsNullOrWhiteSpace(remoteAddress))
                {
                    r.RemoteAddress = remoteAddress;
                }
            },
            ct
        );

    public async Task PurgeLogs(DateTimeOffset since, CancellationToken ct)
    {
        db.Context.SetTimeout(TimeSpan.FromMinutes(5));
        var sessionIDs = db.Context.Sessions.Retrieve()
            .Where(s => s.TimeStarted < since)
            .Select(s => s.ID);
        var requestIDs = db.Context.Requests.Retrieve()
            .Where(r => sessionIDs.Contains(r.SessionID))
            .Select(r => r.ID);
        var entryIDs = db.Context.LogEntries.Retrieve()
            .Where(e => requestIDs.Contains(e.RequestID))
            .Select(e => e.ID);
        var sourceLogEntries = await db.Context.SourceLogEntries.Retrieve()
            .Where(src => entryIDs.Contains(src.SourceID))
            .ToArrayAsync(ct);
        foreach (var sourceLogEntry in sourceLogEntries)
        {
            await db.Context.SourceLogEntries.Delete(sourceLogEntry, ct);
        }
        var targetLogEntries = await db.Context.SourceLogEntries.Retrieve()
            .Where(src => entryIDs.Contains(src.TargetID))
            .ToArrayAsync(ct);
        foreach (var targetLogEntry in targetLogEntries)
        {
            await db.Context.SourceLogEntries.Delete(targetLogEntry, ct);
        }
        var logEntries = await db.Context.LogEntries.Retrieve()
            .Where(e => requestIDs.Contains(e.RequestID))
            .ToArrayAsync(ct);
        foreach (var logEntry in logEntries)
        {
            await db.Context.LogEntries.Delete(logEntry, ct);
        }
        var sourceRequests = await db.Context.SourceRequests.Retrieve()
            .Where(r => requestIDs.Contains(r.SourceID))
            .ToArrayAsync(ct);
        foreach (var sourceRequest in sourceRequests)
        {
            await db.Context.SourceRequests.Delete(sourceRequest, ct);
        }
        var targetRequests = await db.Context.SourceRequests.Retrieve()
            .Where(r => requestIDs.Contains(r.TargetID))
            .ToArrayAsync(ct);
        foreach (var targetRequest in targetRequests)
        {
            await db.Context.SourceRequests.Delete(targetRequest, ct);
        }
        var requests = await db.Context.Requests.Retrieve()
            .Where(r => requestIDs.Contains(r.ID))
            .ToArrayAsync(ct);
        foreach (var request in requests)
        {
            await db.Context.Requests.Delete(request, ct);
        }
        var sessions = await db.Context.Sessions.Retrieve()
            .Where(s => sessionIDs.Contains(s.ID))
            .ToArrayAsync(ct);
        foreach (var session in sessions)
        {
            await db.Context.Sessions.Delete(session, ct);
        }
    }
}