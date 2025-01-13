using XTI_Core;

namespace XTI_HubWebAppApiActions.AppUserMaintenance;

public sealed class EditUserGroupValidation : AppActionValidation<EditUserGroupRequest>
{
    public Task Validate(ErrorList errors, EditUserGroupRequest editRequest, CancellationToken stoppingToken)
    {
        if(editRequest.UserID <= 0)
        {
            errors.Add(new ErrorModel(UserErrors.UserIDIsRequired));
        }
        if(editRequest.UserGroupID <= 0)
        {
            errors.Add(new ErrorModel(UserErrors.UserGroupIDIsRequired));
        }
        return Task.CompletedTask;
    }
}
