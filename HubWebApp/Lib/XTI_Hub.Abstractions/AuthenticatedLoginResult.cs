namespace XTI_Hub.Abstractions;

public sealed record AuthenticatedLoginResult(string AuthKey, string AuthID)
{
    public AuthenticatedLoginResult()
        : this("", "")
    {
    }
}
