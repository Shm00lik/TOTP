using System.Security.Cryptography;

namespace TOTP;

public static class Totp
{
    private const int DefaultInterval = 30;
    private const int DefaultDigits = 6;

    public static string Compute(string key, int digits, int interval, HashAlgorithm hash, int blockSize)
    {
        var decodedKey = Utils.FromBase32(key);

        var timestep = DateTimeOffset.UtcNow.ToUnixTimeSeconds() / interval;

        var otp = Hotp.Compute(decodedKey, timestep, digits, hash, blockSize);
        return otp.ToString($"D{digits}");
    }

    public static string Compute(string key, HashAlgorithm hash, int blockSize)
    {
        return Compute(key, DefaultInterval, DefaultDigits, hash, blockSize);
    }
}