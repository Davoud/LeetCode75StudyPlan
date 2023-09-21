
using System.ComponentModel.Design;

namespace LeetCode75StudyPlan.Backtracking;

internal class CombinationSumer : Solution<(int[] candidates, int target), IList<IList<int>>>
{
    
    private readonly IList<IList<int>> results = new List<IList<int>>();
    private int[] candidates = Array.Empty<int>();
    private int target;

    public IList<IList<int>> CombinationSum(int[] candidates, int target)
    {        
        this.candidates = candidates;
        this.target = target;
        Backtrack(0, 0, new());
        return results;
    }

    private void Backtrack(int k, int sum, Stack<int> a)
    {
        if (sum == target)
        {
            results.Add(a.ToList());
        }
        else
        {
            while (sum < target && k < candidates.Length)
            {
                a.Push(candidates[k]);
                Backtrack(k, sum + candidates[k], a);
                a.Pop();
                k++;
            }
        }
    }

    protected override string Title => "39. Combination Sum";

    protected override IEnumerable<((int[] candidates, int target), IList<IList<int>>)> TestCases
    {
        get
        {
            yield return ((@int[2, 3, 6, 7], 7), List(List(2, 2, 3), List(7)));
        }
    }

    protected override IList<IList<int>> Solve((int[] candidates, int target) input)
    {
        return CombinationSum(input.candidates, input.target);
    }
}
