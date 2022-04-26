using XTI_App.Abstractions;
using XTI_App.Api;

namespace XTI_Hub;

public sealed class PublishException : AppException
{
    public static PublishException Publishing(AppVersionKey versionKey, AppVersionStatus status) =>
        new PublishException($"Unable to begin publishing version '{versionKey.DisplayText}' when it's status is '{status.DisplayText}");

    public static PublishException Published(AppVersionKey versionKey, AppVersionStatus status) =>
        new PublishException($"Unable to publish version '{versionKey.DisplayText} when it's status is '{status.DisplayText}'");

    public PublishException(string message)
        : base(message, "Invalid publish state")
    {
    }
}