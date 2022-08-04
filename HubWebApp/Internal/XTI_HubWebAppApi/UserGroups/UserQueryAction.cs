using Microsoft.AspNetCore.OData.Query;
using XTI_HubDB.Entities;
using XTI_ODataQuery.Api;

namespace XTI_HubWebAppApi.UserGroups;

public sealed class ExpandedUser
{
    public int UserID { get; set; }
    public string UserName { get; set; } = "";
    public string PersonName { get; set; } = "";
    public string Email { get; set; } = "";
    public DateTimeOffset TimeUserAdded { get; set; }
    public int UserGroupID { get; set; }
    public string UserGroupName { get; set; } = "";
}

internal sealed class UserQueryAction : QueryAction<UserGroupKey, ExpandedUser>
{
    private readonly CurrentUser currentUser;
    private readonly IHubDbContext db;

    public UserQueryAction(CurrentUser currentUser, IHubDbContext db)
    {
        this.currentUser = currentUser;
        this.db = db;
    }

    public async Task<IQueryable<ExpandedUser>> Execute(ODataQueryOptions<ExpandedUser> options, UserGroupKey model)
    {
        var user = await currentUser.Value();
        var userPermissions = await user.GetUserGroupPermissions();
        var userGroupModels = userPermissions
            .Where(p => p.CanView)
            .Select(p => p.UserGroup.ToModel());
        var userGroupIDs = userGroupModels
            .Where
            (
                ug =>
                    string.IsNullOrWhiteSpace(model.UserGroupName)
                    || ug.GroupName.Equals(model.UserGroupName)
            )
            .Select(ug => ug.ID)
            .ToArray();
        return from u in db.Users.Retrieve()
               where userGroupIDs.Contains(u.GroupID)
               join ug in db.UserGroups.Retrieve()
               on u.GroupID equals ug.ID
               select new ExpandedUser
               {
                   UserID = u.ID,
                   UserName = u.UserName,
                   PersonName = u.Name,
                   Email = u.Email,
                   TimeUserAdded = u.TimeAdded,
                   UserGroupID = u.GroupID,
                   UserGroupName = ug.DisplayText
               };
    }
}
