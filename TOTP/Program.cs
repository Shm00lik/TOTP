using System.Security.Cryptography;
using TOTP;

using var hash = SHA1.Create();
var totp = Totp.Compute("hellothisisatest", 8, 30, hash, 64);

Console.WriteLine($"You code is: {totp}");