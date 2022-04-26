using XTI_App.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class AppSession
{
    private readonly AppFactory factory;
    private readonly AppSessionEntity record;

    internal AppSession(AppFactory factory, AppSessionEntity record)
    {
        this.factory = factory;
        this.record = record ?? new AppSessionEntity();
        ID = new EntityID(this.record.ID);
    }

    public EntityID ID { get; }
    public int UserID { get => record.UserID; }

    public bool HasStarted() => record.TimeStarted > DateTimeOffset.MinValue;
    public bool HasEnded() => record.TimeEnded < DateTimeOffset.MaxValue;

    public Task<AppRequest> LogRequest
    (
        string requestKey,
        Resource resource,
        Modifier modifier,
        string path,
        DateTimeOffset timeRequested,
        int actualCount
    )
        => factory.Requests.AddOrUpdate(this, requestKey, resource, modifier, path, timeRequested, actualCount);

    public Task Authenticate(IAppUser user)
        => factory.DB
            .Sessions
            .Update(record, r =>
            {
                r.UserID = user.ID.Value;
            });

    public Task End(DateTimeOffset timeEnded)
        => factory.DB
            .Sessions
            .Update(record, r =>
            {
                r.TimeEnded = timeEnded;
            });

    public Task<AppRequest[]> Requests() => factory.Requests.RetrieveBySession(this);

    public Task<AppRequest[]> MostRecentRequests(int howMany)
        => factory.Requests.RetrieveMostRecent(this, howMany);

    public override string ToString() => $"{nameof(AppSession)} {ID.Value}";
}