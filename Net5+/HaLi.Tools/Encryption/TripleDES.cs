using System.Security.Cryptography;
using SSC = System.Security.Cryptography;

namespace HaLi.Tools.Encryption;

public sealed class TripleDES : CryptoManaged
{
    public CryptoBook Secret { get; set; }

    private readonly SSC.TripleDES _crypto = SSC.TripleDES.Create();
    protected override SymmetricAlgorithm Algorithm => _crypto;

    protected override ICryptoTransform CreateDecryptor() => _crypto.CreateDecryptor(Secret.KEY, Secret.IV);
    protected override ICryptoTransform CreateEncryptor() => _crypto.CreateEncryptor(Secret.KEY, Secret.IV);

    public static TripleDES @Default => new TripleDES
    {
        Secret = new CryptoBook("VZkoHQ0xD9l0FtELTOc5Hk90Q7jfDduU", "HZNxshtB9bU=")
    };

    public static class Helper
    {
        public static void GenerateKey(out string key, out string iv)
        {
            using var aes = SSC.TripleDES.Create();
            key = Convert.ToBase64String(aes.Key);
            iv = Convert.ToBase64String(aes.IV);
        }

        public static void GenerateKey(out byte[] key, out byte[] iv)
        {
            using var aes = SSC.TripleDES.Create();
            key = aes.Key;
            iv = aes.IV;
        }
    }
}
