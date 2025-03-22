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
            byte[] bin = Convert.FromBase64String(str);
            return Encoding.GetString(Decompress(bin));
        }

        public byte[] Decrypt(byte[] data)
        {
            return Decompress(data);
        }

        public string Encrypt(string str)
        {
            byte[] bin = Encoding.GetBytes(str);
            return Convert.ToBase64String(Compress(bin));
        }

        public byte[] Encrypt(byte[] data)
        {
            return Compress(data);
        }

        public byte[] Compress(byte[] binary)
        {
            using (MemoryStream ms = new MemoryStream())
            using (GZipStream gz = new GZipStream(ms, CompressionLevel.Fastest, false))
            {
                gz.Write(binary, 0, binary.Length);
                gz.Close();
                return ms.ToArray();
            }
        }

        public byte[] Decompress(byte[] binary)
        {
            using (MemoryStream buffer = new MemoryStream(binary))
            using (GZipStream gz = new GZipStream(buffer, CompressionMode.Decompress))
            using (MemoryStream ms = new MemoryStream())
            {
                gz.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}
