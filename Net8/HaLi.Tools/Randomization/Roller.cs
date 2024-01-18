using HaLi.Tools.Randomization;

namespace HaLi.Tools.Roulette;

public class Roller
{
    public Table Table { get; set; }

    public object Next()
    {
        var w = Table.GetWeights();
        double r = RNG.Next(0.0, Table.sum);
        int i = 0;
        while (i < w.Count && w[i].CompareTo(r) < 0) i++;
        return Table[i];
    }
}

public class Table
{
    public Dictionary<object, double> Pocket { get; set; } = new Dictionary<object, double>();

    internal protected bool isChanged = false;
    internal List<object> prizes = new List<object>();
    private List<double> weights = new List<double>();
    internal double sum = 0.0;

    public object this[int index]
    {
        get
        {
            if (index >= 0 && index < prizes.Count)
                return prizes[index];
            else
                return null;
        }
    }

    public Table()
    {
        isChanged = true;
    }

    public void SetPrize(object prize, double chance)
    {
        if (chance.CompareTo(0) <= 0)
            Pocket.Remove(prize);
        else
            Pocket[prize] = chance;
        isChanged = true;
    }

    public List<double> GetWeights()
    {
        if (isChanged)
        {
            prizes.Clear();
            weights.Clear();

            sum = 0.0;
            foreach (var pair in Pocket)
            {
                prizes.Add(pair.Key);
                sum += pair.Value;
                weights.Add(sum);
            }
            isChanged = false;
        }
        return weights;
    }
}
