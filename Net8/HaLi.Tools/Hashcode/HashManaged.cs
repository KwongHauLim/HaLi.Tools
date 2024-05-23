using System.Security.Cryptography;
using System.Text;

namespace HaLi.Tools.Hashcode
{
    public abstract class HashManaged : IHashCalc
    {
        public Encoding Encoding { get; set; } = Encoding.UTF8;
        public bool UpperCase { get; set; } = true;
        private string Format => UpperCase ? "X2" : "x2";

        protected abstract HashAlgorithm Algorithm { get; }

        public string GetHash(string str)
            => GetHash(Encoding.GetBytes(str));

        public string GetHash(int num)
            => GetHash(BitConverter.GetBytes(num));

        public string GetHash(byte[] binary)
        {
            var hash = Algorithm.ComputeHash(binary);
            return string.Concat(hash.Select(b => b.ToString(Format)));
        }

        public string GetHash(Stream stream)
        {
            var hash = Algorithm.ComputeHash(stream);
            return string.Concat(hash.Select(b => b.ToString(Format)));
        }
    }
}
