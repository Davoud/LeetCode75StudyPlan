using System.Collections;
using System.Collections.Immutable;

namespace LeetCode75StudyPlan.Backtracking;

internal class Permutations : Solution<int[], IList<IList<int>>>
{
    private IList<IList<int>> results = new List<IList<int>>();
    private int[] nums = Array.Empty<int>();
    public IList<IList<int>> Permute(int[] nums)
    {
        results.Clear();        
        this.nums = nums;
        //Backtrack(0, new int[nums.Length], ImmutableHashSet.Create(nums));
        
        Backtrack(0, new int[nums.Length], new BitArray(nums.Length));
        return results;
    }

    private void Backtrack(int k, int[] a, ImmutableHashSet<int> notVisited)
    {
        if(k == a.Length)
        {
            results.Add(a.ToList());
        }
        else
        {            
            foreach(int item in notVisited)
            {
                a[k] = item;                
                Backtrack(k + 1, a, notVisited.Remove(item));                
            }
        }
    }

    private void Backtrack(int k, int[] a, BitArray visited)
    {
        if (k == a.Length)
        {
            results.Add(a.ToList());
        }
        else
        {
            for (int i = 0; i < visited.Length; i++)
            {
                if (!visited[i])
                {
                    a[k] = nums[i];
                    visited[i] = true;
                    Backtrack(k + 1, a, visited);
                    visited[i] = false;
                }
            }
        }
    }

    protected override string Title => "46. Permutations";

    protected override IEnumerable<(int[], IList<IList<int>>)> TestCases
    {
        get
        {
            yield return (@int[1, 2, 3],
                List(List(1, 2, 3), List(1, 3, 2), List(2, 1, 3), List(2, 3, 1), List(3, 1, 2), List(3, 2, 1)));

            yield return (@int[0, 1], List(List(0, 1), List(1, 0)));

            yield return (@int[1], List(List(1)));

        }
    }

    protected override IList<IList<int>> Solve(int[] input) => Permute(input);

   
}
