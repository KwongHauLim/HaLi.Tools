﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HaLi.Tools.Hashcode
{
    public sealed class SHA1 : IHashCalc
    {
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
    }
}
