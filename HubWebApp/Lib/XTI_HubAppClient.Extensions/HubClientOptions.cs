namespace XTI_HubAppClient.Extensions;

public sealed class HubClientOptions
{
    public static readonly string HubClient = nameof(HubClient);

    public string Domain { get; set; } = "";
}