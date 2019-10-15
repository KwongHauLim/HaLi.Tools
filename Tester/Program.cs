using System;
using HaLi.Tools.Encryption;

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
            Console.ReadKey();
        }
    }
}
