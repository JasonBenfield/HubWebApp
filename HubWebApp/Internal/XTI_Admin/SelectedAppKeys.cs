using XTI_App.Abstractions;

namespace XTI_Admin;

public sealed class SelectedAppKeys
{
    private readonly AdminOptions options;
    private readonly SlnFolder slnFolder;

    public SelectedAppKeys(AdminOptions options, SlnFolder slnFolder)
    {
        this.options = options;
        this.slnFolder = slnFolder;
    }

    public AppKey[] Values()
    {
        AppKey[] values;
        if (options.AppKey().Equals(AppKey.Unknown))
        {
            values = slnFolder.AppKeys();
        }
        else
        {
            values = [options.AppKey()];
        }
        return values;
    }
}