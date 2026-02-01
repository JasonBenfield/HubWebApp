using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;
using XTI_Core;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class EfAppRequests
{
    private readonly EfHubDB db;

    internal EfAppRequests(EfHubDB db)
    {
        this.db = db;
    }

    public async Task<EfAppRequest> Request(int id, CancellationToken ct)
    {
        var request = await db.Context.Requests.Retrieve()
            .Where(r => r.ID == id)
            .FirstOrDefaultAsync(ct);
        return new EfAppRequest(db, request ?? throw new Exception($"Request not found with ID {id}"));
    }

    public async Task<EfAppRequest> RequestOrDefault(string requestKey, CancellationToken ct)
    {
        var request = await db.Context.Requests.Retrieve()
            .Where(r => r.RequestKey == requestKey)
            .FirstOrDefaultAsync(ct);
        return new EfAppRequest(db, request ?? new());
    }

    internal async Task<EfAppRequest> AddOrUpdate
    (
        EfAppSession session,
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
        var request = await GetRequestEntityByKey(requestKey, ct);
        if (request == null)
        {
            request = await Add
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
            await db.Context.Requests.Update
            (
                request,
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
        var efRequest = new EfAppRequest(db, request);
        if (!string.IsNullOrWhiteSpace(sourceRequestKey))
        {
            var efSourceRequest = await RequestOrPlaceHolder(sourceRequestKey, timeStarted, ct);
            await AddSourceLinkIfNotExists(efRequest, efSourceRequest, ct);
        }
        return efRequest;
    }

    public async Task<EfAppRequest> RequestOrPlaceHolder(string requestKey, DateTimeOffset now, CancellationToken ct)
    {
        var request = await GetRequestEntityByKey(requestKey, ct);
        if (request == null)
        {
            var efSession = await db.Sessions.DefaultSession(now, ct);
            var efApp = await db.Apps.App(AppKey.Unknown, ct);
            var efCurrentVersion = await efApp.CurrentVersion(ct);
            var efInstallation = await db.InstallLocations.AddUnknownIfNotFound(efCurrentVersion, ct);
            var efResourceGroup = await efInstallation.ResourceGroupOrDefault(ResourceGroupName.Unknown, ct);
            var efResource = await efResourceGroup.ResourceOrDefault(ResourceName.Unknown, ct);
            var efModifier = await efApp.DefaultModifier(ct);
            if (string.IsNullOrWhiteSpace(requestKey))
            {
                requestKey = new GeneratedKey().Value();
            }
            request = await Add
            (
                efSession,
                requestKey,
                efInstallation,
                efResource,
                efModifier,
                path: "",
                timeStarted: now,
                timeEnded: now,
                actualCount: 1,
                requestData: "",
                resultData: "",
                ct: ct
            );
        }
        return new EfAppRequest(db, request);
    }

    private async Task AddSourceLinkIfNotExists(EfAppRequest request, EfAppRequest sourceRequest, CancellationToken ct)
    {
        var linkExists = await db.Context.SourceRequests.Retrieve()
            .Where(src => src.SourceID == sourceRequest.ID && src.TargetID == request.ID)
            .AnyAsync(ct);
        if (!linkExists)
        {
            await db.Context.SourceRequests.Create
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
        db.Context.Requests.Retrieve()
            .Where(r => r.RequestKey == requestKey)
            .FirstOrDefaultAsync(ct);

    private async Task<AppRequestEntity> Add
    (
        EfAppSession session,
        string requestKey,
        EfInstallation installation,
        EfResource resource,
        EfModifier modifier,
        string path,
        DateTimeOffset timeStarted,
        DateTimeOffset timeEnded,
        int actualCount,
        string requestData,
        string resultData,
        CancellationToken ct
    )
    {
        var request = new AppRequestEntity
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
        await db.Context.Requests.Create(request, ct);
        return request;
    }

    internal Task<EfAppRequest[]> RetrieveBySession(EfAppSession session, CancellationToken ct)
        => db.Context.Requests.Retrieve()
            .Where(r => r.SessionID == session.ID)
            .Select(r => new EfAppRequest(db, r))
            .ToArrayAsync(ct);

    internal Task<EfAppRequest[]> RetrieveMostRecent(EfAppSession session, int howMany, CancellationToken ct) =>
        db.Context.Requests.Retrieve()
            .Where(r => r.SessionID == session.ID)
            .OrderByDescending(r => r.TimeStarted)
            .Take(howMany)
            .Select(r => new EfAppRequest(db, r))
            .ToArrayAsync(ct);

    internal Task<EfAppRequest[]> MostRecentForInstallation(EfInstallation installation, int howMany, CancellationToken ct) =>
        db.Context.Requests.Retrieve()
            .Where(r => r.InstallationID == installation.ID)
            .OrderByDescending(r => r.TimeStarted)
            .Take(howMany)
            .Select(r => new EfAppRequest(db, r))
            .ToArrayAsync(ct);

    internal async Task<AppRequestExpandedModel[]> MostRecentForVersion(EfApp app, EfVersion version, int howMany, CancellationToken ct)
    {
        var appVersionIDs = db.Versions.QueryAppVersionID(app, version);
        var resources = db.Context
            .Resources
            .Retrieve()
            .Join
            (
                db.Context.ResourceGroups.Retrieve()
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

    internal async Task<AppRequestExpandedModel[]> MostRecentForResourceGroup(EfResourceGroup group, int howMany, CancellationToken ct)
    {
        var resources = db.Context.Resources.Retrieve()
            .Join
            (
                db.Context
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

    internal async Task<AppRequestExpandedModel[]> MostRecentForResource(EfResource resource, int howMany, CancellationToken ct)
    {
        var resources = db.Context.Resources.Retrieve()
            .Where(r => r.ID == resource.ID)
            .Join
            (
                db.Context
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
        return db.Context.Requests.Retrieve()
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
                db.Context.Sessions.Retrieve()
                    .Join
                    (
                        db.Context
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

    internal async Task<EfAppRequest> SourceRequestOrDefault(int forTargetID, CancellationToken ct)
    {
        var sourceIDs = db.Context.SourceRequests.Retrieve()
            .Where(src => src.TargetID == forTargetID)
            .Select(src => src.SourceID);
        var requests = await db.Context
            .Requests.Retrieve()
            .Where(e => sourceIDs.Contains(e.ID))
            .ToArrayAsync(ct);
        return new EfAppRequest(db, requests.FirstOrDefault() ?? new());
    }

    internal Task<int[]> TargetRequestIDs(int forSourceID, CancellationToken ct) =>
        db.Context.SourceRequests.Retrieve()
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