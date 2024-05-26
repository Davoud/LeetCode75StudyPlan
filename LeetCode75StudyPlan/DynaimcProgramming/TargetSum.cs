using Iced.Intel;
using Microsoft.Diagnostics.NETCore.Client;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode75StudyPlan.DynaimcProgramming;

internal class TargetSum : Solution<(int[] nums, int target), int>
{

    public int FindTargetSumWays(int[] nums, int target)
    {        
        int[] noZero = [.. nums.Where(i => i != 0)];
        int z = (1 << (nums.Length - noZero.Length));
        if(noZero.Length == 0) return z; 
        int c = Find2(noZero, target);
        return c * z;        
    }

    private int Find2(int[] nums, int target)
    {
        var total = nums.Sum();
        if (target > total || target < -total) return 0;

        var tt = 2 * total;
       
        var dp = new int[tt + 1];
        
        dp[total - nums[0]] = 1;
        dp[total + nums[0]]++;
        
        for (var i = 1; i < nums.Length; i++)
        {
            var next = new int[tt + 1];
            
            for(int n = 0; n <= tt; n++)            
            {                
                if (dp[n] > 0)
                {
                    next[n - nums[i]] += dp[n];
                    next[n + nums[i]] += dp[n];
                }
            }
            dp = next;            
        }
        return dp[total + target];                
    }

    private int Find(int[] nums, int target)
    {
        int count = 0;
               
        int[] dp = new int[1 << nums.Length];
        dp[0] = nums[0];
        dp[1] = -nums[0];                

        for (int i = 1; i < nums.Length; i++)
        {           
            int sign = 1;
            for (int j = (1 << i + 1) - 1; j >= 0; j--)        
            {                                
                dp[j] = dp[j / 2] + (sign * nums[i]);
                sign *= -1;
            }            
        }

        for (int j = 0; j < dp.Length; j++)
        {
            if (dp[j] == target)
            {
                count++;
            }
        }

        return count;
    }

    public int FindTargetSumWaysDP2(int[] nums, int target)
    {
        int n = nums.Length;
        int count = 0;
        int[,] dp = new int[n, 1 << n];
        int num = nums[0];
        dp[0, 0] = num;
        dp[0, 1] = -num;

        for (int i = 1; i < n; i++)
        {
            num = nums[i];
            var m = (1 << i+1);            
            int sign = 1;
            for (int j = 0; j < m; j++)
            {
                int p = dp[i - 1, j / 2];
                dp[i, j] = p + (sign * num);                
                sign *= -1;
            }
        }

        for (int j = 0; j < dp.GetLength(1); j++)
        {
            if (dp[n - 1, j] == target)
            {
                count++;
            }
        }
        Dump(dp, nums, target);
        return count;
    }

    private void Dump(int[,] dp, int[] nums, int target)
    {
        int w = dp.GetLength(1);
        Console.WriteLine(StringOf(20, '■'));
        for(int i = 0; i < dp.GetLength(0); i++)
        {            
            for(int j = 0; j < (1 << i+1) && j < w; j++)
            {
                //if (dp[i, j] == 0) break;
                Console.Write($"{dp[i, j],4}");
            }
            Console.WriteLine($" ==> {nums[i]}");
        }
    }

    public int FindTargetSumWaysMem(int[] nums, int target)
    {
        Dictionary<(int, int), int> mem = [];
        return Find(nums.Length - 1, target);
        int Find(int i, int t)
        {
            if (mem.TryGetValue((i, t), out int value))
            {
                return value;
            }
            if (i <= -1)
            {
                return t == 0 ? 1 : 0;
            }
            else
            {
                value = Find(i - 1, t + nums[i]) + Find(i - 1, t - nums[i]);
                return mem[(i, t)] = value;
            }
        }
    }
    public int FindTargetSumWaysRecSimplified(int[] nums, int target)
    {
        return Find(nums.Length, target);
        int Find(int i, int t)
        {
            if (i == 0)
            {
                return t == 0 ? 1 : 0;
            }
            else
            {
                i--;
                return Find(i, t + nums[i]) + Find(i, t - nums[i]);
            }
        }
    }
    public int FindTargetSumWaysRec(int[] nums, int target)
    {

        int count = 0;
        Find(0, target, "");
        return count;

        void Find(int i, int t, string path)
        {
            if (i == nums.Length)
            {
                if (t == 0)
                {
                    count++;
                    Console.WriteLine($"{path}");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{path}");
                    Console.ResetColor();
                }
            }
            else
            {
                int n = nums[i];
                Find(i + 1, t + n, $"{path} + {n}");
                Find(i + 1, t - n, $"{path} - {n}");
            }
        }

    }
    protected override string Title => "494. Target Sum";

    protected override IEnumerable<((int[] nums, int target), int)> TestCases
    {
        get
        {
            yield return (([0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0], 0), 1048576);
            yield return (([9, 7, 0, 3, 9, 8, 6, 5, 7, 6], 2), 40);
            //yield break;
            yield return (([0, 0, 0, 0, 0, 0, 0, 0, 1], 1), 256);
            yield return (([0, 0, 0, 0, 1], 1), 16);
            yield return (([2, 1], 1), 1);
            yield return (([1, 1, 1, 1, 1], 3), 5);
            yield return (([1, 2, 3, 4, 5], 15), 1);
            yield return (([1], 1), 1);
        }
    }

    protected override int Solve((int[] nums, int target) input)
    {
        //return Find(input.nums, input.target);            
        return FindTargetSumWays(input.nums, input.target);
        //return FindTargetSumWaysDP2(input.nums, input.target);
        //return FindTargetSumWaysRecSimplified(input.nums, input.target);
        //return FindTargetSumWaysMem(input.nums, input.target);
    }
}
