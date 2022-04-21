using XTI_Hub.Abstractions;

namespace XTI_Admin;

internal sealed class AppVersionNameAccessor
{
    public AppVersionNameAccessor()
    {
        var value = new DirectoryInfo(Environment.CurrentDirectory).Name;
        Value = new AppVersionName(value);
    }

    public AppVersionName Value { get; }
}
