using System.Collections.Generic;

namespace HaLi.Tools.SecretMemory
{
    public class Memory
    {
        internal List<Block> Blocks { get; private set; }
        private int[] pointer;

        public Memory()
        {
            Blocks = new List<Block>();
            pointer = new int[0];
        }

        internal Block GetFreeBlock()
        {
            Block block = null;

            foreach (var i in pointer)
            {
                if (Blocks[i].Free > 0)
                    block = Blocks[i];
            }

            if (block == null)
            {
                block = new Block();
                Blocks.Add(block);
                pointer = Prime.GetShuffle(Blocks.Count);
                Spy.Protect.Add(block);
            }

            return block;
        }
    }
}
