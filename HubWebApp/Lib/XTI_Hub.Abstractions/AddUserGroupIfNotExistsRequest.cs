namespace XTI_Hub.Abstractions;

public sealed class AddUserGroupIfNotExistsRequest
{
    public AddUserGroupIfNotExistsRequest()
        :this("")
    {    
    }

    public AddUserGroupIfNotExistsRequest(string groupName)
    {
        GroupName = groupName;
    }

    public string GroupName { get; set; }
}
