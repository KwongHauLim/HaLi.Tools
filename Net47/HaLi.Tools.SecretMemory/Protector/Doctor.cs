using System;
using HaLi.Tools.Randomization;

namespace HaLi.Tools.SecretMemory.Protector
{
    /// <summary>
    /// Store spare data, able to restore block to trust
    /// </summary>
    internal class Doctor
    {
        public Block Block { get; set; }
        private readonly byte[] spare;
        private readonly byte[] xor;

        public Doctor(Block block)
        {
            Block = block;

            int size = block.Size;
            spare = new byte[size];
            Buffer.BlockCopy(block.data, 0, spare, 0, size);

            xor = new byte[8];
            RNG.Fill(xor);

            //block.BeforeWrite += Block_BeforeWrite;
            block.AfterWrite += Block_AfterWrite;
            //block.BeforeRead += Block_BeforeRead;
            //block.AfterRead += Block_AfterRead;
        }

        //private void Block_BeforeWrite(object sender, Block.EventArgs e)
        //{
        //}

        private void Block_AfterWrite(object sender, Block.EventArgs e)
        {
            if (Block.Trust)
            {
                spare[e.Position] = (byte)(e.Value ^ xor[e.Position % xor.Length]);
            }
        }

        //private void Block_BeforeRead(object sender, Block.EventArgs e)
        //{
        //    byte b = (byte)(spare[e.Position] ^ xor[e.Position % xor.Length]);
        //    if (e.Value != b)
        //        Block.Trust = false;
        //}

        //private void Block_AfterRead(object sender, Block.EventArgs e)
        //{
        //}

        internal void Heal(Block block)
        {
            Console.WriteLine("Heal");
            int size = spare.Length;
            byte[] real = new byte[size];
            for (int i = 0; i < size; i++)
            {
                real[i] = (byte)(spare[i] ^ xor[i % xor.Length]);
            }
            Buffer.BlockCopy(real, 0, block.data, 0, size);
            block.Trust = true;
        }
    }
}
