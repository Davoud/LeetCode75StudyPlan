namespace LeetCode75StudyPlan.BinarySearch;

internal class KthSmallestPairDistance : Solution<(int[] nums, int k), int>
{
    protected override string Title => "719. Find K-th Smallest Pair Distance";

    public int SmallestDistancePair(int[] nums, int k)
    {
        Array.Sort(nums);
        int n = nums.Length;
        int left = 0, right = nums[^1] - nums[0];
        while (left < right)
        {
            int mid = (left + right) >> 1;
            if(AnyKPairsLessThanOrEqualTo(mid)) 
                right = mid; 
            else 
                left = mid + 1;
        }      

        return left;
       
        bool AnyKPairsLessThanOrEqualTo(int distance)
        {
            int slow = 0, fast = 0, count = 0;
            while (slow < n || fast < n)
            {
                while (fast < n && nums[fast] - nums[slow] <= distance) fast++;
                count += fast - slow - 1;
                slow++;
            }
            return count >= k;
        }
    }

    protected override IEnumerable<((int[] nums, int k), int)> TestCases
    {
        get
        {
            yield return (([1, 3, 1], 1), 0);
            yield return (([1, 1, 1], 2), 0);
            yield return (([1, 6, 1], 3), 5);
        }
    }

    protected override int Solve((int[] nums, int k) input) => SmallestDistancePair(input.nums, input.k);
    
}
