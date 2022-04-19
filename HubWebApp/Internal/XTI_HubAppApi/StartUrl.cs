using System.Web;

namespace XTI_HubAppApi;

internal sealed class StartUrl
{
    public StartUrl(string startUrl, string returnUrl)
    {
        if (string.IsNullOrWhiteSpace(startUrl))
        {
            startUrl = "~/User";
        }
        else
        {
            startUrl = HttpUtility.UrlDecode(startUrl);
        }
        if (!string.IsNullOrWhiteSpace(returnUrl))
        {
            if (startUrl.Contains("?"))
            {
                startUrl += "&";
            }
            else
            {
                startUrl += "?";
            }
            startUrl += $"returnUrl={returnUrl}";
        }
        Value = startUrl;
    }

    public string Value { get; }
}