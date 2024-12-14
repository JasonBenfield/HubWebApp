using Microsoft.AspNetCore.OData.Query;
using XTI_HubDB.Entities;
using XTI_ODataQuery.Api;

namespace XTI_HubWebAppApiActions.Installations;

public sealed class InstallationQueryAction : QueryAction<InstallationQueryRequest, ExpandedInstallation>
{
    private readonly CurrentAppUser currentUser;
    private readonly IHubDbContext db;

    public InstallationQueryAction(CurrentAppUser currentUser, IHubDbContext db)
    {
        this.currentUser = currentUser;
        this.db = db;
    }

    public async Task<IQueryable<ExpandedInstallation>> Execute(ODataQueryOptions<ExpandedInstallation> options, InstallationQueryRequest model)
    {
        var appPermissions = await currentUser.GetAppPermissions();
        var appIDs = appPermissions
            .Where(p => p.CanView)
            .Select(p => p.App.ToModel().ID)
            .ToArray();
        var query = db.ExpandedInstallations.Retrieve()
            .Where(installation => appIDs.Contains(installation.AppID));
        if (model.QueryType.Equals(InstallationQueryType.Values.Installed))
        {
            query = query.Where(installation => installation.InstallationStatus == "Installed");
        }
        else if (model.QueryType.Equals(InstallationQueryType.Values.Current))
        {
            query = query
                .Where
                (
                    installation => 
                        installation.InstallationStatus == "Installed" && 
                        installation.IsCurrent
                );
        }
        else if (model.QueryType.Equals(InstallationQueryType.Values.UpdateAvailable))
        {
            query = query
                .Where
                (
                    installation => installation.InstallationStatus == "Installed" && 
                    installation.IsCurrent && 
                    installation.VersionStatus != "Current"
                );
        }
        return query;
    }
}
