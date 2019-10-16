using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace HaLi.Tools.Encryption
{
    public class GZip : ICrypto
    {
        public Encoding Encoding { get; set; } = Encoding.UTF8;

        public string Decrypt(string str)
        {
            throw new NotImplementedException();
        }

        public string Encrypt(string str)
        {
            byte[] bin = Encoding.GetBytes(str);
            return Convert.ToBase64String(Compress(bin));
        }

        public byte[] Compress(byte[] binary)
        {
            using (MemoryStream ms = new MemoryStream())
            using (GZipStream gz = new GZipStream(ms, CompressionLevel.Fastest, false))
            {
                gz.Write(binary, 0, binary.Length);
                return ms.ToArray();
            }
        }
    }
}
