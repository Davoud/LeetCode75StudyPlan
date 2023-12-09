using LeetCode75StudyPlan.Stack;
using System.Collections;
using System.Globalization;

namespace LeetCode75StudyPlan.Backtracking;

internal class SubsetsII : Solution<int[], IList<IList<int>>>
{
    private IList<IList<int>> res = new List<IList<int>>();
    private int[] nums = Array.Empty<int>();

    public IList<IList<int>> SubsetsWithDup(int[] nums)
    {
        res.Clear();
        this.nums = nums;
        Array.Sort(this.nums);
        //Backtrack(0, new(), false);

        Backtrack(0, new(nums.Length));

        return res;
    }

    private void Backtrack(int k, Stack<int> a, bool choose)
    {
        if (k == nums.Length)
        {
            res.Add(a.ToList());
        }
        else
        {
            Backtrack(k + 1, a, false);
            if (k < 1 || nums[k] != nums[k - 1] || choose)
            {
                a.Push(nums[k]);
                Backtrack(k + 1, a, true);
                a.Pop();
            }
        }
    }

    protected void Backtrack(int k, BitArray a)
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
            Backtrack(k + 1, a);

            if (k < 1 || nums[k] != nums[k - 1] || a[k - 1])
            {
                a[k] = true;
                Backtrack(k + 1, a);
            }
        }
    }


    protected override string Title => "90. Subsets II";

    protected override IEnumerable<(int[], IList<IList<int>>)> TestCases
    {
        get
        {
            yield return ([1, 2, 2],    [[1,2,2],[],[2],[2,2],[1],[1,2]]);
            yield return ([-1, 1, 2],   [[],[-1],[-1,1],[-1,1,2],[-1,2],[1],[1,2],[2]]);
            yield return ([0],          [[0],[]]);
            yield return ([3, 4, 3, 4], [[], [4], [3], [3, 4], [4, 4], [4, 3, 4], [3, 3], [3, 3, 4], [3, 4, 3, 4]]);
        }
    }


    protected override IList<IList<int>> Solve(int[] input) => SubsetsWithDup(input);

}

