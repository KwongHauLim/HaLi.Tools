namespace HaLi.Tools.Encryption;

public class CryptoBook
{
    public byte[] KEY { get; set; }
    public byte[] IV { get; set; }

    public CryptoBook(string key, string iv)
        : this(Convert.FromBase64String(key), Convert.FromBase64String(iv)) { }
    public CryptoBook(byte[] key, byte[] iv)
    {
        KEY = key;
        IV = iv;
    }
}