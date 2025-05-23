﻿using System.Security.Cryptography;
using System.Text;

namespace HaLi.Tools.Hashcode;

public sealed class Cipher : IHashCalc
{
    public static readonly Cipher Shared = new Cipher();
    public string Secret { get; set; }
    public bool UpperCase { get; set; } = true;

    public Cipher() : this("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ")
    {

    }

    public Cipher(string code)
    {
        Secret = code;
    }

    public string ToCipher(int num)
    {
        var sb = new StringBuilder();
        sb.Append(Secret[num & 0x1F]);
        sb.Append(Secret[num >> 4 & 0x1F]);
        sb.Append(Secret[num >> 8 & 0x1F]);
        sb.Append(Secret[num >> 12 & 0x1F]);
        sb.Append(Secret[num >> 16 & 0x1F]);
        sb.Append(Secret[num >> 20 & 0x1F]);
        sb.Append(Secret[num >> 24 & 0x1F]);
        sb.Append(Secret[(num >> 28 & 0x1F) | ((num & 0x01) << 4)]);
        return UpperCase ? sb.ToString().ToUpper() : sb.ToString().ToLower();
    }

    public string GetHash(string str)
        => GetHash(Encoding.UTF8.GetBytes(str));

    public string GetHash(int num)
        => GetHash(BitConverter.GetBytes(num));

    public string GetHash(byte[] binary)
    {
        using var sha1 = new SHA1Managed();
        var hash = sha1.ComputeHash(binary);
        return HashToString(hash);
    }

    public string GetHash(Stream stream)
    {
        using var sha1 = new SHA1Managed();
        var hash = sha1.ComputeHash(stream);
        return HashToString(hash);
    }

    private string HashToString(byte[] hash)
    {
        int num = 0;
        var code = new StringBuilder();
        for (int i = 0; i < hash.Length; i += 4)
        {
            num ^= BitConverter.ToInt32(hash, i);
        }
        return ToCipher(num);
    }
}
