namespace XTI_Hub.Abstractions;

public sealed class AppUserModel
{
    public int ID { get; set; }
    public string UserName { get; set; } = "";
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
}