using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaLi.Tools.Hashcode
{
    public interface IHashCalc
    {
        string GetHash(byte[] binary);
        string GetHash(string str);
    }

    public class Hash 
    {
        private static Hash _ptr = null;
        public static Hash Share => _ptr = _ptr ?? new Hash();

        public IHashCalc Algorithm { get; set; }

        private Hash()
        {
            Algorithm = new SHA1();
        }

        public static string GetHash(byte[] binary)
            => Share.Algorithm.GetHash(binary);

        public static string GetHash(string str)
            => Share.Algorithm.GetHash(str);
    }
}
