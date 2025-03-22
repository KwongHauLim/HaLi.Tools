namespace HaLi.Tools.Encryption
{
    public interface ICrypto
    {
        string Encrypt(string str);
        byte[] Encrypt(byte[] data);
        string Decrypt(string str);
        byte[] Decrypt(byte[] data);
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

        public static byte[] Encrypt(byte[] data)
            => Share.Algorithm.Encrypt(data);

        public static string Decrypt(string str)
            => Share.Algorithm.Decrypt(str);

        public static byte[] Decrypt(byte[] data)
            => Share.Algorithm.Decrypt(data);
    }
}
