using NUnit.Framework;
using XTI_Hub;

namespace HubWebApp.Tests;

internal sealed class Md5HashedPasswordTest
{
    [Test]
    public void ShouldHashPassword()
    {
        var originalPassword = "Password1234";
        var factory = new Md5HashedPasswordFactory();
        var hashedPassword1 = factory.Create(originalPassword);
        Assert.That(hashedPassword1.Value(), Is.Not.EqualTo(originalPassword), "Should hash the password");
        var hashedPassword2 = factory.Create(originalPassword);
        Assert.That(hashedPassword2.Equals(hashedPassword1.Value()), Is.True, "Hashes of the same password should be equal");
        var differentHashedPassword = factory.Create("Password4321");
        Assert.That(hashedPassword1.Equals(differentHashedPassword.Value()), Is.False, "Hashes of different passwords should not be equal");
        Assert.That(hashedPassword2.Equals(differentHashedPassword.Value()), Is.False, "Hashes of different passwords should not be equal");
        Console.WriteLine($"hashedPassword1: {hashedPassword1.Value()}");
        Console.WriteLine($"hashedPassword2: {hashedPassword2.Value()}");
        Console.WriteLine($"differentHashedPassword: {differentHashedPassword.Value()}");
    }
}