using XTI_Core;

namespace XTI_SupportServiceAppApi;

partial class SupportAppApiBuilder
{
    partial void Configure()
    {
        source.SerializedDefaultOptions = XtiSerializer.Serialize(new SupportServiceAppOptions());
    }
}
