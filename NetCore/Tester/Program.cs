using System;
using HaLi.Tools.SecretMemory;

namespace Tester
{
    class Example
    {
        public Example()
        {
            Console.WriteLine("Constructor");
        }

        ~Example()
        {
            Console.WriteLine("Destructor");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            SecretInt b = new SecretInt();
            b.Value = 12;
            GC.SuppressFinalize(b);
            b = null;
            GC.Collect();
            GC.Collect();
            //SecretInt[] secret = new SecretInt[10];
            //for (int i = 0; i < secret.Length; i++)
            //{
            //    secret[i] = new SecretInt();
            //    secret[i].Value = i;
            //}

            //for (int i = 0; i < secret.Length; i++)
            //{
            //    Console.WriteLine(secret[i].Value);
            //}
            for (int i = 0; i < 10; i++)
            {
                new SecretInt().Value = i;
            }
            Console.ReadKey();
        }
    }
}
