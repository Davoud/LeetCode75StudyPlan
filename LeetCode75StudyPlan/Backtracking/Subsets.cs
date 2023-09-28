
using System;
using System.Globalization;

namespace LeetCode75StudyPlan.Backtracking;




class Subset : ITestable
{    
    private int[] nums = Array.Empty<int>();
    private bool[] a = Array.Empty<bool>();
    
    // backtracking 
    public IList<IList<int>> SubsetsByBacktracking(int[] nums)
    {
        this.nums = nums;
        var res = new List<IList<int>>();
        a = new bool[nums.Length];
        Backtrack(0, res);
        return res;
    }

    // bit manupilation
    public IList<IList<int>> SubsetsByBitManupulation(int[] nums)
    {
        var max = (int)Math.Pow(2, nums.Length);
        var res = new List<IList<int>>(max);

        for(uint i = 0; i < max; i++)
        {
            var subset = new List<int>();
            uint value = i;
            for(int j = 0; j < nums.Length; j++)
            {
                if ((value & 1) == 1) subset.Add(nums[j]);
                value >>= 1;
            }             
            res.Add(subset);
        }        

        return res;
    }
    

    protected void Backtrack(int k, List<IList<int>> res)
    {       
        if (k == a.Length)
        {
            List<int> subset = new();
            for (int i = 0; i < k; i++)
                if (a[i]) subset.Add(nums[i]);
            res.Add(subset);            
        }
        else
        {           
            a[k] = false;
            Backtrack(k + 1, res);
           
            a[k] = true;
            Backtrack(k + 1, res);            
        }
    }  

    public void RunTests()
    {
        var s = new Subset();
        var set = @int[1, 4, 10, 3];
        int i = 0;

        var subsets = s.SubsetsByBacktracking(set);
        foreach (var item in subsets)
        {
            $"{i++}\t{item.AsStr("{", "}")}".WriteLine();
        }

        var subsets2 = s.SubsetsByBitManupulation(set);
        i = 0;
        Console.WriteLine("\n");
        foreach (var item in subsets2)
        {
            $"{i++}\t{item.AsStr("{", "}")}".WriteLine();
        }

    }
}

internal class SubsetsGen : Backtracker<int>, ITestable
{

    private IList<IList<int>> results;

    public IList<IList<int>> Subsets()
    {
        Backtrack(new int[input], 0);
        return results;
    }

    public SubsetsGen(int input) : base(input)
    {
        results = new List<IList<int>>();
    }
   
    

    protected override bool IsASolution(int[] a, int k)
    {
        return k == input;
    }

    protected override void ProcessSolution(int[] a, int k)
    {        
        List<int> subset = new();
        for(int i = 0; i < k; i++)           
            if (a[i] == 1)
                subset.Add(i);                    
        results.Add(subset);
    }

    protected override IList<int> ConstructCondidates(int[] a, int k)
    {
        return new List<int> { 1, 0 };
    }

    void ITestable.RunTests()
    {
        for (int i = 1; i < 6; i++)
        {
            $"A Set with {i} members:".WriteLine();
            foreach (var item in new SubsetsGen(i).Subsets())
            {
                $"{item.AsStr("{ ", " }")}".WriteLine();
            }
        }
    }
}
