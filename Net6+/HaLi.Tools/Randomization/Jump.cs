using HaLi.Tools.Maths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaLi.Tools.Randomization;

public class Jump
{
    public int Prime { get; private set; }
    public int Maximum { get; private set; }
    public int Start { get; private set; }
    public int Last { get; private set; }
    public int Remain { get; private set; }
    public bool IsAvailable => Remain > 0;
    private bool isPow = false;

    private Jump() { }

    public Jump(int digits) : this((int)Math.Pow(10, digits), Primes.Generate(digits))
    {
        isPow = true;
    }

    public Jump(int max, int prime)
    {
        Prime = prime;
        Maximum = max;
        Remain = Maximum;
        Last = RNG.Next(0, Maximum);
        Start = Last;
        // Check max is Power of 10
        isPow = (Maximum & (Maximum - 1)) == 0;
    }

    public static Jump ReadFromStream(Stream stream)
    {
        var j = new Jump();

        byte[] buffer = new byte[4];
        stream.Read(buffer, 0, 4);
        j.Prime = BitConverter.ToInt32(buffer, 0);
        stream.Read(buffer, 0, 4);
        j.Maximum = BitConverter.ToInt32(buffer, 0);
        stream.Read(buffer, 0, 4);
        j.Last = BitConverter.ToInt32(buffer, 0);
        stream.Read(buffer, 0, 4);
        j.Remain = BitConverter.ToInt32(buffer, 0);

        int digits = j.Maximum.ToString().Length;

        return j;
    }

    public static void WriteToStream(Stream stream, Jump jump)
    {
        stream.Write(BitConverter.GetBytes(jump.Prime), 0, 4);
        stream.Write(BitConverter.GetBytes(jump.Maximum), 0, 4);
        stream.Write(BitConverter.GetBytes(jump.Last), 0, 4);
        stream.Write(BitConverter.GetBytes(jump.Remain), 0, 4);
    }

    public int Next()
    {
        Last += Prime;
        if (Last >= Maximum)
        {
            if (isPow)
            {
                Last %= Maximum;
            }
            else
            {
                Last = --Start;
            }
        }
        Remain--;
        return Last;
    }
}
