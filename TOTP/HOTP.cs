using System.Security.Cryptography;

namespace TOTP;

public static class Hotp
{
    private const int TruncatedLength = 4;

    public static int Compute(byte[] key, long counter, int digits, HashAlgorithm hash, int blockSize)
    {
        var message = BitConverter.GetBytes(counter);

        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(message);
        }

        var hashed = Hmac.Compute(key, message, hash, blockSize);

        var offset = Utils.GetLastNibble(hashed[^1]);

        var truncated = Utils.Truncate(hashed, offset, TruncatedLength);

        return truncated % (int)Math.Pow(10, digits);
    }
}