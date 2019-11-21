using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HaLi.Tools.SecretMemory.Protector;

namespace HaLi.Tools.SecretMemory
{
    internal class Protection
    {
        public static Lazy<Protection> Share { get; private set; } = new Lazy<Protection>();

        internal List<Agent> Agents { get; private set; }

        public Protection()
        {
            Agents = new List<Agent>();
        }

        internal void Add(Block block)
        {
            Agents.Add(new Agent(block));
        }
    }
}
