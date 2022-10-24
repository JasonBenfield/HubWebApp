namespace XTI_Hub.Abstractions;

public sealed class AddOrUpdateUserRequest
{
    public string UserName { get; set; } = "";
    public string Password { get; set; } = "";
    public string PersonName { get; set; } = "";
    public string Email { get; set; } = "";
}