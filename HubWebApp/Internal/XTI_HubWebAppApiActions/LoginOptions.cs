namespace XTI_HubWebAppApiActions;

public sealed class LoginOptions
{
    public string DefaultReturnUrl { get; set; } = "~/";
    public int DaysBeforeDeactivation { get; set; } = 90;
}
