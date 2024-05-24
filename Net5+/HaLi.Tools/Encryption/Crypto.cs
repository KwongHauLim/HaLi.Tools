namespace HaLi.Tools.Encryption
{
    public interface ICrypto
    {
        string Encrypt(string str);
        string Decrypt(string str);
    }

    public class Crypto
    {
        private static Crypto _ptr = null;
        public static Crypto Share => _ptr ??= new Crypto();

        public ICrypto Algorithm { get; set; }

        private Crypto()
        {
            Algorithm = AES.Default;
        }

        public static string Encrypt(string str)
            => Share.Algorithm.Encrypt(str);

        public static string Decrypt(string str)
            => Share.Algorithm.Decrypt(str);
    }
}
