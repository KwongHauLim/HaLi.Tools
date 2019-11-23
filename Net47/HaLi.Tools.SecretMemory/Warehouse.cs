using System;
using Extensions.Data;

namespace HaLi.Tools.SecretMemory
{
    internal class Warehouse
    {
        public static Lazy<Warehouse> Share { get; private set; } = new Lazy<Warehouse>();
        public XXHash64 Hash64 { get; set; } = XXHash64.Create();
        public Memory Memory { get; set; } = new Memory();

        public Warehouse()
        {
        }
    }
}
