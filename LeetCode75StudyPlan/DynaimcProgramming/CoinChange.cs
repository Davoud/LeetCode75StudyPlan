
using System.ComponentModel.Design;

namespace LeetCode75StudyPlan.DynaimcProgramming;

internal class CoinChanger : Solution<(int[] coins, int amount), int>
{
    public static int CoinChangeDP(int[] coins, int amount)
    {
        int max = amount + 1;
        var dp = new int[amount + 1];
        Array.Fill(dp, max);
        dp[0] = 0;
        
        foreach (var coin in coins)
        {            
            for (int r = coin; r <= amount; r++)
            {
                if (r - coin >= 0)
                    dp[r] = Math.Min(dp[r], dp[r - coin] + 1);
            }            
        }

        return dp[amount] > amount ? -1 : dp[amount];
    }
    public static int CoinChangeRecursiveMem(int[] coins, int amount)
    {
        int n = coins.Length;
        int MAX = int.MaxValue - 1;
        Dictionary<(int, int), int> mem = new();
        int result = Mem(0, amount);
       
        return result == MAX ? -1 : result;

        int Mem(int i, int v)
        {
            if (mem.TryGetValue((i, v), out int value)) return value;            
            value = Change(i, v);
            mem.Add((i, v), value);            
            return value;
        }

        int Change(int i, int r)
        {
            if (r == 0) return 0;
            if (i >= n || r < 0) return MAX;            
            if (coins[i] > r) return Mem(i + 1, r);            
            return Math.Min(Mem(i, r - coins[i]) + 1, Mem(i + 1, r));
        }
    }
    public static int CoinChangeRecursive(int[] coins, int amount)
    {
        int n = coins.Length;
        int MAX = int.MaxValue - 1;
        int result = Change(0, amount);

        return result == MAX ? -1 : result;

        int Change(int i, int r)
        {
            if (r == 0) return 0;            
            if (i >= n || r < 0) return MAX;
            
            if (coins[i] > r)
                return Change(i + 1, r - 0) + 0;
            
            return Math.Min(Change(i + 0, r - coins[i]) + 1, Change(i + 1, r - 0) + 0);            
        }
    }
    protected override string Title => "322. Coin Change";

    protected override IEnumerable<((int[] coins, int amount), int)> TestCases
    {
        get
        {
            yield return (([3, 4], 7), 2);           
            yield return (([1], 1), 1);
            yield return (([1, 2, 5], 11), 3);
            yield return (([2], 3), -1);
            yield return (([1], 0), 0);
        }
    }

    protected override int Solve((int[] coins, int amount) input)
    {
        //return CoinChangeRecursiveMem(input.coins, input.amount);
        return CoinChangeDP(input.coins, input.amount);
    }
}
