
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
        Backtrack(new Stack<int>(), 0, 0);
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
        var kth = nums[k++];
        
        sum += kth;

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

    

    private void BackTrack(int k, int target, ImmutableList<int> path, IList<IList<int>> res)
    {
        $"{k}: {path.Str()}".WriteLine();
        if (target == 0)
        {
            res.Add(path);
        }
        else if (target > 0)
        {
            for (int i = k; i < nums.Length; i++)
            {
                int c = nums[i];
                if (i > k && c != nums[i - 1])
                    continue;

                BackTrack(k + 1, target - c, path.Add(c), res);
            }
        }
    }

    private void BackTrack(int k, int target, int[] a, IList<IList<int>> res)
    {
        if (target == 0)
        {
            res.Add(a.Take(k).ToList());
        }
        else if (target > 0)
        {
            for (int i = k; i < nums.Length; i++)
            {
                if (i <= k || nums[i] != nums[i - 1])
                {
                    a[k] = nums[i];
                    BackTrack(k + 1, target - nums[i], a, res);
                }
            }
        }
    }



    protected override IList<IList<int>> Solve((int[] candidates, int target) input)
        => CombinationSum2(input.candidates, input.target);


    protected override string Title => "40. Combination Sum II";

    protected override IEnumerable<((int[] candidates, int target), IList<IList<int>>)> TestCases
    {
        get
        {
            yield return ((@int[1, 1], 2), List2D("[1,1]"));
            yield return ((@int[2, 5, 2, 1, 2], 5), List2D("[1,2,2],[5]"));
            yield return ((@int[10, 1, 2, 7, 6, 1, 5], 8), List2D("[1,1,6],[1,2,5],[1,7],[2,6]"));
        }
    }
}
