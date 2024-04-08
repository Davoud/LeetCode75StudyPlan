
using System.Runtime.InteropServices.Marshalling;

namespace LeetCode75StudyPlan.DynaimcProgramming;

internal class LongestIncSubseq : Solution<int[], int>
{
    private class FenwickTree(int size)
    {
        private readonly int[] bit = new int[size + 1];

        public int this[int idx]
        {
            get
            {
                int ans = 0;
                while(idx > 0)
                {
                    ans = Math.Max(ans, bit[idx]);
                    idx -= idx & (-idx);
                }
                return ans;
            }
            set
            {
                while(idx < size + 1)
                {
                    bit[idx] = Math.Max(bit[idx], value);
                    idx += idx & (-idx);
                }
            }
        }

        public override string ToString() => bit.AsStr();
        
    }

    private int Compress(int[] nums)
    {        
        IList<int> set = [.. (new SortedSet<int>(nums))];
        int len = set.Count;
        for(int i = 0; i < nums.Length; i++)
        {
            int l = 0, r = len, m = 0, current = nums[i];
            while (l < r) if (set[m = (l + r) >> 1] >= current) r = m; else l = m + 1;
            nums[i] = l + 1;
        }
        return len;
    }

    // using Binary inext tree and compression
    public int LengthOfLIS4(int[] nums)
    {        
        int max = Compress(nums);        
        FenwickTree tree = new(max);

        foreach (int x in nums)
            tree[x] = tree[x - 1] + 1;
        
        return tree[max];
    }

    // using Binary Index Tree
    public int LengthOfLIS3(int[] nums)
    {
        const int B = 10001;
        const int MAX = 20001;
        FenwickTree tree = new(MAX);

        foreach(int x in nums)
            tree[B + x] = tree[B + x - 1] + 1;

        Console.WriteLine(tree);
        return tree[MAX];
    }

    //Binary search and greedy
    public static int LengthOfLIS2(int[] nums) 
    {
        List<int> sub = [];
        foreach(int x in nums)
        {
            if (sub.Count == 0 || sub[^1] < x)
            {
                sub.Add(x);
            }
            else
            {
                int l = 0, r = sub.Count, m = 0;
                while (l < r) if (sub[m = (l + r) >> 1] >= x) r = m; else l = m + 1;
                sub[l] = x;
            }
        }
        return sub.Count;
    }
    public static int LengthOfLIS(int[] nums)
    {      
        int n = nums.Length;
        int[] seq = new int[n];
        int lis = 0;
        for (int i = 0; i < n; i++)
        {
            int max = 0;
            int value = nums[i];
            for(int j = 0; j < i; j++)
            {
                if (nums[j] < value)
                {
                    max = Math.Max(max, seq[j] + 1);
                }
            }
            seq[i] = max;
            lis = Math.Max(max, lis);
        }
        seq.Dump();

        return lis + 1;
    }
    protected override string Title => "300. Longest Increasing Subsequence";

    protected override IEnumerable<(int[], int)> TestCases
    {
        get
        {
            yield return ([10, 9, 2, 5, 3, 7, 101, 18], 4);
            yield return ([0, 1, 0, 3, 2, 3], 4);
            yield return ([7, 7, 7, 7, 7, 7, 7], 1);
        }
    }

    protected override int Solve(int[] input)
    {
        //return LengthOfLIS(input);
        //return LengthOfLIS2(input);
        return LengthOfLIS4(input);
    }
}
