namespace XTI_HubAppApi.Auth;

public sealed class UserNotFoundException : LoginFailedException
{
    public UserNotFoundException(string userName) 
        : base($"{userName} was not found")
    {
    }
}