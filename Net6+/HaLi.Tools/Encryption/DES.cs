using System.Security.Cryptography;
using SSC = System.Security.Cryptography;

namespace HaLi.Tools.Encryption;

public sealed class DES : CryptoManaged
{
    public CryptoBook Secret { get; set; }

    private readonly SSC.DES _crypto = SSC.DES.Create();
    protected override SymmetricAlgorithm Algorithm => _crypto;

    protected override ICryptoTransform CreateDecryptor() => _crypto.CreateDecryptor(Secret.KEY, Secret.IV);
    protected override ICryptoTransform CreateEncryptor() => _crypto.CreateEncryptor(Secret.KEY, Secret.IV);

    public static DES @Default => new DES
    {
        Secret = new CryptoBook("s9oaveKCl00=", "IfBud4uHEEU=")
    };

    public static class Helper
    {
        public static void GenerateKey(out string key, out string iv)
        {
            using var aes = SSC.DES.Create();
            key = Convert.ToBase64String(aes.Key);
            iv = Convert.ToBase64String(aes.IV);
        }

        public static void GenerateKey(out byte[] key, out byte[] iv)
        {
            using var aes = SSC.DES.Create();
            key = aes.Key;
            iv = aes.IV;
        }
    }
}
