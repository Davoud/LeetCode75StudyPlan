using static System.Math;

namespace LeetCode75StudyPlan.DynaimcProgramming;

internal class StockExchange : Solution<int[], int>
{
   
    public static int MaxProfitDP2(int[] prices)
    {
        int sell = 0, buy = -999, s = 0, b = 0;
        Console.WriteLine("| Price | B     | Buy  | S      | Sell | S-Price | B+Price |");
        Console.WriteLine("|-------|-------|------|--------|------|---------|---------|");
        Console.WriteLine("|       |     0 | -999 |      0 |    0 |         |         |");
        foreach (int price in prices)
        {
            b = buy; 
            buy = Max(s - price, buy);            
            s = sell; 
            sell = Max(b + price, sell);
            Console.WriteLine($"|{price,6} |{b,6} |{buy,5} |{s,7} |{sell,5} |{s - price,8} |{b + price,8} |");
        }
        return sell;
    }
    

    public static int MaxProfitMem(int[] p)
    {
        int n = p.Length;
        Dictionary<(int, bool), int> mem = [];

        return Mp(0, true);

        int Mp(int day, bool buy)
        {
            if (day >= n) return 0;
            if (mem.TryGetValue((day, buy), out int v)) return v;
            v = Max(Mp(day + 1, buy), buy ? -p[day] + Mp(day + 1, false) : p[day] + Mp(day + 2, true));
            return mem[(day, buy)] = v;
        }
    }

    public static int MaxProfitRec1(int[] p)
    {
        int n = p.Length;
        return Buy(0);

        int Buy(int d)  => d >= n ? 0 : Max(Buy(d + 1), -p[d] + Sell(d + 1));
        int Sell(int d) => d >= n ? 0 : Max(Sell(d + 1), p[d] + Buy(d + 2));

    }


    public static int MaxProfitRec(int[] p)
    {
        int n = p.Length;
        return MaxProf(0, true);

        int MaxProf(int day, bool buy) => day >= n ? 0 : buy
                ? Max(MaxProf(day + 1, true), -p[day] + MaxProf(day + 1, false))
                : Max(MaxProf(day + 1, false), p[day] + MaxProf(day + 2, true));
    }

    protected override string Title => "309. Best Time to Buy and Sell Stock with Cooldown";

    protected override IEnumerable<(int[], int)> TestCases
    {
        get
        {
            yield return ([1, 2, 4], 3);
            yield return ([1, 2, 3, 0, 2], 3);
            yield return ([1], 0);
        }
    }

    protected override int Solve(int[] input)
    {
        return MaxProfitDP2(input);
        //return MaxProfitRec1(input);
    }
}


