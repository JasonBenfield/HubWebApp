using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;
using XTI_Core;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class AppRequestRepository
{
    private readonly HubFactory factory;

    internal AppRequestRepository(HubFactory factory)
    {
        this.factory = factory;
    }

    public async Task<AppRequest> Request(int id, CancellationToken ct)
    {
        var entity = await factory.DB.Requests.Retrieve()
            .Where(r => r.ID == id)
            .FirstOrDefaultAsync(ct);
        return factory.CreateRequest(entity ?? throw new Exception($"Request not found with ID {id}"));
    }

    public async Task<AppRequest> RequestOrDefault(string requestKey, CancellationToken ct)
    {
        var entity = await factory.DB.Requests.Retrieve()
            .Where(r => r.RequestKey == requestKey)
            .FirstOrDefaultAsync(ct);
        return factory.CreateRequest(entity ?? new());
    }

    internal async Task<AppRequest> AddOrUpdate
    (
        AppSession session,
        string requestKey,
        Installation installation,
        string path,
        DateTimeOffset timeStarted,
        DateTimeOffset timeEnded,
        int actualCount,
        string sourceRequestKey,
        string requestData,
        string resultData,
        CancellationToken ct
    )
    {
        XtiPath xtiPath;
        try
        {
            xtiPath = XtiPath.Parse(path);
        }
        catch
        {
            xtiPath = new XtiPath(AppKey.Unknown);
        }
        if (string.IsNullOrWhiteSpace(xtiPath.Group))
        {
            xtiPath = xtiPath.WithGroup("Home");
        }
        if (string.IsNullOrWhiteSpace(xtiPath.Action))
        {
            xtiPath = xtiPath.WithAction("Index");
        }
        var resourceGroup = await installation.ResourceGroupOrDefault(xtiPath.Group, ct);
        var resource = await resourceGroup.ResourceOrDefault(xtiPath.Action, ct);
        var modCategory = await resourceGroup.ModCategory(ct);
        var modifier = await modCategory.ModifierByModKeyOrDefault(xtiPath.Modifier, ct);
        var truncatedPath = new TruncatedText(path, 100).Value;
        requestData = new TruncatedText(requestData, 5000).Value;
        resultData = new TruncatedText(resultData, 5000).Value;
        var record = await GetRequestEntityByKey(requestKey, ct);
        if (record == null)
        {
            record = await Add
            (
                session,
                requestKey,
                installation,
                resource,
                modifier,
                truncatedPath,
                timeStarted,
                timeEnded,
                actualCount,
                requestData,
                resultData,
                ct
            );
        }
        else
        {
            await factory.DB
                .Requests
                .Update
                (
                    record,
                    r =>
                    {
                        r.SessionID = session.ID;
                        r.InstallationID = installation.ID;
                        r.ResourceID = resource.ID;
                        r.ModifierID = modifier.ID;
                        r.Path = truncatedPath;
                        if (timeStarted < r.TimeStarted)
                        {
                            r.TimeStarted = timeStarted;
                        }
                        if (timeEnded.Year < 9999)
                        {
                            r.TimeEnded = timeEnded;
                        }
                        r.ActualCount = actualCount;
                        r.RequestData = requestData;
                        r.ResultData = resultData;
                    },
                    ct
                );
        }
        var request = factory.CreateRequest(record);
        if (!string.IsNullOrWhiteSpace(sourceRequestKey))
        {
            var sourceRequest = await RequestOrPlaceHolder(sourceRequestKey, timeStarted, ct);
            await AddSourceLinkIfNotExists(request, sourceRequest, ct);
        }
        return request;
    }

    public async Task<AppRequest> RequestOrPlaceHolder(string requestKey, DateTimeOffset now, CancellationToken ct)
    {
        var requestEntity = await GetRequestEntityByKey(requestKey, ct);
        if (requestEntity == null)
        {
            var session = await factory.Sessions.DefaultSession(now, ct);
            var app = await factory.Apps.App(AppKey.Unknown, ct);
            var currentVersion = await app.CurrentVersion(ct);
            var installation = await factory.InstallLocations.AddUnknownIfNotFound(currentVersion, ct);
            var resourceGroup = await installation.ResourceGroupOrDefault(ResourceGroupName.Unknown, ct);
            var resource = await resourceGroup.ResourceOrDefault(ResourceName.Unknown, ct);
            var modifier = await app.DefaultModifier(ct);
            if (string.IsNullOrWhiteSpace(requestKey))
            {
                requestKey = new GeneratedKey().Value();
            }
            requestEntity = await Add
            (
                session,
                requestKey,
                installation,
                resource,
                modifier,
                path: "",
                timeStarted: now,
                timeEnded: now,
                actualCount: 1,
                requestData: "",
                resultData: "",
                ct: ct
            );
        }
        return factory.CreateRequest(requestEntity);
    }

    private async Task AddSourceLinkIfNotExists(AppRequest request, AppRequest sourceRequest, CancellationToken ct)
    {
        var linkExists = await factory.DB.SourceRequests.Retrieve()
            .Where(src => src.SourceID == sourceRequest.ID && src.TargetID == request.ID)
            .AnyAsync(ct);
        if (!linkExists)
        {
            await factory.DB.SourceRequests.Create
            (
                new SourceRequestEntity
                {
                    SourceID = sourceRequest.ID,
                    TargetID = request.ID
                },
                ct
            );
        }
    }

    private Task<AppRequestEntity?> GetRequestEntityByKey(string requestKey, CancellationToken ct) =>
        factory.DB.Requests.Retrieve().FirstOrDefaultAsync(r => r.RequestKey == requestKey, ct);

    private async Task<AppRequestEntity> Add
    (
        AppSession session,
        string requestKey,
        Installation installation,
        Resource resource,
        Modifier modifier,
        string path,
        DateTimeOffset timeStarted,
        DateTimeOffset timeEnded,
        int actualCount,
        string requestData,
        string resultData,
        CancellationToken ct
    )
    {
        var record = new AppRequestEntity
        {
            SessionID = session.ID,
            RequestKey = requestKey,
            InstallationID = installation.ID,
            ResourceID = resource.ID,
            ModifierID = modifier.ID,
            Path = path ?? "",
            TimeStarted = timeStarted,
            TimeEnded = timeEnded,
            ActualCount = actualCount,
            RequestData = requestData,
            ResultData = resultData
        };
        await factory.DB.Requests.Create(record, ct);
        return record;
    }

    internal Task<AppRequest[]> RetrieveBySession(AppSession session, CancellationToken ct)
        => factory.DB.Requests
            .Retrieve()
            .Where(r => r.SessionID == session.ID)
            .Select(r => factory.CreateRequest(r))
            .ToArrayAsync(ct);

    internal Task<AppRequest[]> RetrieveMostRecent(AppSession session, int howMany, CancellationToken ct) =>
        factory.DB.Requests
            .Retrieve()
            .Where(r => r.SessionID == session.ID)
            .OrderByDescending(r => r.TimeStarted)
            .Take(howMany)
            .Select(r => factory.CreateRequest(r))
            .ToArrayAsync(ct);

    internal Task<AppRequest[]> MostRecentForInstallation(Installation installation, int howMany, CancellationToken ct) =>
        factory.DB.Requests.Retrieve()
            .Where(r => r.InstallationID == installation.ID)
            .OrderByDescending(r => r.TimeStarted)
            .Take(howMany)
            .Select(r => factory.CreateRequest(r))
            .ToArrayAsync(ct);

    internal async Task<AppRequestExpandedModel[]> MostRecentForVersion(App app, XtiVersion version, int howMany, CancellationToken ct)
    {
        var appVersionIDs = factory.Versions.QueryAppVersionID(app, version);
        var resources = factory.DB
            .Resources
            .Retrieve()
            .Join
            (
                factory.DB
                    .ResourceGroups
                    .Retrieve()
                    .Where(rg => appVersionIDs.Contains(rg.AppVersionID)),
                res => res.GroupID,
                rg => rg.ID,
                (res, rg) => new ResourceWithGroupRecord
                {
                    ResourceID = res.ID,
                    ActionName = res.Name,
                    GroupID = rg.ID,
                    GroupName = rg.Name,
                    ResultType = ResourceResultType.Values.Value(res.ResultType)
                }
            );
        var requests = await RequestsWithResources(howMany, resources, ct);
        return requests;
    }

    internal async Task<AppRequestExpandedModel[]> MostRecentForResourceGroup(ResourceGroup group, int howMany, CancellationToken ct)
    {
        var resources = factory.DB
            .Resources
            .Retrieve()
            .Join
            (
                factory.DB
                    .ResourceGroups
                    .Retrieve()
                    .Where(rg => rg.ID == group.ID),
                res => res.GroupID,
                rg => rg.ID,
                (res, rg) => new ResourceWithGroupRecord
                {
                    ResourceID = res.ID,
                    ActionName = res.Name,
                    GroupID = rg.ID,
                    GroupName = rg.Name,
                    ResultType = ResourceResultType.Values.Value(res.ResultType)
                }
            );
        var requests = await RequestsWithResources(howMany, resources, ct);
        return requests;
    }

    internal async Task<AppRequestExpandedModel[]> MostRecentForResource(Resource resource, int howMany, CancellationToken ct)
    {
        var resources = factory.DB
            .Resources
            .Retrieve()
            .Where(r => r.ID == resource.ID)
            .Join
            (
                factory.DB
                    .ResourceGroups
                    .Retrieve(),
                res => res.GroupID,
                rg => rg.ID,
                (res, rg) => new ResourceWithGroupRecord
                {
                    ResourceID = res.ID,
                    ActionName = res.Name,
                    GroupID = rg.ID,
                    GroupName = rg.Name,
                    ResultType = ResourceResultType.Values.Value(res.ResultType)
                }
            );
        var requests = await RequestsWithResources(howMany, resources, ct);
        return requests;
    }

    private Task<AppRequestExpandedModel[]> RequestsWithResources(int howMany, IQueryable<ResourceWithGroupRecord> resources, CancellationToken ct)
    {
        return factory.DB
            .Requests
            .Retrieve()
            .Join
            (
                resources,
                req => req.ResourceID,
                res => res.ResourceID,
                (req, res) => new
                {
                    req.ID,
                    req.SessionID,
                    res.GroupName,
                    res.ActionName,
                    req.TimeStarted,
                    req.TimeEnded
                }
            )
            .Join
            (
                factory.DB
                    .Sessions
                    .Retrieve()
                    .Join
                    (
                        factory.DB
                            .Users
                            .Retrieve(),
                        s => s.UserID,
                        u => u.ID,
                        (s, u) => new
                        {
                            SessionID = s.ID,
                            u.UserName
                        }
                    ),
                req => req.SessionID,
                s => s.SessionID,
                (req, s) => new
                {
                    req.ID,
                    s.UserName,
                    req.GroupName,
                    req.ActionName,
                    req.TimeStarted,
                    req.TimeEnded
                }
            )
            .OrderByDescending(r => r.TimeStarted)
            .Take(howMany)
            .Select
            (
                r => new AppRequestExpandedModel
                {
                    ID = r.ID,
                    UserName = r.UserName,
                    GroupName = new ResourceGroupName(r.GroupName).DisplayText,
                    ActionName = new ResourceName(r.ActionName).DisplayText,
                    TimeStarted = r.TimeStarted,
                    TimeEnded = r.TimeEnded
                }
            )
            .ToArrayAsync(ct);
    }

    internal async Task<AppRequest> SourceRequestOrDefault(int forTargetID, CancellationToken ct)
    {
        var sourceIDs = factory.DB.SourceRequests.Retrieve()
            .Where(src => src.TargetID == forTargetID)
            .Select(src => src.SourceID);
        var entities = await factory.DB
            .Requests.Retrieve()
            .Where(e => sourceIDs.Contains(e.ID))
            .ToArrayAsync(ct);
        return factory.CreateRequest(entities.FirstOrDefault() ?? new());
    }

    internal Task<int[]> TargetRequestIDs(int forSourceID, CancellationToken ct) =>
        factory.DB.SourceRequests.Retrieve()
            .Where(src => src.SourceID == forSourceID)
            .Select(src => src.TargetID)
            .ToArrayAsync(ct);

    private sealed class ResourceWithGroupRecord
    {
        public int ResourceID { get; set; }
        public string ActionName { get; set; } = "";
        public int GroupID { get; set; }
        public string GroupName { get; set; } = "";
        public ResourceResultType ResultType { get; set; } = ResourceResultType.Values.None;
    }
}