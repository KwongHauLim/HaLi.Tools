using System.Security.Cryptography;
using SSC = System.Security.Cryptography;

namespace HaLi.Tools.Hashcode;

public sealed class SHA1 : HashManaged
{
    public static readonly SHA1 Shared = new SHA1();
    private SSC.SHA1 _hash = SSC.SHA1.Create();
    protected override HashAlgorithm Algorithm => _hash;
}

public sealed class SHA256 : HashManaged
{
    public static readonly SHA256 Shared = new SHA256();
    private SSC.SHA256 _hash = SSC.SHA256.Create();
    protected override HashAlgorithm Algorithm => _hash;
}

public sealed class SHA384 : HashManaged
{
    public static readonly SHA384 Shared = new SHA384();
    private SSC.SHA384 _hash = SSC.SHA384.Create();
    protected override HashAlgorithm Algorithm => _hash;
}

public sealed class SHA512 : HashManaged
{
    public static readonly SHA512 Shared = new SHA512();
    private SSC.SHA512 _hash = SSC.SHA512.Create();
    protected override HashAlgorithm Algorithm => _hash;
}