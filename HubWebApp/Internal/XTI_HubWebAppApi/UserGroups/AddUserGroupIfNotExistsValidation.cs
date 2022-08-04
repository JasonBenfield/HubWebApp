using XTI_Core;

namespace XTI_HubWebAppApi.UserGroups;

internal sealed class AddUserGroupIfNotExistsValidation : AppActionValidation<AddUserGroupIfNotExistsRequest>
{
    public Task Validate(ErrorList errors, AddUserGroupIfNotExistsRequest model, CancellationToken stoppingToken)
    {
        if (string.IsNullOrWhiteSpace(model.GroupName))
        {
            errors.Add("Group Name is required.");
        }
        return Task.CompletedTask;
    }
}
