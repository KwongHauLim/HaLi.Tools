using System;
using System.IO;
using System.Security.Cryptography;

namespace HaLi.Tools.Encryption
{
    public sealed class AES : ICrypto
    {
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

        public CryptoBook Secret { get; set; }

        public string Encrypt(string str)
        {
            using (AesManaged aes = new AesManaged() { Key = Secret.KEY, IV = Secret.IV })
            using (ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
            using (MemoryStream msEncrypt = new MemoryStream())
            using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
            {
                swEncrypt.Write(str);
                swEncrypt.Flush();
                csEncrypt.FlushFinalBlock();
                msEncrypt.Flush();
                str = Convert.ToBase64String(msEncrypt.ToArray());
            }
            return str;
        }

        public string Decrypt(string str)
        {
            using (AesManaged aes = new AesManaged() { Key = Secret.KEY, IV = Secret.IV })
            using (ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
            using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(str)))
            using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
            {
                str = srDecrypt.ReadToEnd();
            }
            return str;
        }
    }
}
