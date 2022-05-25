using Microsoft.EntityFrameworkCore;
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

    internal async Task<AppRequest> AddOrUpdate
    (
        AppSession session, 
        string requestKey, 
        Installation installation, 
        string path, 
        DateTimeOffset timeRequested, 
        int actualCount
    )
    {
        XtiPath xtiPath;
        try
        {
            xtiPath = XtiPath.Parse(path);
        }
        catch
        {
            xtiPath = new XtiPath(AppName.Unknown.Value);
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
        var record = await factory.DB.Requests.Retrieve()
            .FirstOrDefaultAsync(r => r.RequestKey == requestKey);
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
                timeRequested,
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
                        r.TimeStarted = timeRequested;
                        r.ActualCount = actualCount;
                    }
                );
        }
        return factory.CreateRequest(record);
    }

    public async Task<AppRequest> RequestOrPlaceHolder(string requestKey, DateTimeOffset now)
    {
        var requestEntity = await factory.DB.Requests.Retrieve()
            .FirstOrDefaultAsync(r => r.RequestKey == requestKey);
        if (requestEntity == null)
        {
            var session = await factory.Sessions.DefaultSession(now);
            var app = await factory.Apps.App(AppKey.Unknown);
            var currentVersion = await app.CurrentVersion();
            var installation = await factory.InstallLocations.AddUnknownIfNotFound(currentVersion);
            var resourceGroup = await installation.ResourceGroupOrDefault(ResourceGroupName.Unknown);
            var resource = await resourceGroup.ResourceOrDefault(ResourceName.Unknown);
            var modifier = await app.DefaultModifier();
            requestEntity = await Add
            (
                session,
                string.IsNullOrWhiteSpace(requestKey) ? new GeneratedKey().Value() : requestKey,
                installation,
                resource,
                modifier,
                "",
                now,
                1
            );
        }
        return factory.CreateRequest(requestEntity);
    }

    private async Task<AppRequestEntity> Add(AppSession session, string requestKey, Installation installation, Resource resource, Modifier modifier, string path, DateTimeOffset timeRequested, int actualCount)
    {
        var record = new AppRequestEntity
        {
            SessionID = session.ID,
            RequestKey = requestKey,
            InstallationID = installation.ID,
            ResourceID = resource.ID,
            ModifierID = modifier.ID,
            Path = path ?? "",
            TimeStarted = timeRequested,
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
        var requests = await requestsWithResources(howMany, resources);
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
        var requests = await requestsWithResources(howMany, resources);
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
        var requests = await requestsWithResources(howMany, resources);
        return requests;
    }

    private Task<AppRequestExpandedModel[]> requestsWithResources(int howMany, IQueryable<ResourceWithGroupRecord> resources)
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
                    ID = req.ID,
                    SessionID = req.SessionID,
                    GroupName = res.GroupName,
                    ActionName = res.ActionName,
                    TimeStarted = req.TimeStarted,
                    TimeEnded = req.TimeEnded
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
                    ID = req.ID,
                    UserName = s.UserName,
                    GroupName = req.GroupName,
                    ActionName = req.ActionName,
                    TimeStarted = req.TimeStarted,
                    TimeEnded = req.TimeEnded
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

    private sealed class ResourceWithGroupRecord
    {
        public int ResourceID { get; set; }
        public string ActionName { get; set; } = "";
        public int GroupID { get; set; }
        public string GroupName { get; set; } = "";
        public ResourceResultType ResultType { get; set; } = ResourceResultType.Values.None;
    }
}