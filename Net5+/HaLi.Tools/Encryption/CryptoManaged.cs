using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HaLi.Tools.Encryption
{
    public abstract class CryptoManaged : ICrypto
    {
        protected abstract SymmetricAlgorithm Algorithm { get; }

        protected virtual ICryptoTransform CreateEncryptor() => Algorithm.CreateEncryptor();
        protected virtual ICryptoTransform CreateDecryptor() => Algorithm.CreateDecryptor();

        public string Encrypt(string str)
        {
            using (ICryptoTransform encryptor = CreateEncryptor())
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
            using (ICryptoTransform decryptor = CreateDecryptor())
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
