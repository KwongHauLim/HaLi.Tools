using System;
using System.IO;
using System.Linq;
using System.Text;

namespace HaLi.Tools.Hashcode
{
    public sealed class MD5 : IHashCalc
    {
        public Encoding Encoding { get; set; } = Encoding.UTF8;
        public bool UpperCase { get; set; } = true;
        private string Format => UpperCase ? "X2" : "x2";

        public string GetHash(byte[] binary)
        {
            using var md5 = System.Security.Cryptography.MD5.Create();
            byte[] hash = md5.ComputeHash(binary);
            return string.Concat(hash.Select(b => b.ToString(Format)));
        }

        public string GetHash(string str)
            => GetHash(Encoding.GetBytes(str));

        public string GetHash(int num)
            => GetHash(BitConverter.GetBytes(num));

        public string GetHash(Stream stream)
        {
            using var md5 = System.Security.Cryptography.MD5.Create();
            byte[] hash = md5.ComputeHash(stream);
            return string.Concat(hash.Select(b => b.ToString(Format)));
        }
    }
}
