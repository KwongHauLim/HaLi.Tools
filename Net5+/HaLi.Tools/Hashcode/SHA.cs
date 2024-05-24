using System.Security.Cryptography;
using SSC = System.Security.Cryptography;

namespace HaLi.Tools.Hashcode;

public sealed class SHA1 : HashManaged
{
    private SSC.SHA1 _hash = SSC.SHA1.Create();
    protected override HashAlgorithm Algorithm => _hash;
}

public sealed class SHA256 : HashManaged
{
    private SSC.SHA256 _hash = SSC.SHA256.Create();
    protected override HashAlgorithm Algorithm => _hash;
}

public sealed class SHA384 : HashManaged
{
    private SSC.SHA384 _hash = SSC.SHA384.Create();
    protected override HashAlgorithm Algorithm => _hash;
}

public sealed class SHA512 : HashManaged
{
    private SSC.SHA512 _hash = SSC.SHA512.Create();
    protected override HashAlgorithm Algorithm => _hash;
}