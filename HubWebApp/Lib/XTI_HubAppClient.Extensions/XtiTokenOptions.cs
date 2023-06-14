namespace XTI_HubAppClient.Extensions;

public sealed class XtiTokenOptions
{
    public static readonly string XtiToken = nameof(XtiToken);

    public string UserName { get; set; } = "";
    public string Password { get; set; } = "";
}
