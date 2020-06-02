using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace HaLi.Tools.Hashcode
{
    public sealed class Cipher : IHashCalc
    {
        public string Secret { get; set; }
        public bool UpperCase { get; set; } = true;

        public Cipher() : this("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ")
        {

        }

        public Cipher(string code)
        {
            Secret = code;
        }

        public string GetHash(int code)
        {
            var sb = new StringBuilder();
            sb.Append(Secret[code & 0x1F]);
            sb.Append(Secret[code >> 4 & 0x1F]);
            sb.Append(Secret[code >> 8 & 0x1F]);
            sb.Append(Secret[code >> 12 & 0x1F]);
            sb.Append(Secret[code >> 16 & 0x1F]);
            sb.Append(Secret[code >> 20 & 0x1F]);
            sb.Append(Secret[code >> 24 & 0x1F]);
            sb.Append(Secret[(code >> 28 & 0x1F) | ((code & 0x01) << 4)]);
            return UpperCase ? sb.ToString().ToUpper() : sb.ToString().ToLower();
        }

        public string GetHash(string str)
        {
            using var sha1 = new SHA1Managed();
            var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(str));
            var code = BitConverter.ToInt32(hash, 0);
            return GetHash(code);
        }

        public string GetHash(byte[] binary)
        {
            return GetHash(Convert.ToBase64String(binary));
        }

        public string GetHash(Stream stream)
        {
            using var ms = new MemoryStream();
            stream.CopyTo(ms);
            return GetHash(ms.ToArray());
        }
    }
}
