namespace XTI_Hub.Abstractions;

public sealed record UserGroupKey(string UserGroupName)
{
    public UserGroupKey()
        : this("")
    {
    }
}