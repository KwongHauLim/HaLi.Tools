using System;

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
            block.BeforeWrite += Block_BeforeWrite;
            block.AfterWrite += Block_AfterWrite;
            block.BeforeRead += Block_BeforeRead;
            block.AfterRead += Block_AfterRead;
        }

        private void Block_BeforeRead(object sender, EventArgs e)
        {
            FindShake = Hash != Spy.CalcHash(Block.data);
        }

        private void Block_AfterRead(object sender, EventArgs e)
        {
            unchecked { Read++; }
        }
        
        private void Block_BeforeWrite(object sender, EventArgs e)
        {
            FindShake = Hash != Spy.CalcHash(Block.data);
        }

        private void Block_AfterWrite(object sender, EventArgs e)
        {
            unchecked { Write++; }
            Hash = Spy.CalcHash(Block.data);
        }
    }
}
