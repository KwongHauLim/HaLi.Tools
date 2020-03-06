using System;
using System.Diagnostics;

namespace HaLi.Tools.Extends
{
    [DebuggerStepThrough]
    public static class ExtendDouble
    {
        public static double Round(this double f, int decimals)
        {
            decimal dec = new decimal(f);
            return Convert.ToDouble(decimal.Round(dec, decimals));
        }

        public static double Ceil(this double f)
        {
            return Math.Ceiling(f);
        }

        public static double ModOne(this double f)
        {
            return f - (int)f;
        }

        public static bool AboveZero(this double f)
        {
            return f.CompareTo(0.0) > 0;
        }

        public static bool UnderZero(this double f)
        {
            return f.CompareTo(0.0) < 0;
        }

        public static bool EqualZero(this double f)
        {
            return f.CompareTo(0.0) == 0;
        }

        public static string ToBase64(this double f)
        {
            return Convert.ToBase64String(BitConverter.GetBytes(f));
        }
    } 
}
