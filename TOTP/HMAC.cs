using System.Security.Cryptography;

namespace TOTP;

public static class Hmac
{
    private const byte OuterPadByte = 0x5C;
    private const byte InnerPadByte = 0x36;

    public static byte[] Compute(byte[] key, byte[] message, HashAlgorithm hash, int blockSize)
    {
        var blockSizedKey = GetBlockSizedKey(key, hash, blockSize);

        var outerPad = Utils.CreateRepeated(OuterPadByte, blockSize);
        var innerPad = Utils.CreateRepeated(InnerPadByte, blockSize);

        var outerKeyPad = Utils.Xor(blockSizedKey, outerPad);
        var innerKeyPad = Utils.Xor(blockSizedKey, innerPad);

        var innerConcat = Utils.Concat(innerKeyPad, message);
        var hashedInner = hash.ComputeHash(innerConcat);

        var outerConcat = Utils.Concat(outerKeyPad, hashedInner);

        return hash.ComputeHash(outerConcat);
    }

    private static byte[] GetBlockSizedKey(byte[] key, HashAlgorithm hash, int blockSize)
    {
        return key.Length > blockSize
            ? hash.ComputeHash(key)
            : Utils.Pad(key, blockSize);
    }
}