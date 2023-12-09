
using System.Globalization;

namespace LeetCode75StudyPlan.BinarySearch;

internal class SplitArrayLargestSum : Solution<(int[] nums, int k), int>
{
    protected override string Title => "410. Split Array Largest Sum";

    protected override IEnumerable<((int[] nums, int k), int)> TestCases
    {
        get
        {
            yield return (([7, 2, 5, 10, 8], 2), 18);
            yield return (([1, 2, 3, 4, 5], 2), 9);
        }
    }

    protected override int Solve((int[] nums, int k) input)
    {
        BinarySearcher bs = new(threshold =>
        {
            (int count, int total) = (1, 0);
            foreach (int num in input.nums)
            {
                total += num;
                if(total > threshold)
                {
                    total = num;
                    count++;
                    if (count > input.k) return false;
                }
            }
            return true;
        });

        return bs.SearchIn(input.nums.Max(), input.nums.Sum());
    }
}
