using System;
using System.Collections.Generic;

namespace HaLi.Tools.SecretMemory
{
    public class Memory
    {
        public List<Block> Blocks { get; private set; }

        public Memory()
        {
            Blocks = new List<Block>();
        }

        public Block GetFreeBlock()
        {
            Block block = null;

            for (int i = 0; i < Blocks.Count; i++)
            {
                if (Blocks[i].Free > 0)
                    block = Blocks[i];            
            }

            if (block == null)
            {
                block = new Block();
                Blocks.Add(block);
            }

            return block;
        }
    }
}
