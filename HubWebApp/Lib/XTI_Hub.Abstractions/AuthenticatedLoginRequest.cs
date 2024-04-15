namespace XTI_Hub.Abstractions;

public sealed class AuthenticatedLoginRequest
{
    public AuthenticatedLoginRequest()
        :this("", "", "")
    {   
    }

    public AuthenticatedLoginRequest(string authKey, string authID, string returnKey)
    {
        AuthKey = authKey;
        AuthID = authID;
        ReturnKey = returnKey;
    }

    public string AuthKey { get; set; }
    public string AuthID { get; set; }
    public string ReturnKey { get; set; }
}