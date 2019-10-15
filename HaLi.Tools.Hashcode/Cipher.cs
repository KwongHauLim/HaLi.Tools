using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaLi.Tools.Hashcode
{
    public sealed class Cipher : IHashCalc
    {
        public string Secret { get; set; } = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public bool UpperCase { get; set; } = true;

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
            return GetHash(str.GetHashCode());
        }
    }
}
