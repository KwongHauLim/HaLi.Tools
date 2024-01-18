using System.Security.Cryptography;
using System.Text;

namespace HaLi.Tools.Hashcode;

public sealed class Base64 : IHashCalc
{
    public string ToBase64(int num)
    {
        byte[] base64 = new byte[8]
        {
            (byte)(num &0xFF),
            (byte)(num >> 4 & 0xFF),
            (byte)(num >> 8 & 0xFF),
            (byte)(num >> 12 & 0xFF),
            (byte)(num >> 16 & 0xFF),
            (byte)(num >> 20 & 0xFF),
            (byte)(num >> 24 & 0xFF),
            (byte)((num >> 28 & 0x0F) | ((num & 0x0F) << 4))
        };
        return Convert.ToBase64String(base64);
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
        var code = new StringBuilder();
        for (int i = 0; i < hash.Length; i += 4)
        {
            code.Append(ToBase64(BitConverter.ToInt32(hash, i)));
        }
        return code.ToString();
    }

    public bool Validate(string hash)
    {
        if (hash.Length % 12 != 0) return false;

        for (int i = 0; i < hash.Length - 1; i += 12)
        {
            if (!Validate8(hash.Substring(i, 12)))
                return false;
        }

        return true;
    }

    private bool Validate8(string hash)
    {
        var b = Convert.FromBase64String(hash);
        int f = b[0];
        int last = f >> 4;
        int curr = 0;
        for (int i = 1; i < b.Length; i++)
        {
            curr = b[i];
            if ((curr & 0x0F) != last)
                return false;
            last = curr >> 4 & 0x0F;
        }
        return (f & 0x0F) == last;
    }
}
