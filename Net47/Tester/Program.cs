using System;
using System.Diagnostics;
using System.Threading;
using HaLi.Tools.SecretMemory;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            //var pp = new Pack[10];
            //for (int i = 0; i < pp.Length; i++)
            //{
            //    pp[i] = new Pack()
            //    {
            //        id = i,
            //        sec = Environment.TickCount / 10000000f,
            //        msg = $"Hello, i am message @ {Environment.TickCount}"
            //    };

            //}


            //for (int i = 0; i < pp.Length; i++)
            //{
            //    Console.WriteLine(pp[i].msg);
            //    pp[i].msg = $"Random - {RNG.Int32}";
            //    Console.WriteLine($"I change message:{pp[i].msg}");
            //}

            Thread.Sleep(1000);

            var watch = new Stopwatch();
            var msg = "I am a test message";
            string read = string.Empty;
            long end = Environment.TickCount + 5000;
            long count = 0;
            Console.WriteLine($"Start:{Environment.TickCount}");
            watch.Restart();
            //Task.Run(() =>
            //{
            while (Environment.TickCount < end)
            {
                Secret.String m = $"{msg}:{Environment.TickCount}";
                read = m;
                count++;
            }
            //}).Wait();
            watch.Stop();

            Console.WriteLine($"End:{Environment.TickCount}");
            Console.WriteLine($"Run:{count}");

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
