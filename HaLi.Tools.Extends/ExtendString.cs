using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaLi.Tools.Extends
{
    [DebuggerStepThrough]
    public static class ExtendString
    {
        public static string[] ToArray(this string s)
            => new string[1] { s };

        public static List<string> ToList(this string s)
            => new List<string>() { s };

        public static string Base64Encode(this string s)
            => Base64Encode(s, Encoding.UTF8);
        public static string Base64Encode(this string s, Encoding encode)
            => Convert.ToBase64String(encode.GetBytes(s));

        public static string Base64Decode(this string s)
            => Base64Decode(s, Encoding.UTF8);
        public static string Base64Decode(this string s, Encoding encode)
            => encode.GetString(Convert.FromBase64String(s));

        public static string UnicodeEncode(this string s)
        {
            byte[] bin = Encoding.UTF8.GetBytes(s);
            bin = Encoding.Convert(Encoding.UTF8, Encoding.Unicode, bin);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < bin.Length; i += 2)
            {
                sb.Append('\\');
                sb.Append("u");
                sb.Append(bin[i + 1].ToString("X2"));
                sb.Append(bin[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public static string UnicodeDecode(this string s)
        {
            StringBuilder sb = new StringBuilder();
            int i = 0;
            while (i < s.Length)
            {
                if (s.Length - i < 6)
                {
                    sb.Append(s[i]);
                    break;
                }
                else if (s.Substring(i, 2).Equals("\\u"))
                {
                    byte[] bin = new byte[2];
                    bin[0] = Convert.ToByte(s.Substring(i + 2, 2), 16);
                    bin[1] = Convert.ToByte(s.Substring(i + 4, 2), 16);
                    sb.Append(ByteToHex(bin));
                }
            }
            return sb.ToString();
        }

        private static string ByteToHex(byte[] bytes)
        {
            char[] c = new char[bytes.Length * 2];
            int b;
            for (int i = 0; i < bytes.Length; i++)
            {
                b = bytes[i] >> 4;
                c[i * 2] = (char)(55 + b + (((b - 10) >> 31) & -7));
                b = bytes[i] & 0xF;
                c[i * 2 + 1] = (char)(55 + b + (((b - 10) >> 31) & -7));
            }
            return new string(c);
        }

        public static bool ParseBoolean(this string s, bool @default = default)
        {
            if (string.IsNullOrEmpty(s))
                return @default;

            s = s.ToUpper();

            if (s.Length == 1)
            {
                if (s.Equals("Y") || s.Equals("T") || s.Equals("1")) return true;
                if (s.Equals("N") || s.Equals("F") || s.Equals("0")) return false;
            }
            else
            {
                if (s.Equals("YES") || s.Equals("TRUE")) return true;
                if (s.Equals("NO") || s.Equals("FALSE")) return false;
            }

            return @default;
        }

        public static short ParseInt16(this string s, short @default = default)
        {
            short val = 0;

            if (short.TryParse(s, out val))
                return val;
            else
                return @default;
        }

        public static int ParseInt32(this string s, int @default = default)
        {
            int val = 0;

            if (int.TryParse(s, out val))
                return val;
            else
                return @default;
        }

        public static long ParseInt64(this string s, long @default = default)
        {
            long val = 0;

            if (long.TryParse(s, out val))
                return val;
            else
                return @default;
        }

        public static ushort ParseUInt16(this string s, ushort @default = default)
        {
            ushort val = 0;

            if (ushort.TryParse(s, out val))
                return val;
            else
                return @default;
        }

        public static uint ParseUInt32(this string s, uint @default = default)
        {
            uint val = 0;

            if (uint.TryParse(s, out val))
                return val;
            else
                return @default;
        }

        public static ulong ParseUInt64(this string s, ulong @default = default)
        {
            ulong val = 0;

            if (ulong.TryParse(s, out val))
                return val;
            else
                return @default;
        }

        public static float ParseFloat(this string s, float @default = default)
        {
            float val = 0;

            if (float.TryParse(s, out val))
                return val;
            else
                return @default;
        }

        public static double ParseDouble(this string s, double @default = default)
        {
            double val = 0;

            if (double.TryParse(s, out val))
                return val;
            else
                return @default;
        }

        public static DateTime ParseDateTime(this string s, DateTime @default)
        {
            DateTime val = DateTime.MinValue;

            if (DateTime.TryParse(s, out val))
                return val;
            else
                return @default;
        }

        public static TimeSpan ParseTimeSpan(this string s, TimeSpan @default)
        {
            TimeSpan val = TimeSpan.Zero;

            if (TimeSpan.TryParse(s, out val))
                return val;
            else
                return @default;
        }

        public static string Left(this string s, int len)
        {
            if (len == 0 || s.Length == 0)
                return string.Empty;
            else if (s.Length <= len)
                return s;
            else
                return s.Substring(0, len);
        }

        public static string Right(this string s, int len)
        {
            if (len == 0 || s.Length == 0)
                return string.Empty;
            else if (s.Length <= len)
                return s;
            else
                return s.Substring(s.Length - len, len);
        }

        public static bool AnyContains(this string s, params string[] sample)
        {
            foreach (var item in sample)
            {
                if (s.Contains(item))
                    return true;
            }
            return false;
        }

        public static bool AnyEqual(this string s, params string[] sample)
        {

            foreach (var item in sample)
            {
                if (s.Equals(item))
                    return true;
            }
            return false;
        }

        public static bool SurroundWith(this string s, string start, string end)
            => s.StartsWith(start) && s.EndsWith(end);

        public static string Shuffle(this string s)
        {
            var list = new List<char>(s.ToCharArray());
            list.Shuffle();
            return string.Join(string.Empty, list.ToArray());
        }
    }
}
