using System.Security.Cryptography;
using System.Text;

namespace HaLi.Tools.Hashcode;

public sealed class SHA1 : IHashCalc
{
    public Encoding Encoding { get; set; } = Encoding.UTF8;
    public bool UpperCase { get; set; } = true;
    private string Format => UpperCase ? "X2" : "x2";

    public string GetHash(string str)
        => GetHash(Encoding.GetBytes(str));

    public string GetHash(int num)
        => GetHash(BitConverter.GetBytes(num));

    public string GetHash(byte[] binary)
    {
        using var sha1 = new SHA1Managed();
        var hash = sha1.ComputeHash(binary);
        return string.Concat(hash.Select(b => b.ToString(Format)));
    }

    public string GetHash(Stream stream)
    {
        using var sha1 = new SHA1Managed();
        var hash = sha1.ComputeHash(stream);
        return string.Concat(hash.Select(b => b.ToString(Format)));
    }
}
