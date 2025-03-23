using System.Security.Cryptography;
using SSC = System.Security.Cryptography;

namespace HaLi.Tools.Hashcode;

public sealed class MD5 : HashManaged
{
    public static readonly MD5 Shared = new MD5();
    private SSC.MD5 _hash = SSC.MD5.Create();
    protected override HashAlgorithm Algorithm => _hash;
}
