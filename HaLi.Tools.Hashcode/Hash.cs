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
    }

    public class Hash
    {
    }
}
