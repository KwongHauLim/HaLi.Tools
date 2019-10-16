using System;
using HaLi.Tools.Encryption;
using HaLi.Tools.Hashcode;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            string secret = Crypto.Encrypt("I am a secret message, you should not see me");
            Console.WriteLine(secret);
            string visible = Crypto.Decrypt(secret);
            Console.WriteLine(visible);
            string sha1 = Hash.GetHash(visible);
            Console.WriteLine($"sha1:{sha1}");
            string cipher = new HaLi.Tools.Hashcode.Cipher().GetHash(visible);
            Console.WriteLine($"cipher:{cipher}");
            GZip gZip = new GZip();
            string zip = gZip.Encrypt(visible);
            Console.WriteLine($"zip:{zip}");
            string unzip = gZip.Decrypt(zip);
            Console.WriteLine($"unzip:{unzip}");
            Console.ReadKey();
        }
    }
}
