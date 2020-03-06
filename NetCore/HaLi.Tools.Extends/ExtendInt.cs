using System;
using System.Diagnostics;

namespace HaLi.Tools.Extends
{
    [DebuggerStepThrough]
    public static class ExtendInt
    {
        public static string ToBase64(this int f) => Convert.ToBase64String(BitConverter.GetBytes(f));

        public static string ToBase64(this uint f) => Convert.ToBase64String(BitConverter.GetBytes(f));
    } 
}
