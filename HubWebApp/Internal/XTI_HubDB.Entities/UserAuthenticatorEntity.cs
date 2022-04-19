namespace XTI_HubDB.Entities;

public sealed class UserAuthenticatorEntity
{
    public int ID { get; set; }
    public int UserID { get; set; }
    public int AuthenticatorID { get; set; }
    public string ExternalUserKey { get; set; } = "";
}