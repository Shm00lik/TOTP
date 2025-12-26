namespace TOTP;

public static class Utils
{
    public static byte[] Pad(byte[] data, int blockSize)
    {
        if (data.Length >= blockSize)
        {
            return data;
        }

        var padded = CreateRepeated(0, blockSize);

        Array.Copy(data, padded, data.Length);

        return padded;
    }

    public static byte[] CreateRepeated(byte value, int length)
    {
        var array = new byte[length];

        for (var i = 0; i < length; i++)
        {
            array[i] = value;
        }

        return array;
    }

    public static byte[] Xor(byte[] arr1, byte[] arr2)
    {
        if (arr1.Length != arr2.Length)
        {
            throw new ArgumentException("Arrays must be of the same length to XOR.");
        }

        var result = new byte[arr1.Length];

        for (var i = 0; i < arr1.Length; i++)
        {
            result[i] = (byte)(arr1[i] ^ arr2[i]);
        }

        return result;
    }

    public static byte[] Concat(byte[] arr1, byte[] arr2)
    {
        var result = new byte[arr1.Length + arr2.Length];

        arr1.CopyTo(result, 0);
        arr2.CopyTo(result, arr1.Length);

        return result;
    }

    public static byte GetLastNibble(byte b)
    {
        return (byte)(b & 0x0F);
    }

    public static byte[] ExtractBytes(byte[] data, int offset, int amount)
    {
        return data[(offset + 1)..(offset + amount + 1)];
    }
}