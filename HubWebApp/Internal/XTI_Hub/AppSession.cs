using XTI_Hub.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class AppSession
{
    private readonly HubFactory factory;
    private readonly AppSessionEntity record;

    internal AppSession(HubFactory factory, AppSessionEntity record)
    {
        this.factory = factory;
        this.record = record;
        ID = this.record.ID;
    }

    public int ID { get; }
    public int UserID { get => record.UserID; }

    public bool HasStarted() => record.TimeStarted > DateTimeOffset.MinValue;
    public bool HasEnded() => record.TimeEnded < DateTimeOffset.MaxValue;

    public Task<AppRequest> LogRequest
    (
        string requestKey,
        Installation installation,
        string path,
        DateTimeOffset timeStarted,
        DateTimeOffset timeEnded,
        int actualCount,
        string sourceRequestKey,
        string requestData,
        string resultData
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
            resultData
        );

    public Task Authenticate(AppUser user) =>
        factory.DB
            .Sessions
            .Update
            (
                record,
                r =>
                {
                    r.UserID = user.ID;
                }
            );

    public Task End(DateTimeOffset timeEnded) =>
        factory.DB
            .Sessions
            .Update
            (
                record,
                r =>
                {
                    r.TimeEnded = timeEnded;
                }
            );

    public Task<AppRequest[]> Requests() => factory.Requests.RetrieveBySession(this);

    public Task<AppRequest[]> MostRecentRequests(int howMany) =>
        factory.Requests.RetrieveMostRecent(this, howMany);

    public Task<AppUser> User() => factory.Users.User(record.UserID);

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

    public override string ToString() => $"{nameof(AppSession)} {record.ID}";
}