using System;
using System.Collections.Generic;
using System.Linq;
using HaLi.Tools.Randomization;

namespace HaLi.Tools.Roulette
{
    public class Roller
    {
        public Table Table { get; set; }

        public object Next()
        {
            double r = RNG.Next(0.0,Table.sum);
            return null;
            //int i = Table.Chance().IndexOf(v => v.CompareTo(r) <= 0);
        }
    }

    public class Table
    {
        public Dictionary<object, double> Pocket { get; set; } = new Dictionary<object, double>();

        internal protected bool isChanged = false;
        private List<object> prizes = new List<object>();
        private List<double> weights = new List<double>();
        internal double sum = 0.0;

        public Table()
        {
            isChanged = true;
        }

        public List<double> Chance()
        {
            if (isChanged)
            {
                prizes.Clear();
                weights.Clear();

                sum = 0.0;
                foreach (var pair in Pocket)
                {
                    prizes.Add(pair.Key);
                    sum += pair.Value;
                    weights.Add(sum);
                }
                isChanged = false;
            }
            return weights;
        }
    }
}
