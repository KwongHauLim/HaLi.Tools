using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace HaLi.Tools.Hashcode
{
    public sealed class SHA1 : IHashCalc
    {
        public Encoding Encoding { get; set; } = Encoding.UTF8;
        public bool UpperCase { get; set; } = true;

        public string GetHash(byte[] binary)
        {
            using (var sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(binary);
                var format = UpperCase ? "X2" : "x2";
                return string.Concat(hash.Select(b => b.ToString(format)));
            }
        }

        public string GetHash(string str)
        {
            return GetHash(Encoding.GetBytes(str));
        }
    }
}
