using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace HaLi.Tools.Randomization
{
    [DebuggerStepThrough]
    public static class ExtendString
    {
        public static string Shuffle(this string s)
        {
            var list = new List<char>(s.ToCharArray());
            list.Shuffle();
            return string.Join(string.Empty, list.ToArray());
        }
    }
}
