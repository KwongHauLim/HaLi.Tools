using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaLi.Tools.Encryption
{
    public class Cipher : ICrypto
    {
        public class CryptoBook
        {
            public byte[] Forward { get; private set; } = new byte[256];
            public byte[] Reverse { get; private set; } = new byte[256];
            
            public CryptoBook()
            {
                var tmp = new List<int>(Enumerable.Range(0, 256));
                var rand = new Random();
                int idx;
                byte b;

                for (int i = 0; i < 256; i++)
                {
                    idx = rand.Next(0, tmp.Count);
                    b = (byte)tmp[idx];
                    Forward[i] = b;
                    Reverse[b] = (byte)i;
                    tmp.RemoveAt(idx);
                }
            }

            public CryptoBook(string code)
            {
                byte b;
                for (int i = 0; i < 256; i++)
                {
                    b = Convert.ToByte(code.Substring(i * 2, 2), 16);
                    Forward[i] = b;
                    Reverse[b] = (byte)i;
                }
            }

            public override string ToString()
                => string.Concat(Forward.Select(b => b.ToString("X2")));
        }

        public CryptoBook Secret { get; set; } = new CryptoBook();

        public Encoding Encoding { get; set; } = Encoding.UTF8;

        public string Decrypt(string str)
        {
            byte[] bin = Convert.FromBase64String(str);
            for (int i = 0; i < bin.Length; i++)
            {
                bin[i] = Secret.Reverse[bin[i]];
            }
            return Encoding.GetString(bin);
        }

        public string Encrypt(string str)
        {
            byte[] bin = Encoding.GetBytes(str);
            for (int i = 0; i < bin.Length; i++)
            {
                bin[i] = Secret.Forward[bin[i]];
            }
            return Convert.ToBase64String(bin);
        }

        public static Cipher @Default => new Cipher()
        {
            Encoding = Encoding.UTF8,
            Secret = new CryptoBook("E9E8EBEAEDECEFEE111013121514171619181B1A1D1C1F1E010003020504070609080B0A0D0C0F0E313033323534373639383B3A3D3C3F3E212023222524272629282B2A2D2C2F2E515053525554575659585B5A5D5C5F5E414043424544474649484B4A4D4C4F4E717073727574777679787B7A7D7C7F7E616063626564676669686B6A6D6C6F6E919093929594979699989B9A9D9C9F9E818083828584878689888B8A8D8C8F8EB1B0B3B2B5B4B7B6B9B8BBBABDBCBFBEA1A0A3A2A5A4A7A6A9A8ABAAADACAFAED1D0D3D2D5D4D7D6D9D8DBDADDDCDFDEC1C0C3C2C5C4C7C6C9C8CBCACDCCCFCEF1F0F3F2F5F4F7F6F9F8FBFAFDFCFFFEE1E0E3E2E5E4E7E6")
        };
    }
}
