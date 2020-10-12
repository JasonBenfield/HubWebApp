namespace HubWebApp.AuthApi
{
    public sealed class UserNotFoundException : LoginFailedException
    {
        public UserNotFoundException(string userName) : base($"{userName} was not found")
        {
        }
    }
}
