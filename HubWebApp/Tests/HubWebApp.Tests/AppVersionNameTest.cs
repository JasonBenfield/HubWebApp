using XTI_Core;
using XTI_Hub.Abstractions;

namespace HubWebApp.Tests;

internal sealed class AppVersionNameTest
{
    [Test]
    public void ShouldDeserialize()
    {
        var versionName = new AppVersionName("whatever");
        var serialized = XtiSerializer.Serialize(versionName);
        var deserialized = XtiSerializer.Deserialize<AppVersionName>(serialized);
        Assert.That(deserialized, Is.EqualTo(versionName));
    }
}
