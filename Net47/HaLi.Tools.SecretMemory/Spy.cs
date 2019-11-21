using System;

namespace HaLi.Tools.SecretMemory
{
    internal static class Spy
    {
        internal static Warehouse House => Warehouse.Share.Value;
        internal static Memory Memory => House.Memory;
        internal static Block GetFreeBlock() => Memory.GetFreeBlock();

        internal static Protection Protect => Protection.Share.Value;

        public static int CalcHash(byte[] binary) => BitConverter.ToInt32(House.Hash64.ComputeHash(binary), 0);
        public static int CalcHash(int[] array)
        {
            byte[] binary = new byte[array.Length * 4];
            for (int i = 0; i < array.Length; i++)
            {
                Buffer.BlockCopy(
                    BitConverter.GetBytes(array[i]), 0,
                    binary, i * 4,
                    4);
            }
            return CalcHash(binary);
        }
    }
}
