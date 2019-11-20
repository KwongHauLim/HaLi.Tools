using System;
using System.Collections.Generic;
using System.Text;
using Extensions.Data;

namespace HaLi.Tools.SecretMemory
{
    internal class SecretLibrary
    {
        public static Lazy<SecretLibrary> Share { get; private set; } = new Lazy<SecretLibrary>();
        public Memory Memory { get; set; } = new Memory();
        public XXHash64 Hash64 { get; set; } = XXHash64.Create();

        public SecretLibrary()
        {
        }

    }

    internal static class S
    {
        internal static SecretLibrary Library => SecretLibrary.Share.Value;
        internal static Memory Memory => Library.Memory;
        internal static Block GetFreeBlock() => Memory.GetFreeBlock();

        public static int CalcHash(byte[] binary) => BitConverter.ToInt32(SecretLibrary.Share.Value.Hash64.ComputeHash(binary), 0);
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
