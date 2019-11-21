using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaLi.Tools.SecretMemory.Protector
{
    internal class Agent
    {
        public Block Block { get; set; }
        public int Write { get; private set; }
        public int Read { get; private set; }
        public int Hash { get; private set; }

        public bool FindShake { get; private set; }

        public Agent(Block block)
        {
            Block = block;
            block.OnWrite += Block_OnWrite;
            block.OnRead += Block_OnRead;
        }

        private void Block_OnRead(object sender, EventArgs e)
        {
            unchecked { Read++; }
            FindShake = Hash != Spy.CalcHash(Block.data);
        }

        private void Block_OnWrite(object sender, EventArgs e)
        {
            unchecked { Write++; }
            Hash = Spy.CalcHash(Block.data);
        }
    }
}
