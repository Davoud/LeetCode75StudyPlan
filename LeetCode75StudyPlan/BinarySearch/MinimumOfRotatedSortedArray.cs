namespace LeetCode75StudyPlan.BinarySearch;

internal class MinimumOfRotatedSortedArray : Solution<int[], int>
{
    public int FindMin(int[] nums)
    {
        (int lo, int hi) = (0, nums.Length - 1);
        while (nums[lo] > nums[hi])
        {
            int mid = lo + ((hi - lo) / 2);
            if (nums[mid] < nums[lo])
            {
                hi = mid;
            }
            else
            {
                lo = mid + 1;
            }
        }
        return nums[lo];
    }

    protected override string Title => "153. Find Minimum in Rotated Sorted Array (https://leetcode.com/problems/find-minimum-in-rotated-sorted-array/)";

    protected override IEnumerable<(int[], int)> TestCases
    {
        get
        {
            yield return ([3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 0, 1, 2],    0);
            yield return ([3, 4, 5, 1, 2],          1);
            yield return ([11, 13, 15, 17],        11);
        }
    }

    protected override int Solve(int[] nums) => FindMin(nums);
    
}
