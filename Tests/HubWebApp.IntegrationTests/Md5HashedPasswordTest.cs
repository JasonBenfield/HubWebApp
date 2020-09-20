using HubWebApp.Extensions;
using NUnit.Framework;

namespace HubWebApp.IntegrationTests
{
    public sealed class Md5HashedPasswordTest
    {
        [Test]
        public void ShouldHashPassword()
        {
            var originalPassword = "Password1234";
            var hashedPassword1 = new Md5HashedPassword(originalPassword);
            Assert.That(hashedPassword1.Value(), Is.Not.EqualTo(originalPassword), "Should hash the password");
            var hashedPassword2 = new Md5HashedPassword(originalPassword);
            Assert.That(hashedPassword2.Equals(hashedPassword1.Value()), Is.True, "Hashes of the same password should be equal");
        }
    }
}