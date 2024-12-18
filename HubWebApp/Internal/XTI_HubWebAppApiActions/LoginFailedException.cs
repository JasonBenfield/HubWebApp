namespace XTI_HubWebAppApiActions;

public class LoginFailedException : AppException
{
    protected LoginFailedException(string message) 
        : base(message, "User name or Password was incorrect")
    {
    }
}