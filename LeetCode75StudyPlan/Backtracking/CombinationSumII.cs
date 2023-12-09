
using System.Collections.Immutable;
using System.Globalization;

namespace LeetCode75StudyPlan.Backtracking;

internal class CombinationSumII : Solution<(int[] candidates, int target), IList<IList<int>>>
{

    private int[] nums = Array.Empty<int>();
    private List<IList<int>> res;
    private int target;

    public IList<IList<int>> CombinationSum2(int[] candidates, int target)
    {
        Array.Sort(candidates);
        this.nums = candidates;
        res = new List<IList<int>>();
        this.target = target;
        if (candidates[0] > target) return res;
        Backtrack(ImmutableList.Create<int>(), 0, 0);
        ////Backtrack(new Stack<int>(), 0, 0);
        return res;
    }

    private void Backtrack(Stack<int> subset, int k, int sum)
    {
        var kth = nums[k++];
        sum += kth;

        if (sum == target)
        {            
            subset.Push(kth);
            res.Add(subset.ToList());
            subset.Pop();
        }
        else if (k < nums.Length && sum < target)
        {
            subset.Push(kth);
            Backtrack(subset, k, sum);
            subset.Pop();

            while (k < nums.Length - 1 && kth == nums[k]) k++;
            Backtrack(subset, k, sum - kth);
        }
    }

    private void Backtrack(ImmutableList<int> subset, int k, int sum)
    {
        var kth = nums[k];        
        sum += kth;
        k++;

        if (sum == target)
        {
            res.Add(subset.Add(kth));
        }
        else if (k < nums.Length && sum < target)
        {
            Backtrack(subset.Add(kth), k, sum);

            while (k < nums.Length - 1 && kth == nums[k]) k++;

            Backtrack(subset, k, sum - kth);
        }
    }


    protected override IList<IList<int>> Solve((int[] candidates, int target) input)
        => CombinationSum2(input.candidates, input.target);


    protected override string Title => "40. Combination Sum II";

    protected override IEnumerable<((int[] candidates, int target), IList<IList<int>>)> TestCases
    {
        get
        {
            yield return (([1, 1], 2), [[1,1]]);
            yield return (([2, 5, 2, 1, 2], 5), [[1,2,2],[5]]);
            yield return (([10, 1, 2, 7, 6, 1, 5], 8), [[1,1,6],[1,2,5],[1,7],[2,6]]);
        }
    }
}
