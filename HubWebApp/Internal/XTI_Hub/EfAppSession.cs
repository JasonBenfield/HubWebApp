using XTI_Hub.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class EfAppSession
{
    private readonly EfHubDB factory;
    private readonly AppSessionEntity record;

    internal EfAppSession(EfHubDB factory, AppSessionEntity record)
    {
        this.factory = factory;
        this.record = record;
        ID = this.record.ID;
    }

    public int ID { get; }
    public int UserID { get => record.UserID; }

    public bool HasStarted() => record.TimeStarted > DateTimeOffset.MinValue;
    public bool HasEnded() => record.TimeEnded < DateTimeOffset.MaxValue;

    public Task<EfAppRequest> LogRequest
    (
        string requestKey,
        EfInstallation installation,
        string path,
        DateTimeOffset timeStarted,
        DateTimeOffset timeEnded,
        int actualCount,
        string sourceRequestKey,
        string requestData,
        string resultData, 
        CancellationToken ct
    ) => factory.Requests.AddOrUpdate
        (
            this,
            requestKey,
            installation,
            path,
            timeStarted,
            timeEnded,
            actualCount,
            sourceRequestKey,
            requestData,
            resultData,
            ct
        );

    public Task Authenticate(EfAppUser user, CancellationToken ct) =>
        factory.Context
            .Sessions
            .Update
            (
                record,
                r =>
                {
                    r.UserID = user.ID;
                },
                ct
            );

    public Task End(DateTimeOffset timeEnded, CancellationToken ct) =>
        factory.Context
            .Sessions
            .Update
            (
                record,
                r =>
                {
                    r.TimeEnded = timeEnded;
                },
                ct
            );

    public Task<EfAppRequest[]> Requests(CancellationToken ct) => factory.Requests.RetrieveBySession(this, ct);

    public Task<EfAppRequest[]> MostRecentRequests(int howMany, CancellationToken ct) =>
        factory.Requests.RetrieveMostRecent(this, howMany, ct);

    public Task<EfAppUser> User(CancellationToken ct) => factory.Users.User(record.UserID, ct);

    public AppSessionModel ToModel() =>
        new 
        (
            ID: record.ID,
            TimeStarted: record.TimeStarted,
            TimeEnded: record.TimeEnded,
            SessionKey: record.SessionKey,
            RequesterKey: record.RequesterKey,
            RemoteAddress: record.RemoteAddress,
            UserAgent: record.UserAgent
        );

    public override string ToString() => $"{nameof(EfAppSession)} {record.ID}";
}