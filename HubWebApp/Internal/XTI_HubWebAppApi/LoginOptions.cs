namespace XTI_HubWebAppApi;

public sealed class LoginOptions
{
    public static readonly string Login = nameof(Login);
    public string DefaultReturnUrl { get; set; } = "~/";
}
