=== HaLi.Tools.Randomization ===
Random functions in easy access

### Super easy use
    using HaLi.Tools.Randomization;

    int r1 = RNG.Int32;      // Get a random int    
    long r2 = RNG.Int64;     // Get a random long    
    float r3 = RNG.Float;    // Get a random float
    double r4 = RNG.Double;  // Get a random double
    uint r5 = RNG.UInt32;    // Get a random uint
    ulong r6 = RNG.UInt64;   // Get a random ulong
    
    int r7 = RNG.Next(0,100);        // Get a random 0 ~ 99 of int
    double r8 = RNG.Next(0.0,100.0); // Get a random 0 ~ 99 of double
    
    // fill array
    int[] r9 = new int[100];
    long[] r10 = new long[100];
    float[] r11 = new float[100];
    double[] r12 = new double[100];
    uint[] r13 = new uint[100];
    ulong[] r14 = new ulong[100];
    RNG.Fill(r9);
    RNG.Fill(r10);
    RNG.Fill(r11);
    RNG.Fill(r12);
    RNG.Fill(r13);
    RNG.Fill(r14);
    
#### What is inside
It is use [XorShift](http://en.wikipedia.org/wiki/Xorshift) method to generate random number</br>


