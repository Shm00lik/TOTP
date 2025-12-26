using System.Security.Cryptography;
using System.Text;
using TOTP;

using var hash = SHA256.Create();

var key = "key"u8.ToArray();
var message = "The quick brown fox jumps over the lazy dog"u8.ToArray();

var result = Hmac.Compute(key, message, hash, 64);

Console.WriteLine(Convert.ToHexStringLower(result));