using System;
using HaLi.Tools.Roulette;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            var roulette = new Roller();
            roulette.Table = new Table();
            roulette.Table.SetPrize("a", 20.0);
            roulette.Table.SetPrize("b", 20.0);

            var ret = roulette.Next();

            Console.ReadKey();
        }
    }
}
