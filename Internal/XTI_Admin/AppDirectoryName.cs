using XTI_App.Abstractions;

namespace XTI_Admin;

internal sealed class AppDirectoryName
{
    public AppDirectoryName(AppKey appKey)
    {
        var appType = appKey.Type.Equals(AppType.Values.Package) ? "" : appKey.Type.DisplayText;
        Value = $"{appKey.Name.DisplayText}{appType}".Replace(" ", "");
    }

    public string Value { get; }
}
