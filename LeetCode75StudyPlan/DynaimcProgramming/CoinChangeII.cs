using Dia2Lib;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode75StudyPlan.DynaimcProgramming;

internal class CoinChangeII : Solution<(int amount, int[] coins), int>
{
    public int Change(int amount, int[] coins)
    {
        Span<int> dp = stackalloc int[amount + 1];
        dp[0] = 1;
        foreach (int coin in coins)
        {
            for (int i = coin; i <= amount; i++)
            {
                dp[i] += dp[i - coin];
            }
        }

        return dp[amount];
    }

    public int ChangeSymbolTable(int amount, int[] coins) // terrible performance
    {
        int n = coins.Length;

        if (amount == 0) return 1;
        if (n == 0) return 0;

        Dictionary<(int, int), int> dp = [];

        for (int i = 1; i <= amount; i++)
            dp[(0, i)] = 0;

        for (int i = 1; i < n + 1; i++)
        {
            int coin = coins[i - 1];
            for (int sum = 1; sum < amount + 1; sum++)
            {
                dp[(i, sum)] = coin > sum
                    ? dp[(i - 1, sum)]
                    : dp[(i - 1, sum)] + (sum - coin == 0 ? 1 : dp[(i, sum - coin)]);
            }
        }        

        return dp[(n, amount)];       
    }
    public int ChangeDp(int amount, int[] coins)
    {
        int n = coins.Length;

        if (amount == 0) return 1;
        if (n == 0) return 0;

        int[,] dp = new int[n + 2, amount + 2];

        for (int i = 0; i < n + 1; i++)
            dp[i, 0] = 1;

        for (int i = 1; i < n + 1; i++)
            for (int sum = 1; sum < amount + 1; sum++)
                dp[i, sum] = coins[i - 1] > sum 
                    ? dp[i - 1, sum] 
                    : dp[i - 1, sum] + dp[i, sum - coins[i - 1]];
                                    
        return dp[n, amount];                        
    }
    public int ChangeMem(int amount, int[] c)
    {
        int n = c.Length;
        if (amount == 0)
            return 1;

        if (n == 0)
            return 0;

        Dictionary<(int, int), int> mem = [];

        int count = f(n, amount);

        int[,] dp = new int[n, amount];

        "".WriteLine();
        foreach (var item in mem.ToLookup(k => k.Key.Item1, v => (v.Key.Item2, v.Value)))
        {
            Console.Write($"{item.Key}: ");
            foreach (var value in item)
            {
                dp[item.Key - 1, value.Item1 - 1] = value.Value;
                Console.Write($"{value.Item1}({value.Value}) ");
            }
            Console.WriteLine();
        }

        Dump(amount, n, dp, c);

        return count;

        int f(int i, int sum)
        {
            var key = (i, sum);
            if (mem.TryGetValue(key, out int value)) return value;

            if (i == 0)
            {
                return 0;
            }

            if (sum == 0)
            {
                return 1;
            }

            int coin = c[i - 1];
            value = coin > sum ? f(i - 1, sum) : f(i, sum - coin) + f(i - 1, sum);
            return mem[key] = value;
        }
    }

    private static void Dump(int amount, int n, int[,] dp, int[] c)
    {
        Console.Write("\n  ");
        for (int i = 1; i <= amount; i++)
        {
            Console.Write($"{i,3}");
        }
        for (int i = 0; i < n; i++)
        {
            Console.Write($"\n{c[i]} ");
            for (int j = 0; j < amount; j++)
            {
                Console.Write($"{dp[i, j],3}");
            }
        }
        "".WriteLine();
    }

    public int ChangeRec(int amount, int[] c)
    {
        int len = c.Length;
        if (amount == 0) return 1;
        if (len == 0) return 0;
        return f(len, amount, []);

        int f(int n, int sum, ImmutableList<int> h)
        {
            if (n == 0)
            {
                return 0;
            }
            else if (sum == 0)
            {
                Console.WriteLine(string.Join(", ", h));
                return 1;
            }
            else if (c[n - 1] > sum)
            {
                return f(n - 1, sum, h);
            }
            else
            {
                return f(n, sum - c[n - 1], h.Add(c[n - 1])) + f(n - 1, sum, h);
            }
        }


    }

    protected override string Title => "518. Coin Change II";

    protected override IEnumerable<((int amount, int[] coins), int)> TestCases
    {
        get
        {
            yield return ((8, [3, 4, 5]), 2);
            yield return ((7, [1, 2, 4, 7]), 7);
            yield return ((5, [1, 2, 5]), 4);
            yield return ((3, [2]), 0);
            yield return ((10, [10]), 1);
        }
    }

    protected override int Solve((int amount, int[] coins) input)
        => Change(input.amount, input.coins);


}
