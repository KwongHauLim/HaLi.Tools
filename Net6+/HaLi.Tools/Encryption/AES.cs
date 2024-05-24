using System.Security.Cryptography;
using SSC = System.Security.Cryptography;

namespace HaLi.Tools.Encryption;

public sealed partial class AES : CryptoManaged
{
    public CryptoBook Secret { get; set; }

    private readonly SSC.Aes _crypto = SSC.Aes.Create();
    protected override SymmetricAlgorithm Algorithm => _crypto;

    protected override ICryptoTransform CreateDecryptor() => _crypto.CreateDecryptor(Secret.KEY, Secret.IV);
    protected override ICryptoTransform CreateEncryptor() => _crypto.CreateEncryptor(Secret.KEY, Secret.IV);

    public static AES @Default => new AES
    {
        Secret = new CryptoBook("Te6BZKhwp5HfYA6OQsinNlkM1CN97FQtE4NdWe9WwBM=", "L0g9aJpY6hvQ2xAddS6GIA==")
    };

    public static class Helper
    {
        public static void GenerateKey(out string key, out string iv)
        {
            using var aes = SSC.Aes.Create();
            key = Convert.ToBase64String(aes.Key);
            iv = Convert.ToBase64String(aes.IV);
        }

        public static void GenerateKey(out byte[] key, out byte[] iv)
        {
            using var aes = SSC.Aes.Create();
            key = aes.Key;
            iv = aes.IV;
        }
    }
}
