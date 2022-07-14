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

    public async Task<AppSession> Session(string sessionKey)
    {
        var record = await GetSession(sessionKey);
        return factory.CreateSession(record ?? throw new Exception($"Session '{sessionKey}' not found"));
    }

    public async Task<AppSession> SessionOrPlaceHolder(string sessionKey, DateTimeOffset now)
    {
        var record = await GetSession(sessionKey);
        if (record == null)
        {
            record = await AddPlaceHolderSession(sessionKey, new GeneratedKey().Value(), now);
        }
        return factory.CreateSession(record);
    }

    private Task<AppSessionEntity?> GetSession(string sessionKey) =>
        factory.DB
            .Sessions
            .Retrieve()
            .FirstOrDefaultAsync(s => s.SessionKey == sessionKey);

    private static readonly string defaultRequestKey = "default";

    internal async Task<AppSession> DefaultSession(DateTimeOffset now)
    {
        var record = await factory.DB
            .Sessions
            .Retrieve()
            .FirstOrDefaultAsync
            (
                r => r.RequesterKey == defaultRequestKey && r.TimeStarted >= now.Date
            );
        if (record == null)
        {
            record = await AddPlaceHolderSession(new GeneratedKey().Value(), defaultRequestKey, now);
        }
        return factory.CreateSession(record);
    }

    private async Task<AppSessionEntity> AddPlaceHolderSession(string sessionKey, string requesterKey, DateTimeOffset now)
    {
        var user = await factory.Users.Anon();
        var record = await Add
        (
            sessionKey,
            user,
            now,
            requesterKey,
            "",
            ""
        );
        return record;
    }

    public Task<AppSession[]> ActiveSessions(DateTimeRange timeRange) =>
        factory.DB
            .Sessions
            .Retrieve()
            .Where(s => s.TimeEnded == DateTimeOffset.MaxValue && s.TimeStarted >= timeRange.Start && s.TimeStarted <= timeRange.End)
            .Select(s => factory.CreateSession(s))
            .ToArrayAsync();

    public Task<AppSession[]> SessionsByTimeRange(DateTimeRange timeRange) =>
        factory.DB
            .Sessions
            .Retrieve()
            .Where(s => s.TimeStarted >= timeRange.Start && s.TimeStarted <= timeRange.End)
            .Select(s => factory.CreateSession(s))
            .ToArrayAsync();

    public async Task<AppSession> AddOrUpdate(string sessionKey, AppUser user, DateTimeOffset timeStarted, string requesterKey, string userAgent, string remoteAddress)
    {
        var record = await GetSession(sessionKey);
        if (record == null)
        {
            record = await Add(sessionKey, user, timeStarted, requesterKey, userAgent, remoteAddress);
        }
        else
        {
            await Update(record, user, timeStarted, requesterKey, userAgent, remoteAddress);
        }
        return factory.CreateSession(record);
    }

    private async Task<AppSessionEntity> Add(string sessionKey, AppUser user, DateTimeOffset timeStarted, string requesterKey, string userAgent, string remoteAddress)
    {
        var record = new AppSessionEntity
        {
            SessionKey = sessionKey,
            UserID = user.ID,
            TimeStarted = timeStarted,
            RequesterKey = requesterKey ?? "",
            TimeEnded = DateTimeOffset.MaxValue,
            UserAgent = userAgent ?? "",
            RemoteAddress = remoteAddress ?? ""
        };
        await factory.DB.Sessions.Create(record);
        return record;
    }

    public Task Update(AppSessionEntity record, AppUser user, DateTimeOffset timeStarted, string requesterKey, string userAgent, string remoteAddress)
        => factory.DB
            .Sessions
            .Update
            (
                record,
                r =>
                {
                    r.UserID = user.ID;
                    r.TimeStarted = timeStarted;
                    r.RequesterKey = requesterKey;
                    r.UserAgent = userAgent ?? "";
                    r.RemoteAddress = remoteAddress ?? "";
                }
            );

}