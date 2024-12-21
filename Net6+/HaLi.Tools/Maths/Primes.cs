using HaLi.Tools.Randomization;

namespace HaLi.Tools.Maths;

public static class Primes
{
    public static int Simple()
    {
        var primes = new List<int> { 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97 };
        return RNG.Random(primes);
    }

    public static int Generate() => Generate(8);
    public static int Generate(int digits)
    {
        if (digits < 1) throw new ArgumentException("Digits must be greater than 0");

        int lowerBound = (int)Math.Pow(10, digits - 1);
        int upperBound = (int)Math.Pow(10, digits) - 1;

        Random random = new Random();
        int prime;

        do
        {
            prime = random.Next(lowerBound, upperBound);
        } while (!IsPrime(prime));

        return prime;
    }

    public static bool IsPrime(int number)
    {
        if (number < 2) return false;
        for (int i = 2; i <= Math.Sqrt(number); i++)
        {
            if (number % i == 0) return false;
        }
        return true;
    }
}
