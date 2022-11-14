namespace XTI_HubDB.Entities;

public sealed class AuthenticatorEntity
{
    public int ID { get; set; }
    public string AuthenticatorKey { get; set; } = "";
    public string AuthenticatorName { get; set; } = "";
}