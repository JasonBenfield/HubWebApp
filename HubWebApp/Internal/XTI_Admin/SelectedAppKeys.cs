using XTI_App.Abstractions;

namespace XTI_Admin;

public sealed class SelectedAppKeys
{
    public SelectedAppKeys(AdminOptions options, PublishableFolder publishableFolder)
    {
        if (options.AppKey().Equals(AppKey.Unknown))
        {
            Values = publishableFolder.AppKeys();
        }
        else
        {
            Values = new[] { options.AppKey() };
        }
    }

    public AppKey[] Values { get; }
}