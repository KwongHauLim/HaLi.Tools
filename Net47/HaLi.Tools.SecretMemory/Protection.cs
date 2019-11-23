using System;
using System.Collections.Generic;
using HaLi.Tools.SecretMemory.Protector;

namespace HaLi.Tools.SecretMemory
{
    internal class Protection
    {
        public static Lazy<Protection> Share { get; private set; } = new Lazy<Protection>();
        public static bool FindShark { get; internal set; } = false;

        internal Dictionary<Block, Agent> Agents { get; private set; }
        internal Dictionary<Block, Doctor> Doctors { get; private set; }

        public Protection()
        {
            Agents = new Dictionary<Block, Agent>();
            Doctors = new Dictionary<Block, Doctor>();
        }

        internal void Add(Block block)
        {
            Agents[block] = new Agent(block);
            Doctors[block] = new Doctor(block);
        }

        internal void HealMe(Block block)
        {
            if (Doctors.TryGetValue(block, out Doctor doctor))
                doctor.Heal(block);

            if (Agents.TryGetValue(block, out Agent agent))
                agent.Safe(block);
        }
    }
}
