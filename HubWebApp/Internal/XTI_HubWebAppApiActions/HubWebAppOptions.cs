namespace XTI_HubWebAppApiActions;

public sealed class HubWebAppOptions
{
    public LoginOptions Login { get; set; } = new();
    public StorageOptions Storage { get; set; } = new();
}
