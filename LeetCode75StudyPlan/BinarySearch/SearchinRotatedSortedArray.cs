namespace LeetCode75StudyPlan.BinarySearch;

internal class SearchinRotatedSortedArray : Solution<(int[] nums, int target), int>
{
    protected override string Title => "33. Search in Rotated Sorted Array";

    protected override IEnumerable<((int[] nums, int target), int)> TestCases
    {
        get
        {
            yield return ((@int[4, 5, 6, 7, 0, 1, 2], 0), 4);
            yield return ((@int[4, 5, 6, 7, 0, 1, 2], 4), -1);
            yield return ((@int[1], 0), -1);
        }
    }

    protected override int Solve((int[] nums, int target) input)
    {
        return -1;
    }
}
