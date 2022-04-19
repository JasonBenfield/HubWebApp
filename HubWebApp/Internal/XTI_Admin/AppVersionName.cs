namespace XTI_Admin;

internal sealed class AppVersionName
{
    public AppVersionName()
    {
        Value = new DirectoryInfo(Environment.CurrentDirectory).Name;
    }

    public string Value { get; }
}
