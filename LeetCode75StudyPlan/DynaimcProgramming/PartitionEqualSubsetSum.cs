

using LeetCode75StudyPlan.BinarySearch;
using System.Collections;
using System.Numerics;
using System.Runtime.Intrinsics.Arm;

namespace LeetCode75StudyPlan.DynaimcProgramming;

internal class PartitionEqualSubsetSum : Solution<int[], bool>
{
    public bool CanPartitionBitMask(int[] nums)
    {
        int sum = nums.Sum();
        if ((sum & 1) == 1) return false;               
        
        BigInteger dp = BigInteger.One, half = dp << (sum >> 1);
        
        foreach (int num in nums)
            dp |= dp << num;

        return (dp & half) != 0;        
    }
    public bool CanPartition(int[] nums)
    {        
        int t = nums.Sum();
        if ((t & 1) == 1) return false;
        
        int h = t / 2;
        BitArray dp = new(h + 1);
        dp[0] = true;
        
        foreach (int num in nums)
            for (int j = h; j >= num; j--) dp[j] |= dp[j - num];                    
        
        return dp[h];
    }

    public bool CanPartitionDP2(int[] nums)
    {
        int n = nums.Length;
        int totalSum = nums.Sum();
        Dictionary<int, bool> dp = [];
        return (totalSum & 1) != 1 && CP(totalSum / 2);

        bool CP(int sum, int i = 0)
        {
            if (sum == 0) return true;
            if (i >= n || sum < 0) return false;
            if (dp.TryGetValue(sum, out bool v)) return v;
            v = CP(sum - nums[i], i + 1) || CP(sum, i + 1);
            return dp[sum] = v;            
        }
    }

    public bool CanPartitionDP1(int[] nums)
    {
        int n = nums.Length;
        int totalSum = nums.Sum();
        bool?[] dp = new bool?[10001];
        return (totalSum & 1) != 1 && CP(totalSum / 2);

        bool CP(int sum, int i = 0)
        {
            if (sum == 0) return true;
            if (i >= n || sum < 0) return false;
            if (dp[sum] is bool v) return v;
            v = CP(sum - nums[i], i + 1) || CP(sum, i + 1);
            dp[sum] = v;
            return v;            
        }
    }

    public bool CanPartitionRec2(int[] nums)
    {
        int n = nums.Length;
        int totalSum = nums.Sum();
        return (totalSum & 1) != 1 && CP(totalSum / 2);

        bool CP(int sum, int i = 0)
        {
            if (sum == 0) return true;
            if (i >= n && sum < 0) return false;
            return CP(sum - nums[i], i + 1) || CP(sum - 0, i + 1);
        }
    }

    public bool CanPartitionRec1(int[] nums)
    {
        int n = nums.Length;
        return CP();

        bool CP(int i = 0, int sum1 = 0, int sum2 = 0) {
            if (i >= n) return sum1 == sum2;                    
            return CP(i + 1, sum1 + nums[i], sum2) || CP(i + 1, sum1, sum2 + nums[i]);       
        }
    }


    protected override string Title => "416. Partition Equal Subset Sum";

    protected override IEnumerable<(int[], bool)> TestCases
    {
        get
        {
            yield return ([100, 100, 100, 100, 100, 100, 100, 100], true);
            yield return ([2, 2, 1, 1], true);            
            yield return ([1, 5, 3], false);
            yield return ([1, 5, 11, 5], true);
            yield return ([1, 2, 3, 5], false);
        }
    }

    protected override bool Solve(int[] input) => CanPartitionBitMask(input);
    
}

