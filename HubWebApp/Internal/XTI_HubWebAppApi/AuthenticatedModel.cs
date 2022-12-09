namespace XTI_HubWebAppApi;

public sealed class AuthenticatedModel
{
    public AuthenticatedModel()
        : this(AppUserName.Anon)
    {
    }

    public AuthenticatedModel(AppUserName userName)
    {
        UserName = userName.Value;
    }

    public string UserName { get; set; }
}
