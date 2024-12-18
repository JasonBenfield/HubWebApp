namespace XTI_HubWebAppApiActions;

public sealed class UserNotFoundException : LoginFailedException
{
    public UserNotFoundException(string userName) 
        : base($"{userName} was not found")
    {
    }
}