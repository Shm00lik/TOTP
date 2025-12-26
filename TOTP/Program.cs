using System.Security.Cryptography;
using TOTP;

using var hash = SHA1.Create();

var key = SimpleBase.Base32.Rfc4648.Decode("hellothisisatest");

var c = DateTimeOffset.UtcNow.ToUnixTimeSeconds() / 30;

var otp = Hotp.Compute(key, (int)c, 6, hash, 64);
Console.WriteLine(otp.ToString("D6"));