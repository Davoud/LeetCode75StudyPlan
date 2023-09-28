
using System.Collections.Immutable;
using System.Globalization;

namespace LeetCode75StudyPlan.Backtracking;

internal class CombinationSumII : Solution<(int[] candidates, int target), IList<IList<int>>>
{
    
    private int[] candidates = Array.Empty<int>();

    private List<IList<int>> ans;
    private List<int> subset;
    private int target;

    public IList<IList<int>> CombinationSum2(int[] candidates, int target)
    {

        Array.Sort(candidates);
        ans = new List<IList<int>>();
        this.target = target;
        if (candidates[0] > target)
        {
            return ans;
        }

        

        Backtrack(candidates, ImmutableList<int>.Empty, 0, 0);
        return ans;
    }

    private void Backtrack(int[] candidates, ImmutableList<int> subset, int k, int sum)
    {
        if (k < candidates.Length)
        {
            sum += candidates[k];
            if (sum <= target)
            {
                subset = subset.Add(candidates[k]);
                if (sum == target)
                {
                    ans.Add(subset);
                }
                else
                {
                    Backtrack(candidates, subset, k + 1, sum);

                    subset = subset.RemoveAt(subset.Count - 1);
                    sum -= candidates[k];

                    while (k + 1 < candidates.Length && candidates[k] == candidates[k + 1]) k++;                    

                    Backtrack(candidates, subset, k + 1, sum);
                }
            }
        }
    }

    private void BackTrack(int k, int target, ImmutableList<int> path, IList<IList<int>> res)
    {
        $"{k}: {path.Str()}".WriteLine();
        if (target == 0)
        {
            res.Add(path);
        }
        else if(target > 0)
        {
            for (int i = k; i < candidates.Length; i++)
            {
                int c = candidates[i];               
                if (i > k && c != candidates[i - 1])
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
            for (int i = k; i < candidates.Length; i++)
            {
                if (i <= k || candidates[i] != candidates[i - 1])
                {
                    a[k] = candidates[i];
                    BackTrack(k + 1, target - candidates[i], a, res);                    
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
            yield return ((@int[2, 5, 2, 1, 2], 5), List2D("[1,2,2],[5]"));
            yield return ((@int[10, 1, 2, 7, 6, 1, 5], 8), List2D("[1,1,6],[1,2,5],[1,7],[2,6]"));
        }
    }
}
