namespace LeetCode75StudyPlan.BinarySearch;

internal class MedianOfTwoSortedArrays : Solution<(int[] nums1, int[] nums2), double>
{

    public double FindMedianSortedArrays(int[] nums1, int[] nums2)
    {
        return 0;
    }

    protected override string Title => "4. Median of Two Sorted Arrays";

    protected override IEnumerable<((int[] nums1, int[] nums2), double)> TestCases
    {
        get
        {
            yield return ((@int[1, 3], @int[2]), 2.0);
            yield return ((@int[1, 2], @int[3, 4]), 2.5);
        }
    }

    protected override double Solve((int[] nums1, int[] nums2) input) => FindMedianSortedArrays(input.nums1, input.nums2);
    
}