namespace HaLi.Tools.Encryption;

public interface ICrypto
{
    string Encrypt(string str);
    string Decrypt(string str);
}

public class Crypto
{
    private static Crypto _ptr = null;
    public static Crypto Share => _ptr = _ptr ?? new Crypto();

    public ICrypto Algorithm { get; set; }

    private Crypto()
    {
        Algorithm = new AES
        {
            Secret = new AES.CryptoBook("Te6BZKhwp5HfYA6OQsinNlkM1CN97FQtE4NdWe9WwBM=", "L0g9aJpY6hvQ2xAddS6GIA==")
        };
    }

    public static string Encrypt(string str)
        => Share.Algorithm.Encrypt(str);

    public static string Decrypt(string str)
        => Share.Algorithm.Decrypt(str);
}
