using System.Security.Cryptography;
using XTI_App.Abstractions;

namespace XTI_Hub;

public sealed class Md5HashedPassword : HashedPassword
{
    private const int saltLength = 16;
    private const int hashLength = 20;

    internal Md5HashedPassword(string password) : base(password)
    {
    }

    protected override string Hash(string password)
    {
        var salt = new byte[saltLength];
        RandomNumberGenerator.Create().GetBytes(salt);
        return "V2_" + HashV2(password, salt);
    }

    protected override bool _Equals(string password, string hashedPassword)
    {
        var isV2 = hashedPassword.StartsWith("V2_");
        if (isV2)
        {
            hashedPassword = hashedPassword.Substring(3);
        }
        var otherHashedBytes = Convert.FromBase64String(hashedPassword);
        var salt = new byte[16];
        Array.Copy(otherHashedBytes, 0, salt, 0, 16);
        var thisHashed = isV2 ? HashV2(password, salt) : HashV1(password, salt);
        return hashedPassword == thisHashed;
    }

    private static string HashV1(string password, byte[] salt)
    {
#pragma warning disable SYSLIB0041 // Type or member is obsolete
        var deriveBytes = new Rfc2898DeriveBytes(password, salt, 100000);
#pragma warning restore SYSLIB0041 // Type or member is obsolete
        var hash = deriveBytes.GetBytes(hashLength);
        var hashBytes = new byte[saltLength + hashLength];
        Array.Copy(salt, 0, hashBytes, 0, saltLength);
        Array.Copy(hash, 0, hashBytes, saltLength, hashLength);
        return Convert.ToBase64String(hashBytes);
    }

    private static string HashV2(string password, byte[] salt)
    {
        var deriveBytes = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256);
        var hash = deriveBytes.GetBytes(hashLength);
        var hashBytes = new byte[saltLength + hashLength];
        Array.Copy(salt, 0, hashBytes, 0, saltLength);
        Array.Copy(hash, 0, hashBytes, saltLength, hashLength);
        return Convert.ToBase64String(hashBytes);
    }
}