namespace XTI_HubWebAppApiActions;

public sealed class AuthenticatedModel
{
    public AuthenticatedModel()
        : this(AppUserName.Anon, "")
    {
    }

    public AuthenticatedModel(AppUserName userName, string authID)
    {
        UserName = userName.Value;
        AuthID = authID;
    }

    public string UserName { get; set; }
    public string AuthID { get; set; }
}
