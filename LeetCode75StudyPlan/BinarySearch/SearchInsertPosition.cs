using System.Data.SqlTypes;
using System.Globalization;

namespace LeetCode75StudyPlan.BinarySearch;

internal class SearchInsertPosition : Solution<(int[] nums, int target), int>
{
    protected override string Title => "35. Search Insert Position";

    protected override IEnumerable<((int[], int), int)> TestCases
    {
        get
        {
            yield return ((@int[1, 3, 5, 6], 5), 2);
            yield return ((@int[1, 3, 5, 6], 2), 1);
        }
    }

    protected override int Solve((int[] nums, int target) input)
    {
        BinarySearcher bs = new(i => input.nums[i] >= input.target);
        return bs.SearchIn(0, input.nums.Length);
    }
}
