using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HaLi.Tools.SecretMemory;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            var pp = new Pack[10];
            for (int i = 0; i < pp.Length; i++)
            {
                pp[i] = new Pack()
                {
                    id = i,
                    sec = Environment.TickCount / 10000000f,
                    msg = $"Hello, i am message @ {Environment.TickCount}"
                };

            }

            Console.ReadKey();
        }
    }

    public class Pack
    {
        public Secret.Int id;
        public Secret.Float sec;
        public Secret.String msg;
    }
}
