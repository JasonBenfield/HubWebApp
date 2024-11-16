using Microsoft.EntityFrameworkCore;
using System.Globalization;
using XTI_App.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class AppRequestRepository
{
    private readonly HubFactory factory;

    internal AppRequestRepository(HubFactory factory)
    {
        this.factory = factory;
    }

    public async Task<AppRequest> Request(int id)
    {
        var entity = await factory.DB.Requests.Retrieve()
            .Where(r => r.ID == id)
            .FirstOrDefaultAsync();
        return factory.CreateRequest(entity ?? throw new Exception($"Request not found with ID {id}"));
    }

    public async Task<AppRequest> RequestOrDefault(string requestKey)
    {
        var entity = await factory.DB.Requests.Retrieve()
            .Where(r => r.RequestKey == requestKey)
            .FirstOrDefaultAsync();
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
        string sourceRequestKey
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
        var resourceGroup = await installation.ResourceGroupOrDefault(xtiPath.Group);
        var resource = await resourceGroup.ResourceOrDefault(xtiPath.Action);
        var modCategory = await resourceGroup.ModCategory();
        var modifier = await modCategory.ModifierByModKeyOrDefault(xtiPath.Modifier);
        var record = await GetRequestEntityByKey(requestKey);
        if (record == null)
        {
            record = await Add
            (
                session,
                requestKey,
                installation,
                resource,
                modifier,
                path,
                timeStarted,
                timeEnded,
                actualCount
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
                        r.Path = path;
                        if (timeStarted < r.TimeStarted)
                        {
                            r.TimeStarted = timeStarted;
                        }
                        if (timeEnded.Year < 9999)
                        {
                            r.TimeEnded = timeEnded;
                        }
                        r.ActualCount = actualCount;
                    }
                );
        }
        var request = factory.CreateRequest(record);
        if (!string.IsNullOrWhiteSpace(sourceRequestKey))
        {
            var sourceRequest = await RequestOrPlaceHolder(sourceRequestKey, timeStarted);
            await AddSourceLinkIfNotExists(request, sourceRequest);
        }
        return request;
    }

    public async Task<AppRequest> RequestOrPlaceHolder(string requestKey, DateTimeOffset now)
    {
        var requestEntity = await GetRequestEntityByKey(requestKey);
        if (requestEntity == null)
        {
            var session = await factory.Sessions.DefaultSession(now);
            var app = await factory.Apps.App(AppKey.Unknown);
            var currentVersion = await app.CurrentVersion();
            var installation = await factory.InstallLocations.AddUnknownIfNotFound(currentVersion);
            var resourceGroup = await installation.ResourceGroupOrDefault(ResourceGroupName.Unknown);
            var resource = await resourceGroup.ResourceOrDefault(ResourceName.Unknown);
            var modifier = await app.DefaultModifier();
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
                actualCount: 1
            );
        }
        return factory.CreateRequest(requestEntity);
    }

    private async Task AddSourceLinkIfNotExists(AppRequest request, AppRequest sourceRequest)
    {
        var linkExists = await factory.DB.SourceRequests.Retrieve()
            .Where(src => src.SourceID == sourceRequest.ID && src.TargetID == request.ID)
            .AnyAsync();
        if (!linkExists)
        {
            await factory.DB.SourceRequests.Create
            (
                new SourceRequestEntity
                {
                    SourceID = sourceRequest.ID,
                    TargetID = request.ID
                }
            );
        }
    }

    private Task<AppRequestEntity?> GetRequestEntityByKey(string requestKey) =>
        factory.DB.Requests.Retrieve().FirstOrDefaultAsync(r => r.RequestKey == requestKey);

    private async Task<AppRequestEntity> Add(AppSession session, string requestKey, Installation installation, Resource resource, Modifier modifier, string path, DateTimeOffset timeStarted, DateTimeOffset timeEnded, int actualCount)
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
            ActualCount = actualCount
        };
        await factory.DB.Requests.Create(record);
        return record;
    }

    internal Task<AppRequest[]> RetrieveBySession(AppSession session)
        => factory.DB.Requests
            .Retrieve()
            .Where(r => r.SessionID == session.ID)
            .Select(r => factory.CreateRequest(r))
            .ToArrayAsync();

    internal Task<AppRequest[]> RetrieveMostRecent(AppSession session, int howMany)
    {
        return factory.DB.Requests
            .Retrieve()
            .Where(r => r.SessionID == session.ID)
            .OrderByDescending(r => r.TimeStarted)
            .Take(howMany)
            .Select(r => factory.CreateRequest(r))
            .ToArrayAsync();
    }

    internal Task<AppRequest[]> MostRecentForInstallation(Installation installation, int howMany) =>
        factory.DB.Requests.Retrieve()
            .Where(r => r.InstallationID == installation.ID)
            .OrderByDescending(r => r.TimeStarted)
            .Take(howMany)
            .Select(r => factory.CreateRequest(r))
            .ToArrayAsync();

    internal async Task<AppRequestExpandedModel[]> MostRecentForVersion(App app, XtiVersion version, int howMany)
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
        var requests = await RequestsWithResources(howMany, resources);
        return requests;
    }

    internal async Task<AppRequestExpandedModel[]> MostRecentForResourceGroup(ResourceGroup group, int howMany)
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
        var requests = await RequestsWithResources(howMany, resources);
        return requests;
    }

    internal async Task<AppRequestExpandedModel[]> MostRecentForResource(Resource resource, int howMany)
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
        var requests = await RequestsWithResources(howMany, resources);
        return requests;
    }

    private Task<AppRequestExpandedModel[]> RequestsWithResources(int howMany, IQueryable<ResourceWithGroupRecord> resources)
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
            .ToArrayAsync();
    }

    internal async Task<AppRequest> SourceRequestOrDefault(int forTargetID)
    {
        var sourceIDs = factory.DB.SourceRequests.Retrieve()
            .Where(src => src.TargetID == forTargetID)
            .Select(src => src.SourceID);
        var entities = await factory.DB
            .Requests.Retrieve()
            .Where(e => sourceIDs.Contains(e.ID))
            .ToArrayAsync();
        return factory.CreateRequest(entities.FirstOrDefault() ?? new());
    }

    internal Task<int[]> TargetRequestIDs(int forSourceID) =>
        factory.DB.SourceRequests.Retrieve()
            .Where(src => src.SourceID == forSourceID)
            .Select(src => src.TargetID)
            .ToArrayAsync();

    private sealed class ResourceWithGroupRecord
    {
        public int ResourceID { get; set; }
        public string ActionName { get; set; } = "";
        public int GroupID { get; set; }
        public string GroupName { get; set; } = "";
        public ResourceResultType ResultType { get; set; } = ResourceResultType.Values.None;
    }
}