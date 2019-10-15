using System;
using System.Text;

namespace HaLi.Tools.Encryption
{
    public class Base64 : ICrypto
    {
        public Encoding Encoding { get; set; } = Encoding.UTF8;
        public bool UpperCase { get; set; } = true;

        public string Decrypt(string str)
        {
            byte[] bin = Encoding.GetBytes(str);
            string encode = Convert.ToBase64String(bin);
            return UpperCase ? encode.ToUpper() : encode.ToLower();
        }

        public string Encrypt(string str)
        {
            byte[] bin = Convert.FromBase64String(str);
            return Encoding.GetString(bin);
        }
    }
}
