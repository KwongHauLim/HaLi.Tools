using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace HaLi.Tools.Hashcode
{
    public sealed class Base64 : IHashCalc
    {
        public string GetHash(int code)
        {
            byte[] base64 = new byte[8]
            {
                (byte)(code &0xFF),
                (byte)(code >> 4 & 0xFF),
                (byte)(code >> 8 & 0xFF),
                (byte)(code >> 12 & 0xFF),
                (byte)(code >> 16 & 0xFF),
                (byte)(code >> 20 & 0xFF),
                (byte)(code >> 24 & 0xFF),
                (byte)((code >> 28 & 0x0F) | ((code & 0x0F) << 4))
            };
            return Convert.ToBase64String(base64);
        }

        public string GetHash(string str)
        {
            using var sha1 = new SHA1Managed();
            var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(str));
            var code = new StringBuilder();
            for (int i = 0; i < hash.Length; i += 4)
            {
                code.Append(GetHash(BitConverter.ToInt32(hash, i)));
            }
            return code.ToString();
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

        public bool Validate(string hash)
        {
            if (hash.Length % 12 != 0) return false;

            for (int i = 0; i < hash.Length - 1; i += 12)
            {
                if (!Validate8(hash.Substring(i, 12)))
                    return false;
            }

            return true;
        }

        private bool Validate8(string hash)
        {
            var b = Convert.FromBase64String(hash);
            int f = b[0];
            int last = f >> 4;
            int curr = 0;
            for (int i = 1; i < b.Length; i++)
            {
                curr = b[i];
                if ((curr & 0x0F) != last)
                    return false;
                last = curr >> 4 & 0x0F;
            }
            return (f & 0x0F) == last;
            //int first = Secret.IndexOf(hash[0]);
            //int last = first >> 4 & 1;
            //int curr = 0;
            //for (int i = 1; i < hash.Length; i++)
            //{
            //    curr = Secret.IndexOf(hash[i]);
            //    if ((curr & 1) != last)
            //        return false;
            //    last = curr >> 4 & 1;
            //}
            //return (first & 1) == last;
        }
    }
}
