using System;

namespace HaLi.Tools.SecretMemory.Protector
{
    /// <summary>
    /// Checking block data correct,
    /// If found unbelieve data, mark this block untrust
    /// </summary>
    internal class Agent
    {
        public Block Block { get; set; }
        public int Write { get; private set; }
        public int Read { get; private set; }
        public int Hash { get; private set; }

        public bool FindShark { get; private set; }

        public Agent(Block block)
        {
            Block = block;

            Hash = Spy.CalcHash(Block.data);

            block.BeforeWrite += Block_BeforeWrite;
            block.AfterWrite += Block_AfterWrite;
            block.BeforeRead += Block_BeforeRead;
            block.AfterRead += Block_AfterRead;
        }

        private void Block_BeforeRead(object sender, Block.EventArgs e)
        {
            Check();
        }

        private void Block_AfterRead(object sender, Block.EventArgs e)
        {
            unchecked { Read++; }
        }

        private void Block_BeforeWrite(object sender, Block.EventArgs e)
        {
            Check();
        }

        private void Block_AfterWrite(object sender, Block.EventArgs e)
        {
            unchecked { Write++; }
            if (Block.Trust)
            {
                Hash = Spy.CalcHash(Block.data);
            }
        }

        private bool Check()
        {
            if (!FindShark)
                FindShark = Hash != Spy.CalcHash(Block.data);
            if (FindShark)
            {
                Console.WriteLine("Findshark");
                Block.Trust = false;
                Protection.FindShark = true;
                return true;
            }
            return false;
        }

        internal void Safe(Block block)
        {
            Hash = Spy.CalcHash(block.data);
            FindShark = false;
        }
    }
}
