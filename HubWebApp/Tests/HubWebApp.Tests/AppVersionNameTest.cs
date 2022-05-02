using System.Text.Json;
using XTI_Hub.Abstractions;

namespace HubWebApp.Tests;

internal sealed class AppVersionNameTest
{
    [Test]
    public void ShouldDeserialize()
    {
        var versionName = new AppVersionName("whatever");
        var serialized = JsonSerializer.Serialize(versionName);
        var deserialized = JsonSerializer.Deserialize<AppVersionName>(serialized);
        Assert.That(deserialized, Is.EqualTo(versionName));
    }
}
