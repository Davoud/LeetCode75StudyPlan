using static System.Helper.Mathematics;

namespace LeetCode75StudyPlan.BinarySearch;

internal class MedianOfTwoSortedArrays : Solution<(int[] nums1, int[] nums2), double>
{

    public double FindMedianSortedArrays(int[] nums1, int[] nums2)
    {
        int n1 = nums1.Length, n2 = nums2.Length;
        if(n1 < n2) return FindMedianSortedArrays(nums2 , nums1);

        int lo = 0, hi = 2 * n2;
        while(lo <= hi) {
            int mid2 = (lo + hi) / 2;
            int mid1 = n1 + n2 - mid2;

            (double l1, double r1) = Find(nums1, mid1);
            (double l2, double r2) = Find(nums2, mid2);

            if (l1 > r2)
                lo = mid2 + 1;
            else if (l2 > r1)
                hi = mid2 - 1;
            else
                return (Math.Max(l1, l2) + Math.Min(r1, r2)) / 2;
        }

        return -1;

        (double left, double right) Find(int[] nums, int mid)
        {
            int l = mid == 0 ? int.MinValue : nums[(mid - 1) / 2];
            int r = mid == nums.Length * 2 ? int.MaxValue : nums[mid / 2];
            return (l, r);
        }
    }

    private static int[] MergeSorted((int[] nums1, int[] nums2) tc)
    {
        int[] n1 = tc.nums1, n2 = tc.nums2;
        int len1 = n1.Length, len2 = n2.Length;
        int len = len1 + len2;
        int[] n = new int[len];

        int i = 0, j = 0, k = 0;

        while (i < len1 && j < len2)
            n[k++] = n1[i] <= n2[j] ? n1[i++] : n2[j++];

        while (i < len1)
            n[k++] = n1[i++];

        while (j < len2)
            n[k++] = n2[j++];
        
        return n;
    }
    
    
    protected override string Title => "4. Median of Two Sorted Arrays";

    protected override IEnumerable<((int[] nums1, int[] nums2), double)> TestCases
    {
        get
        {
            var case1 = (@int[1, 3, 5, 7, 9, 11], @int[2, 4, 6, 8, 10, 12, 13, 14, 15, 16, 17, 20]);            
            yield return (case1, Median(MergeSorted(case1)));

            var case2 = (@int[1, 3], @int[2]);
            yield return (case2, Median(MergeSorted(case2)));

            var case3 = (@int[1, 2], @int[3, 4]);
            yield return (case3, Median(MergeSorted(case3)));

            var case4 = (@int[1, 2, 3, 3], @int[1, 1, 1, 1, 2, 2, 3, 3, 3, 4, 4]);
            yield return (case4, Median(MergeSorted(case4)));
        }
    }

    protected override double Solve((int[] nums1, int[] nums2) input) => FindMedianSortedArrays(input.nums1, input.nums2);
    
}