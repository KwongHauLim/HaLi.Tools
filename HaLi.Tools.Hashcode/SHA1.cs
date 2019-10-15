using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HaLi.Tools.Hashcode
{
    public sealed class SHA1 : IHashCalc
    {
        public string GetHash(byte[] binary)
        {
            using (var sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(binary);
                return string.Concat(hash.Select(b => b.ToString("X2")));
            }
        }
    }
}
